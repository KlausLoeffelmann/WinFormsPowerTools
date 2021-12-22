using System.Drawing;

namespace System.Windows.Forms.Documents
{
    public interface IDocumentItem
    {
        PointF Location { get; set; }
        SizeF Size { get; set; }
        void OnRender(PointF ScrollOffset, object DeviceContext);
    }
}
