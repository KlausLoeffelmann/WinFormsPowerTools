﻿// MIT Licence.
// From: https://github.com/mono/SkiaSharp/blob/ce7778c0c48b5ea668d91420023b295d5551006f/source/SkiaSharp.Views/SkiaSharp.Views.Shared/SKPaintGLSurfaceEventArgs.cs

using SkiaSharp;

namespace SkiaWinForms
{
    public class SkiaPaintEventArgs
    {
		//public SkiaPaintEventArgs(SKSurface surface)
		//	: this(surface, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888)
		//{
		//}

		public SkiaPaintEventArgs(SKSurface surface, SKImageInfo info)
			: this(surface, GRSurfaceOrigin.BottomLeft, info)
		{
		}


		public SkiaPaintEventArgs(SKSurface surface, GRSurfaceOrigin origin, SKImageInfo info)
			: this(surface, origin, info, info)
		{
		}

		//public SkiaPaintEventArgs(SKSurface surface, GRSurfaceOrigin origin, SKColorType colorType)
		//{
		//	Surface = surface;
		//	//BackendRenderTarget = renderTarget;
		//	ColorType = colorType;
		//	Origin = origin;
		//	Info = new SKImageInfo(renderTarget.Width, renderTarget.Height, ColorType);
		//	RawInfo = Info;
		//}

		public SkiaPaintEventArgs(SKSurface surface, GRSurfaceOrigin origin, SKImageInfo info, SKImageInfo rawInfo)
		{
			Surface = surface;
			//BackendRenderTarget = renderTarget;
			ColorType = info.ColorType;
			Origin = origin;
			Info = info;
			RawInfo = rawInfo;
		}

		//public SkiaPaintEventArgs(SKSurface surface, GRSurfaceOrigin origin, SKColorType colorType, GRGlFramebufferInfo glInfo)
		//{
		//	Surface = surface;
		//	//BackendRenderTarget = renderTarget;
		//	ColorType = colorType;
		//	Origin = origin;
		//	Info = new SKImageInfo(renderTarget.Width, renderTarget.Height, colorType);
		//	RawInfo = Info;
		//}

		public SKSurface Surface { get; private set; }
		//public GRBackendRenderTarget BackendRenderTarget { get; private set; }
		public SKColorType ColorType { get; private set; }
		public GRSurfaceOrigin Origin { get; private set; }
		public SKImageInfo Info { get; private set; }
		public SKImageInfo RawInfo { get; private set; }
	}
}
