using System.Windows.Forms;

namespace Microsoft.Maui.Graphics.D2D
{
    public class D2DCanvasState : CanvasState
    {
        private D2DLayer _d2DLayer;

        public D2DCanvasState(Control? window)
        {
            _d2DLayer = new D2DLayer(window);
        }

        public D2DCanvasState(D2DCanvasState prototype) : base(prototype)
        {
            _d2DLayer = new D2DLayer(prototype._d2DLayer.Window);
        }

        internal D2DLayer CurrentStateLayer => _d2DLayer;
    }
}
