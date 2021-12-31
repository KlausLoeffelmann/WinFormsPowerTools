using System.Windows.Forms;

namespace WinFormsPowerToolsDemo
{
    public partial class SkiaSharpDemoForm
    {
        internal class GdiPlusRenderTargetPanel : Panel
        {
            public GdiPlusRenderTargetPanel() : base()
            {
                DoubleBuffered = true;
            }
        }
    }
}
