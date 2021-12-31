using SkiaSharp;
using System.Drawing;

namespace WinFormsPowerToolsDemo.SkiaSharpDemo
{
    internal abstract class Shape
    {
        private SizeF _canvasSize;

        public abstract void Trigger();
        public abstract void OnGdiplusRender(Graphics g);
        public abstract void OnSkiaRender(SKSurface surface);
        public virtual void CanvasSizeChanged(SizeF canvasSize)
            => _canvasSize = canvasSize;
        public SizeF CanvasSize => _canvasSize;
    }
}
