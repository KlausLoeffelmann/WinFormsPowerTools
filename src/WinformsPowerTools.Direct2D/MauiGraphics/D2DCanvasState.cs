using System.Windows.Forms;

namespace Microsoft.Maui.Graphics.D2D
{
    public class D2DCanvasState : CanvasState
    {
        private D2DLayer _d2DLayer;

        public D2DCanvasState(IWin32Window? window)
        {
            _d2DLayer = new D2DLayer(window);
        }

        public D2DCanvasState(D2DCanvasState prototype) : base(prototype)
        {
            _d2DLayer = new D2DLayer(prototype._d2DLayer.Window);
        }

        internal void BeginDraw()
        {
            _d2DLayer.BeginDraw();
        }

        internal void EndDraw()
        {
            _d2DLayer.EndDraw();
        }

        internal void DrawLine(float x1, float y1, float x2, float y2)
        {
            _d2DLayer.DrawLine(x1, y1, x2, y2);
        }

        internal Color StrokeColor
        {
            get => _d2DLayer.StrokeColor;
            set => _d2DLayer.StrokeColor = value;
        }
    }
}
