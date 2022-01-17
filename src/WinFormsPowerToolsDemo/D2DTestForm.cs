using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPowerToolsDemo
{
    public partial class D2DTestForm : Form
    {
        public D2DTestForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            d2dGraphicsView1.Drawable = new LineFigure();
        }
    }

    public class LineFigure : IDrawable
    {
        public void Draw(ICanvas canvas, Microsoft.Maui.Graphics.RectangleF dirtyRect)
        {
            canvas.StrokeColor = Colors.Blue;
            canvas.DrawLine(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
        }
    }
}
