using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.D2D;

namespace WinFormsPowerToolsDemo
{
    public partial class D2DTestForm : Form
    {
        public D2DTestForm()
        {
            InitializeComponent();
        }

        private void d2dPanel1_PaintIGraphics(object sender, PaintIGraphicsEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.FillRectangle(Brushes.Green, 10, 10, 200, 200);
            e.Graphics.DrawRectangle(Pens.DarkBlue, 10, 10, 200, 200);

            Image image = Properties.Resources.TestImage;
            e.Graphics.DrawImage(image, 10, 10, image.Width, image.Height);
        }
    }
}
