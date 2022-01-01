using SkiaSharp;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SkiaWinForms
{
    public class SkiaCanvas : Control
	{
		private Bitmap? bitmap;

		// Are we in DesignMode or not?
		private bool designMode;

		public SkiaCanvas()
		{
			Initialize();
		}

		private void Initialize()
		{
			designMode = DesignMode;
			DoubleBuffered = true;
			SetStyle(ControlStyles.ResizeRedraw, true);
		}

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

			// get the bitmap
			var info = CreateBitmap();

			if (info.Width == 0 || info.Height == 0)
				return;

			var data = bitmap!.LockBits(
				new Rectangle(0, 0, Width, Height),
				ImageLockMode.WriteOnly,
				bitmap.PixelFormat);

			// create the surface
			using (var surface = SKSurface.Create(info, data.Scan0, data.Stride))
			{
				// start drawing
				OnPaintSurface(new SkiaPaintEventArgs(surface, info));

				surface.Canvas.Flush();
			}

			// write the bitmap to the graphics
			bitmap.UnlockBits(data);
			e.Graphics.DrawImage(bitmap, 0, 0);
		}

		private SKImageInfo CreateBitmap()
		{
			var info = new SKImageInfo(Width, Height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

			if (bitmap == null || bitmap.Width != info.Width || bitmap.Height != info.Height)
			{
				FreeBitmap();

				if (info.Width != 0 && info.Height != 0)
					bitmap = new Bitmap(
						info.Width,
						info.Height,
						System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
			}

			return info;
		}

		private void FreeBitmap()
		{
			if (bitmap != null)
			{
				bitmap.Dispose();
				bitmap = null;
			}
		}

		protected virtual void OnPaintSurface(SkiaPaintEventArgs e)
		{
			// invoke the event
			PaintSurface?.Invoke(this, e);
		}
	}
}
