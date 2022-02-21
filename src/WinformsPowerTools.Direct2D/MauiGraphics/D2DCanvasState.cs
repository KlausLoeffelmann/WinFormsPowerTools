using System.Windows.Forms;
using System.Windows.Forms.Direct2D;

namespace Microsoft.Maui.Graphics.D2D
{
    public class D2DCanvasState : CanvasState
    {
        private Direct2DLayer _d2DLayer;

        public D2DCanvasState(Control? window)
        {
            _d2DLayer = new Direct2DLayer(window);
        }

        public D2DCanvasState(D2DCanvasState prototype) : base(prototype)
        {
            _d2DLayer = new Direct2DLayer(prototype._d2DLayer.Window);
        }

        internal Direct2DLayer CurrentStateLayer => _d2DLayer;
    }
}
