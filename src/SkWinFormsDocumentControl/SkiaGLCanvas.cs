using OpenTK.WinForms;
using SkiaSharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SkiaWinForms
{
    public class SkiaGLCanvas : GLControl
    {
		private const SKColorType colorType = SKColorType.Rgba8888;
		private const GRSurfaceOrigin surfaceOrigin = GRSurfaceOrigin.BottomLeft;
		private const int Stencil = 8;
		private const int Samples = 8;

		private GRContext? grContext;
		private GRGlFramebufferInfo glInfo;
		private GRBackendRenderTarget? renderTarget;
		private SKSurface? surface;
		private SKCanvas? canvas;
		private SKSizeI lastSize;

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

		public SKSize CanvasSize => lastSize;

		public GRContext? GRContext => grContext;

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
			if (grContext == null)
			{
				var glInterface = GRGlInterface.Create();
				GRContextOptions grContextOptions = new();
				grContext = GRContext.CreateGl(glInterface, grContextOptions);
			}

			// get the new surface size
			var newSize = new SKSizeI(Width, Height);

			// manage the drawing surface
			if (renderTarget == null || lastSize != newSize || !renderTarget.IsValid)
			{
				// create or update the dimensions
				lastSize = newSize;

				//GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
				//GL.GetInteger(GetPName.StencilRef, out var stencil); stencil = 8;
				//GL.GetInteger(GetPName.Samples, out var samples); samples = 16;

				var maxSamples = grContext.GetMaxSurfaceSampleCount(colorType);

				//if (samples > maxSamples)
				//	samples = maxSamples;
				var framebuffer = 0;
				glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());

				// destroy the old surface
				surface?.Dispose();
				surface = null;
				canvas = null;

				// re-create the render target
				renderTarget?.Dispose();
				renderTarget = new GRBackendRenderTarget(newSize.Width, newSize.Height, Samples, Stencil, glInfo);
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
				OnPaintSurface(new SkiaPaintEventArgs(
					surface,
					surfaceOrigin,
					new SKImageInfo(renderTarget.Width, renderTarget.Height, colorType)));
			}

			// update the control
			canvas?.Flush();
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
			canvas = null;
			surface?.Dispose();
			surface = null;
			renderTarget?.Dispose();
			renderTarget = null;
			grContext?.Dispose();
			grContext = null;
		}
	}
}
