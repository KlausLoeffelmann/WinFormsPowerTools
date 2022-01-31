using System.Drawing;
using Windows.Win32;
using Windows.Win32.Graphics.Direct2D;
using Windows.Win32.Graphics.Direct2D.Common;

namespace System.Windows.Forms.D2D
{
    // TODO: Implement IDisposable.
    internal class ID2D1Pen
    {
        private const int MaxCachedPens = 10;

        ID2D1SolidColorBrush _penBrush;
        float _penSize;
        ID2D1StrokeStyle? _penStyle;

        private static WeakCache<Pen, ID2D1Pen> s_penCache = new(MaxCachedPens);

        private ID2D1Pen(ID2D1SolidColorBrush penBrush, float penSize, ID2D1StrokeStyle? penStyle = null)
        {
            _penBrush = penBrush;
            _penSize = penSize;
            _penStyle = penStyle;
        }

        public ID2D1SolidColorBrush PenBrush => _penBrush;
        public float PenSize => _penSize;
        public ID2D1StrokeStyle? PenStyle => _penStyle;

        public static ID2D1Pen FromPen(Pen pen, ID2D1RenderTarget renderTarget)
        {
            if (s_penCache.TryGetValue(pen, out var d2dPen))
            {
                return d2dPen!;
            }

            D2D1_COLOR_F strokeColor;

            strokeColor.a = pen.Color.A;
            strokeColor.b = pen.Color.B;
            strokeColor.g = pen.Color.G;
            strokeColor.r = pen.Color.R;

            renderTarget.CreateSolidColorBrush(in strokeColor, null, out var strokeColorBrush);

            d2dPen = new(strokeColorBrush, pen.Width);
            s_penCache.Cache(pen, d2dPen);

            return d2dPen;
        }
    }
}
