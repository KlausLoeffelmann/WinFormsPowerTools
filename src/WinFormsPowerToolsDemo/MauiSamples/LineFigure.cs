using Microsoft.Maui.Graphics;

namespace WinFormsPowerToolsDemo.MauiGraphics
{
    public class LineFigure : IDrawable
    {
        public void Draw(ICanvas canvas, Microsoft.Maui.Graphics.RectangleF dirtyRect)
        {
            canvas.StrokeColor = Colors.Blue;
            canvas.DrawLine(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
        }
    }
}
