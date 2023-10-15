using System.Windows.Forms.TextLayout;
using WinFormsPowerTools.TextLayout.TextLayout;

namespace System.Windows.Forms.Markdown;

public partial class MarkDownPanel
{
    /// <summary>
    ///  GdiPlusFactory is a factory class that creates GDI+ objects 
    ///  - setup in a way so it can be efficiently reused in the context of OnPaint.
    /// </summary>
    private class GdiPlusFactory : IDeviceContextLayoutFactory
    {
        private readonly GdiPlusTextLayoutEngine _defaultTextLayoutEngine;

        public GdiPlusFactory()
        {
            _defaultTextLayoutEngine = new GdiPlusTextLayoutEngine();
        }

        ITextLayoutEngine IDeviceContextLayoutFactory.GetDeviceTextLayoutEngine()
        {
            _defaultTextLayoutEngine.SetDeviceContext(
                ((IDeviceContextLayoutFactory) this).GetDeviceContext());

            return _defaultTextLayoutEngine;
        }

        IDeviceContext IDeviceContextLayoutFactory.GetDeviceContext()
            => Graphics 
            ?? throw new NullReferenceException(
                "GdiPlusFactory wasn't able to return a Graphics object as IDeviceContext.");

        public Graphics? Graphics { get; set; }
    }
}
