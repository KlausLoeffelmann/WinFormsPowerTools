using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsPowerToolsDemo.SkiaSharpDemo;

namespace WinFormsPowerToolsDemo
{
    public partial class SkiaSharpDemoForm : Form
    {
        private Shapes _movingCircleShapes;
        private Timer _timer;

        public SkiaSharpDemoForm()
        {
            _movingCircleShapes = Shapes.RandomShapes(1000);
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _movingCircleShapes.Trigger();

            if (tabControl1.SelectedIndex == 0)
            {
                gdiPlusRenderTargetPanel.Invalidate();
            }
            else
            {
                skiaCanvasRenderTarget.Invalidate();
            }
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (_timer.Enabled)
            {
                _timer.Enabled = false;
                startStopButton.Text = "Start";
            }
            else
            {
                _timer.Enabled = true;
                startStopButton.Text = "Stop";
            }
        }

        private void gdiPlusRenderTargetPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _movingCircleShapes.OnGdiplusRender(e.Graphics);
        }

        private void skiaCanvasRenderTarget_PaintSurface(object sender, SkiaWinForms.SKPaintGLSurfaceEventArgs e)
        {
            e.Surface.Canvas.Clear();
            _movingCircleShapes.OnSkiaRender(e.Surface);
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                _movingCircleShapes.CanvasSizeChanged(
                    new SizeF(
                        gdiPlusRenderTargetPanel.ClientSize.Width,
                        gdiPlusRenderTargetPanel.ClientSize.Height));
            }
            else
            {
                _movingCircleShapes.CanvasSizeChanged(
                    new SizeF(
                        skiaCanvasRenderTarget.ClientSize.Width,
                        skiaCanvasRenderTarget.ClientSize.Height));
            }
        }

        private void openSkiaPlaygroundButton_Click(object sender, EventArgs e)
        {
            SkiaSharpPlaygroundForm form = new();
            form.ShowDialog();
        }
    }
}
