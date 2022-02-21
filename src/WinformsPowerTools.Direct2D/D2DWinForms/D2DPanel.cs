using Windows.Win32;
using Windows.Win32.Graphics.Direct2D.Common;

namespace System.Windows.Forms.D2D
{
    public partial class D2DPanel : Control
    {
        public event EventHandler<PaintIGraphicsEventArgs>? PaintIGraphics;
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

            ((ISupportsBeginAndEndDraw)_graphics).BeginDraw();
            OnPaintIGraphics(_graphics);
            ((ISupportsBeginAndEndDraw)_graphics).EndDraw();
        }

        protected virtual void OnPaintIGraphics(IGraphics graphics) 
            => PaintIGraphics?.Invoke(this, new PaintIGraphicsEventArgs(graphics));
    }
}
