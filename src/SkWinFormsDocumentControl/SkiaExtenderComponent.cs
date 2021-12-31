using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.WinForms;
using SkiaSharp;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NativeWindow = OpenTK.Windowing.Desktop.NativeWindow;

namespace SkiaWinForms
{
    internal class SkiaExtenderComponent : Component
    {
		private const SKColorType colorType = SKColorType.Rgba8888;
		private const GRSurfaceOrigin surfaceOrigin = GRSurfaceOrigin.BottomLeft;

		private GLControlSettings _glControlSettings;
		private GRContext? grContext;
		private GRGlFramebufferInfo glInfo;
		private GRBackendRenderTarget? renderTarget;
		private SKSurface? surface;
		private SKCanvas? canvas;
		private SKSizeI lastSize;
		private NativeWindow? _nativeWindow = null;
		private bool _resizeEventSuppressed;
        private Control? _extendingControl;

        [Category("Appearance")]
		public event EventHandler<SKPaintGLSurfaceEventArgs>? PaintSurface;

        public SkiaExtenderComponent()
        {
			_glControlSettings = new GLControlSettings();
		}

		public Control? ExtendingControl {
			get => _extendingControl; 
			set
            {
				_extendingControl = value;
            }
		}

		private void HookUpEvents()
        {
            if (ExtendingControl is null)
            {
				throw new NullReferenceException("Cannot hook-up events when the Control is not set.");
            }

			ExtendingControl.HandleCreated += ExtendingControl_HandleCreated;
            ExtendingControl.HandleDestroyed += ExtendingControl_HandleDestroyed;
            ExtendingControl.ParentChanged += ExtendingControl_ParentChanged;
            ExtendingControl.Paint += ExtendingControl_Paint;
            ExtendingControl.Resize += ExtendingControl_Resize;
		}

		private void UnhookEvents()
		{
			if (ExtendingControl is null)
			{
				throw new NullReferenceException("Cannot hook-up events when the Control is not set.");
			}

			ExtendingControl.HandleCreated -= ExtendingControl_HandleCreated;
			ExtendingControl.ParentChanged -= ExtendingControl_ParentChanged;
			ExtendingControl.Paint -= ExtendingControl_Paint;
			ExtendingControl.HandleDestroyed -= ExtendingControl_HandleDestroyed;
			ExtendingControl.Resize -=ExtendingControl_Resize;
		}

		/// <summary>
		/// This is invoked when the Resize event is triggered, and is used to position
		/// the internal GLFW window accordingly.
		///
		/// Note: This method may be called before the OpenGL context is ready or the
		/// NativeWindow even exists, so everything inside it requires safety checks.
		/// </summary>
		/// <param name="e">An EventArgs instance (ignored).</param>
		private void ExtendingControl_Resize(object? sender, EventArgs e)
		{
		}

		private void ExtendingControlResizeCore()
        {
			// Do not raise OnResize event before the handle and context are created.
			if (!ExtendingControl!.IsHandleCreated)
			{
				_resizeEventSuppressed = true;
				return;
			}

			ResizeNativeWindow();
		}

		private void ExtendingControl_HandleDestroyed(object? sender, EventArgs e)
        {
			DestroyNativeWindow();
		}

		private void ExtendingControl_Paint(object sender, PaintEventArgs e)
		{
			if (IsDesignMode())
			{
				e.Graphics.Clear(ExtendingControl!.BackColor);
				return;
			}

			EnsureCreated();
			MakeCurrent();

			// create the contexts if not done already
			if (grContext == null)
			{
				var glInterface = GRGlInterface.Create();
				grContext = GRContext.CreateGl(glInterface);
			}

			// get the new surface size
			var newSize = new SKSizeI(ExtendingControl!.Width, ExtendingControl!.Height);

			// manage the drawing surface
			if (renderTarget == null || lastSize != newSize || !renderTarget.IsValid)
			{
				// create or update the dimensions
				lastSize = newSize;

				GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
				GL.GetInteger(GetPName.StencilBits, out var stencil);
				GL.GetInteger(GetPName.Samples, out var samples);

				var maxSamples = grContext.GetMaxSurfaceSampleCount(colorType);
				if (samples > maxSamples)
					samples = maxSamples;

				glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());

				// destroy the old surface
				surface?.Dispose();
				surface = null;
				canvas = null;

				// re-create the render target
				renderTarget?.Dispose();
				renderTarget = new GRBackendRenderTarget(newSize.Width, newSize.Height, samples, stencil, glInfo);
			}

