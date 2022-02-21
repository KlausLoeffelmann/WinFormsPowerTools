using System.Drawing;

namespace System.Windows.Forms.D2D
{
    public interface ISupportsBeginAndEndDraw
    {
        void BeginDraw();
        void Clear(Color color);
        void EndDraw();
    }
}
