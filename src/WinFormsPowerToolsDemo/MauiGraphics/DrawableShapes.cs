using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;

namespace WinFormsPowerToolsDemo.MauiGraphics
{
    internal class DrawableShapes : Collection<DrawableShape>, IDrawable
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

        public static DrawableShapes RandomShapes(int count)
        {
            Random random = new(DateTime.Now.Millisecond);
            
            DrawableShapes shapes = new DrawableShapes();
            for (int i = 0; i < count; i++)
            {
                shapes.Add(new MovingCircleShape(
                    lineColor: Color.FromRgba((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)),
                    fillColor: Color.FromRgba((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)),
                    penWidth: random.Next(5) + 2,
                    radius: random.Next(100) + 20,
                    xSpeed: (float)(random.NextDouble() * 10 + .1),
                    ySpeed: (float)(random.NextDouble() * 10 + .1),
                    startLocation: new PointF(random.Next(200), random.Next(200))));
            }

            return shapes;
        }

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            foreach (var item in this)
            {
                item.Draw(canvas, dirtyRect);
            }
        }
    }
}
