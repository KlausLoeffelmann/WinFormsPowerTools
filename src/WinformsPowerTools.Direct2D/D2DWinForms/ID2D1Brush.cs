﻿using System.Drawing;

using Windows.Win32;
using Windows.Win32.Graphics.Direct2D;
using Windows.Win32.Graphics.Direct2D.Common;

namespace System.Windows.Forms.D2D
{
    internal class ID2D1Brush
    {
        ID2D1SolidColorBrush _brush;
        private const int MaxCachedBrushes = 10;

        private static WeakCache<Brush, ID2D1Brush> s_brushCache = new(MaxCachedBrushes);

        private ID2D1Brush(ID2D1SolidColorBrush brush)
        {
            _brush = brush;
        }

        public ID2D1SolidColorBrush Brush => _brush;

        public static ID2D1Brush FromSolidBrush(SolidBrush brush, ID2D1RenderTarget renderTarget)
        {
            if (s_brushCache.TryGetValue(brush, out var d2dBrush))
            {
                return d2dBrush!;
            }

            D2D1_COLOR_F strokeColor;

            strokeColor.a = brush.Color.A;
            strokeColor.b = brush.Color.B;
            strokeColor.g = brush.Color.G;
            strokeColor.r = brush.Color.R;

            renderTarget.CreateSolidColorBrush(in strokeColor, null, out var strokeColorBrush);

            d2dBrush = new(strokeColorBrush);
            s_brushCache.Cache(brush, d2dBrush);

            return d2dBrush;
        }
    }
}
