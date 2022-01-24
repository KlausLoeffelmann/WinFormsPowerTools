using System.Drawing;

namespace System.Windows.Forms.D2D
{
    public interface ISupportBeginAndEndDraw
    {
        void BeginDraw();
        void Clear(Color color);
        void EndDraw();
    }
}
