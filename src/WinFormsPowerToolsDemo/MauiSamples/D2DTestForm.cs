using Microsoft.Maui.Graphics;
using System;
using System.Windows.Forms;

namespace WinFormsPowerToolsDemo.MauiGraphics
{
    public partial class D2DTestForm : Form
    {
        private DrawableShapes _movingCircleShapes;
        private Timer _timer;

        public D2DTestForm()
        {
            _movingCircleShapes = DrawableShapes.RandomShapes(1000);
            InitializeComponent();
            D2dGraphicsView_Resize(null, null);

            _d2dGraphicsView.Drawable = _movingCircleShapes;

            _timer = new Timer();
            _timer.Interval = 10;
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _movingCircleShapes.Trigger();
            _d2dGraphicsView.Invalidate();
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (_timer.Enabled)
            {
                _timer.Enabled = false;
                _startStopButton.Text = "Start";
            }
            else
            {
                _timer.Enabled = true;
                _startStopButton.Text = "Stop";
            }
        }

        private void D2dGraphicsView_Resize(object sender, EventArgs e)
        {
            _movingCircleShapes.CanvasSizeChanged(
                new SizeF(
                    _d2dGraphicsView.ClientSize.Width,
                    _d2dGraphicsView.ClientSize.Height));
        }
    }
}
