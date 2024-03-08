namespace System.Windows.Forms.Documents;

public struct PaddingF
{
    public PaddingF(float all)
    {
        Left = all;
        Top = all;
        Right = all;
        Bottom = all;
    }

    public PaddingF(float horizontal, float vertical)
    {
        Left = horizontal;
        Top = vertical;
        Right = horizontal;
        Bottom = vertical;
    }

    public PaddingF(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }
}
