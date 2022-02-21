namespace System.Windows.Forms.Direct2D
{
    public partial class Direct2DPanel : Control
    {
        public event EventHandler<GraphicsPaintEventArgs>? PaintIGraphics;
        private IGraphics _graphics;

        public Direct2DPanel()
        {
            ResizeRedraw = true;

            // We can get the IGraphics (which covers the D2D Graphics in this case)
            // easily by the Extension method.
            _graphics = this.GetDirect2Graphics();
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
            => PaintIGraphics?.Invoke(this, new GraphicsPaintEventArgs(graphics));
    }
}
