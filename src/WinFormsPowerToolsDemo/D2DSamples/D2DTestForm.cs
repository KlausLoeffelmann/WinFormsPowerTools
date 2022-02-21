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
        private IDirect2DImage _d2dImage;

        public D2DTestForm()
        {
            InitializeComponent();
        }

        private void d2dPanel1_PaintIGraphics(object sender, PaintIGraphicsEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.FillRectangle(Brushes.Green, 10, 10, 200, 200);
            e.Graphics.DrawRectangle(Pens.DarkBlue, 10, 10, 200, 200);

            IDirect2DImaging d2dImaging = (IDirect2DImaging)e.Graphics;

            if (_d2dImage is null)
            {
                Image image = Properties.Resources.TestImage;
                _d2dImage = d2dImaging.FromImage(image);
            }

            d2dImaging.DrawImage(
                _d2dImage,
                15,
                15,
                _d2dImage.Width,
                _d2dImage.Height);
        }

        private void _btnWritePixels_Click(object sender, EventArgs e)
        {
            if (_d2dImage is null)
            {
                return;
            }

            var random = new Random();
            for (int i=0;i< _d2dImage.BitmapBytes.Length;i++)
            {
                _d2dImage.BitmapBytes[i] = (byte)random.Next(255);
            }

            _d2dImage.ApplyBitmapBytes();
            d2dPanel1.Invalidate();
        }
    }
}
