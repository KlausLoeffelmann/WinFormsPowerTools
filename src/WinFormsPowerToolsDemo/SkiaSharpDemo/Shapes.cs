using SkiaSharp;
using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace WinFormsPowerToolsDemo.SkiaSharpDemo
{
    internal class Shapes : Collection<Shape>
    {
        public void Trigger()
        {
            foreach (var item in this)
            {
                item.Trigger();
            }
        }

        public void CanvasSizeChanged(SizeF canvasSize)
        {
            foreach (var item in this)
            {
                item.CanvasSizeChanged(canvasSize);
            }
        }

        public void OnGdiplusRender(Graphics g)
        {
            foreach (var item in this)
            {
                item.OnGdiplusRender(g);
            }
        }

        public void OnSkiaRender(SKSurface surface)
        {
            foreach (var item in this)
            {
                item.OnSkiaRender(surface);
            }
        }

        public static Shapes RandomShapes(int count)
        {
            Random random = new(DateTime.Now.Millisecond);
            
            Shapes shapes = new Shapes();
            for (int i = 0; i < count; i++)
            {
                shapes.Add(new MovingCircleShape(
                    lineColor: Color.FromArgb(random.Next()),
                    fillColor: Color.FromArgb(random.Next()),
                    penWidth: random.Next(5) + 2,
                    radius: random.Next(100) + 20,
                    xSpeed: (float)(random.NextDouble() * 10 + .1),
                    ySpeed: (float)(random.NextDouble() * 10 + .1),
                    startLocation: new PointF(random.Next(200), random.Next(200))));
            }

            return shapes;
        }
    }
}
