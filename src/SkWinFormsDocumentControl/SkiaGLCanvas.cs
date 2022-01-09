using OpenTK.WinForms;
using SkiaSharp;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SkiaWinForms
{
    public class SkiaGLCanvas : GLControl
    {
		private const SKColorType colorType = SKColorType.Rgba8888;
		private const GRSurfaceOrigin surfaceOrigin = GRSurfaceOrigin.BottomLeft;
		private const int Stencil = 8;
		private const int Samples = 2;

		private GRContext? _grContext;
		private GRGlFramebufferInfo _glInfo;
		private GRBackendRenderTarget? _renderTarget;
		private SKSurface? _surface;
		private SKCanvas? _canvas;
		private SKSizeI _lastSize;

		// Are we in DesignMode or not?
		private bool designMode;

		public SkiaGLCanvas() : base()
		{
			Initialize();
		}

		private void Initialize()
		{
			designMode = DesignMode;
			ResizeRedraw = true;
		}

		public SKSize CanvasSize => _lastSize;

		public GRContext? GRContext => _grContext;

		[Category("Appearance")]
		public event EventHandler<SkiaPaintEventArgs>? PaintSurface;

		protected override void OnPaint(PaintEventArgs e)
		{
			if (designMode)
			{
				e.Graphics.Clear(BackColor);
				return;
			}

			base.OnPaint(e);

			MakeCurrent();

			// create the contexts if not done already
			if (_grContext == null)
			{
				var glInterface = GRGlInterface.Create();
				GRContextOptions grContextOptions = new();
				grContextOptions.BufferMapThreshold = 500000;
				_grContext = GRContext.CreateGl(glInterface, grContextOptions);
			}

			// get the new surface size
			var newSize = new SKSizeI(Width, Height);

			// manage the drawing surface
			if (_renderTarget == null || _lastSize != newSize || !_renderTarget.IsValid)
			{
				// create or update the dimensions
				_lastSize = newSize;

				//GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
				//GL.GetInteger(GetPName.StencilRef, out var stencil); stencil = 8;
				//GL.GetInteger(GetPName.Samples, out var samples); samples = 16;

				var maxSamples = _grContext.GetMaxSurfaceSampleCount(colorType);

				//if (samples > maxSamples)
				//	samples = maxSamples;
				var framebuffer = 0;
				_glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());

				// destroy the old surface
				_surface?.Dispose();
				_surface = null;
				_canvas = null;

				// re-create the render target
				_renderTarget?.Dispose();
				_renderTarget = new GRBackendRenderTarget(newSize.Width, newSize.Height, Samples, Stencil, _glInfo);
			}

			// create the surface
			if (_surface == null)
			{
				SKSurfaceProperties props = new(SKPixelGeometry.Unknown);
				_surface = SKSurface.Create(_grContext, _renderTarget, surfaceOrigin, colorType, props);
				_canvas = _surface.Canvas;
			}

			using (new SKAutoCanvasRestore(_canvas, false))
			{
				// start drawing
				OnPaintSurface(new SkiaPaintEventArgs(
					_surface,
					surfaceOrigin,
					new SKImageInfo(_renderTarget.Width, _renderTarget.Height, colorType)));
			}

			// update the control
			var info=_renderTarget.GetGlFramebufferInfo(out _glInfo);	
			_canvas?.Flush();
			SwapBuffers();
		}

		protected virtual void OnPaintSurface(SkiaPaintEventArgs e)
		{
			// invoke the event
			PaintSurface?.Invoke(this, e);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			// clean up
			_canvas = null;
			_surface?.Dispose();
			_surface = null;
			_renderTarget?.Dispose();
			_renderTarget = null;
			_grContext?.Dispose();
			_grContext = null;
		}
	}
}
