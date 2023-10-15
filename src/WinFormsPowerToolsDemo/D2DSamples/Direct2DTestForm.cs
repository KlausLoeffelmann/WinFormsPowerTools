﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Forms.Direct2D;

namespace WinFormsPowerToolsDemo
{
    public partial class Direct2DTestForm : Form
    {
        private IDirect2DImage? _d2dImage;

        public Direct2DTestForm()
        {
            InitializeComponent();
            direct2dPanel1.HandleCreated += D2dPanel1_HandleCreated;
        }

        private void D2dPanel1_HandleCreated(object? sender, EventArgs e)
        {
            if (_d2dImage is null)
            {
                //Image image = Properties.Resources.TestImage;
                using Image image = new Bitmap(200, 1, PixelFormat.Format32bppArgb);

                _d2dImage = ((IDirect2DImaging)direct2dPanel1.Graphics).FromImage(image);
            }
        }

        private void d2dPanel1_PaintIGraphics(object sender, GraphicsPaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.FillRectangle(Brushes.Green, 10, 10, 200, 200);
            e.Graphics.DrawRectangle(Pens.DarkBlue, 10, 10, 200, 200);

            IDirect2DImaging d2dImaging = (IDirect2DImaging)e.Graphics;

            d2dImaging.DrawImage(
                _d2dImage!,
                15,
                15,
                _d2dImage!.Width,
                _d2dImage!.Height * 2);
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
            direct2dPanel1.Invalidate();
        }
    }
}
