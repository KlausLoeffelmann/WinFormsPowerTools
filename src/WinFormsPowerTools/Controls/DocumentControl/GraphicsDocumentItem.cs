using System.Drawing;

namespace System.Windows.Forms.Documents
{
    public abstract class GraphicsDocumentItem : IDocumentItem
    {
        public PointF Location { get; set; }
        public SizeF Size { get; set; }

        void IDocumentItem.OnRender(PointF ScrollOffset, object DeviceContext)
            => OnRender(ScrollOffset, (Graphics)DeviceContext);

        internal protected abstract void OnRender(PointF scrollOffset, Graphics DeviceContext);
    }
}
