namespace WinFormsPowerTools.AutoLayout
{
    public record struct AutoLayoutSize
    {
        public AutoLayoutSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsZeroOrSmaller
             => Width <= 0 || Height <= 0;

        public int Width { get; }
        public int Height { get; }
    }
}
