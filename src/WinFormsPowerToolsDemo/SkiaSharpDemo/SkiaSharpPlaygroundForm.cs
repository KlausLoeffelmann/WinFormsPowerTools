using SkiaSharp;
using System.Windows.Forms;

namespace WinFormsPowerToolsDemo
{
    public partial class SkiaSharpPlaygroundForm : Form
    {
        public SkiaSharpPlaygroundForm()
        {
            InitializeComponent();
        }

        private void skiaCanvas1_PaintSurface(object sender, SkiaWinForms.SkiaPaintEventArgs e)
        {
            SKPaint _fillingPaint = new();
            _fillingPaint.Color = new SKColor(200, 10, 10, 200);
            _fillingPaint.Style = SKPaintStyle.Fill;

            SKPaint _drawingPaint = new();
            //_drawingPaint.BlendMode = SKBlendMode.Color;
            _drawingPaint.Color = new SKColor(10, 10, 200, 150);
            _drawingPaint.StrokeWidth = 5;
            _drawingPaint.Style = SKPaintStyle.Stroke;

            e.Surface.Canvas.DrawCircle(200, 200, 100, _fillingPaint);
            e.Surface.Canvas.DrawCircle(200, 200, 100, _drawingPaint);
        }
    }
}
