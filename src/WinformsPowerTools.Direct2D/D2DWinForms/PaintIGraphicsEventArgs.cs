namespace System.Windows.Forms.D2D
{
    public class PaintIGraphicsEventArgs : EventArgs
    {
        public PaintIGraphicsEventArgs(IGraphics graphics)
            => Graphics = graphics;

        public IGraphics Graphics { get; }
    }
}
