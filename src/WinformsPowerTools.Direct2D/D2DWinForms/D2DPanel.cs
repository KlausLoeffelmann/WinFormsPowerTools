using Windows.Win32;
using Windows.Win32.Graphics.Direct2D.Common;

namespace System.Windows.Forms.D2D
{
    public partial class D2DPanel : Control
    {
        private IGraphics _graphics;

        public D2DPanel()
        {
            ResizeRedraw = true;
            _graphics = new D2DGraphics(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        unsafe protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!IsHandleCreated || IsAncestorSiteInDesignMode)

            {
                return;
            }

            ((ISupportBeginAndEndDraw)_graphics).BeginDraw();
            OnIGraphicsPaint(_graphics);
            ((ISupportBeginAndEndDraw)_graphics).EndDraw();
        }

        protected virtual void OnIGraphicsPaint(IGraphics graphics)
        {
        }
    }
}