			// create the surface
			if (surface == null)
			{
				surface = SKSurface.Create(grContext, renderTarget, surfaceOrigin, colorType);
				canvas = surface.Canvas;
			}

			using (new SKAutoCanvasRestore(canvas, true))
			{
				// start drawing
				OnPaintSurface(new SKPaintGLSurfaceEventArgs(surface, renderTarget, surfaceOrigin, colorType, glInfo));
			}

			// update the control
			canvas!.Flush();
			SwapBuffers();
		}

		/// <summary>
		/// This event is raised when this control's parent control is changed,
		/// which may result in this control becoming a different size or shape, so
		/// we capture it to ensure that the underlying GLFW window gets correctly
		/// resized and repositioned as well.
		/// </summary>
		/// <param name="e">An EventArgs instance (ignored).</param>
		private void ExtendingControl_ParentChanged(object? sender, EventArgs e)
		{
			ResizeNativeWindow();
		}

		private void ExtendingControl_HandleCreated(object? sender, EventArgs e)
        {
			CreateNativeWindow(_glControlSettings.ToNativeWindowSettings());

			if (_resizeEventSuppressed)
			{
				ExtendingControlResizeCore();
				_resizeEventSuppressed = false;
			}

			if (IsDesignMode())
			{
				// _designTimeRenderer = new GLControlDesignTimeRenderer(this);
			}

			if (ExtendingControl!.Focused || (_nativeWindow?.IsFocused ?? false))
			{
				ForceFocusToCorrectWindow();
			}
		}

		/// <summary>
		/// Because we're really two windows in one, keyboard-focus is a complex
		/// topic.  To ensure correct behavior, we have to capture the various attempts
		/// to assign focus to one or the other window, and if focus is sent to the
		/// wrong window, we have to redirect it to the correct one.  So every attempt
		/// to set focus to *either* window will trigger this method, which will force
		/// the focus to whichever of the two windows it's supposed to be on.
		/// </summary>
		private void ForceFocusToCorrectWindow()
		{
			if (IsDesignMode() || _nativeWindow == null)
				return;

			unsafe
			{
				if (IsNativeInputEnabled(_nativeWindow))
				{
					// Focus should be on the NativeWindow inside the GLControl.
					_nativeWindow.Focus();
				}
				else
				{
					// Focus should be on the GLControl itself.
					ExtendingControl!.Focus();
				}
			}
		}

