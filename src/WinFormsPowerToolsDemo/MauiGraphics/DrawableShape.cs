using Microsoft.Maui.Graphics;

namespace WinFormsPowerToolsDemo.MauiGraphics
{
    internal abstract class DrawableShape : IDrawable
    {
        private SizeF _canvasSize;

        public abstract void Trigger();
        public virtual void CanvasSizeChanged(SizeF canvasSize)
            => _canvasSize = canvasSize;

        public abstract void Draw(ICanvas canvas, Microsoft.Maui.Graphics.RectangleF dirtyRect);

        public SizeF CanvasSize => _canvasSize;
    }
}
