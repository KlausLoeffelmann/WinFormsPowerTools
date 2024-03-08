namespace System.Windows.Forms.Documents;

internal interface IDocumentControl
{
    IntPtr Handle { get; }
    Rectangle ClientRectangle { get; }
    Rectangle DisplayRectangle { get; }
    void SetDisplayFromScrollProps(int x, int y);
    bool IsHandleCreated { get; }
    void Invalidate();
}