		/// <summary>
		/// Determine if native input is enabled for the given NativeWindow.
		/// </summary>
		/// <param name="nativeWindow">The NativeWindow to query.</param>
		/// <returns>True if native input is enabled; false if it is not.</returns>
		private unsafe bool IsNativeInputEnabled(NativeWindow nativeWindow)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				IntPtr hWnd = GLFW.GetWin32Window(nativeWindow.WindowPtr);
				IntPtr style = Win32.GetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE);
				return ((Win32.WindowStyles)(long)style & Win32.WindowStyles.WS_DISABLED) == 0;
			}
			else
			{
				throw new NotSupportedException("The current operating system is not supported by this control.");
			}
		}

		/// <summary>
		/// Construct the child NativeWindow that will wrap the underlying GLFW instance.
		/// </summary>
		/// <param name="nativeWindowSettings">The NativeWindowSettings to use for
		/// the new GLFW window.</param>
		private void CreateNativeWindow(NativeWindowSettings nativeWindowSettings)
		{
			if (IsDesignMode())
			{
				return;
			}

			_nativeWindow = new NativeWindow(nativeWindowSettings);
			//_nativeWindow.FocusedChanged += OnNativeWindowFocused;

			NonportableReparent(_nativeWindow);

			// Force the newly child-ified GLFW window to be resized to fit this control.
			ResizeNativeWindow();

			// And now show the child window, since it hasn't been made visible yet.
			_nativeWindow.IsVisible = true;
		}

		/// <summary>
		/// Ensure that the required underlying GLFW window has been created.
		/// </summary>
		private void EnsureCreated()
		{
			if (ExtendingControl!.IsHandleCreated)
			{
				ExtendingControl.CreateControl();

				if (_nativeWindow == null)
					throw new InvalidOperationException("Failed to create GLControl."
						+ " This is ususally caused by trying to perform operations on the GLControl"
						+ " before its containing form has been fully created.  Make sure you are not"
						+ " invoking methods on it before the Form's constructor has completed.");
			}

			if (_nativeWindow == null && !IsDesignMode())
			{
				throw new InvalidOperationException("Failed to recreate GLControl :-(");
			}
		}

		/// <summary>
		/// Reparent the given NativeWindow to be a child of this GLControl.  This is a
		/// non-portable operation, as its name implies:  It works wildly differently
		/// between OSes.  The current implementation only supports Microsoft Windows.
		/// </summary>
		/// <param name="nativeWindow">The NativeWindow that must become a child of
		/// this control.</param>
		private unsafe void NonportableReparent(NativeWindow nativeWindow)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				IntPtr hWnd = GLFW.GetWin32Window(nativeWindow.WindowPtr);

				// Reparent the real HWND under this control.
				Win32.SetParent(hWnd, ExtendingControl!.Handle);

				// Change the real HWND's window styles to be "WS_CHILD | WS_DISABLED" (i.e.,
				// a child of some container, with no input support), and turn off *all* the
				// other style bits (most of the rest of them could cause trouble).  In
				// particular, this turns off stuff like WS_BORDER and WS_CAPTION and WS_POPUP
				// and so on, any of which GLFW might have turned on for us.
				IntPtr style = (IntPtr)(long)(Win32.WindowStyles.WS_CHILD
					| Win32.WindowStyles.WS_DISABLED);

				Win32.SetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_STYLE, style);

				// Change the real HWND's extended window styles to be "WS_EX_NOACTIVATE", and
				// turn off *all* the other extended style bits (most of the rest of them
				// could cause trouble).  We want WS_EX_NOACTIVATE because we don't want
				// Windows mistakenly giving the GLFW window the focus as soon as it's created,
				// regardless of whether it's a hidden window.
				style = (IntPtr)(long)Win32.WindowStylesEx.WS_EX_NOACTIVATE;
				Win32.SetWindowLongPtr(hWnd, Win32.WindowLongs.GWL_EXSTYLE, style);
			}
			else
			{
				throw new NotSupportedException("The current operating system is not supported by this control.");
			}
		}
		private bool IsDesignMode()
			=> IsAncestorSiteInDesignModeInternal;

		private bool IsAncestorSiteInDesignModeInternal =>
			GetSitedParentSite(ExtendingControl!) is ISite thisOrAncestorSite ? thisOrAncestorSite.DesignMode : false;

		private ISite? GetSitedParentSite(Control control) =>
			control is null
				? throw new ArgumentNullException(nameof(control))
				: control.Site != null || control.Parent is null
					? control.Site
					: GetSitedParentSite(control.Parent);


		protected virtual void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
		{
			// invoke the event
			PaintSurface?.Invoke(this, e);
		}

		/// <summary>
		/// Makes this control's OpenGL context current in the calling thread.
		/// All OpenGL commands issued are hereafter interpreted by this context.
		/// When using multiple GLControls, calling MakeCurrent on one control
		/// will make all other controls non-current in the calling thread.
		/// A GLControl can only be current in one thread at a time.
		/// </summary>
		public void MakeCurrent()
		{
			if (ExtendingControl!.IsHandleCreated && !DesignMode)
			{
				EnsureCreated();
				_nativeWindow!.MakeCurrent();
			}
		}

		/// <summary>
		/// Swaps the front and back buffers, presenting the rendered scene to the user.
		/// </summary>
		public void SwapBuffers()
		{
			if (IsDesignMode())
				return;

			EnsureCreated();
			_nativeWindow!.Context.SwapBuffers();
		}

		/// <summary>
		/// Gets the <see cref="IGraphicsContext"/> instance that is associated with the <see cref="GLControl"/>.
		/// </summary>
		public IGraphicsContext? Context => _nativeWindow?.Context;

		/// <summary>
		/// Destroy the child NativeWindow that wraps the underlying GLFW instance.
		/// </summary>
		private void DestroyNativeWindow()
		{
			if (_nativeWindow != null)
			{
				_nativeWindow.Dispose();
				_nativeWindow = null!;
			}
		}

		/// <summary>
		/// Resize the native window to fit this control.
		/// </summary>
		private void ResizeNativeWindow()
		{
			if (IsDesignMode())
				return;

			if (_nativeWindow != null)
			{
				_nativeWindow.ClientRectangle = new Box2i(0, 0, ExtendingControl!.Width, ExtendingControl!.Height);
			}
		}
	}
}
