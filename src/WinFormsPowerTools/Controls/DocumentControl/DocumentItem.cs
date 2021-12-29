using System.Drawing;

#nullable enable

namespace System.Windows.Forms.Documents
{
    public abstract class DocumentItem
    {
        private Document? _parentDocument;

        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public Document? ParentDocument => _parentDocument;

        internal void SetParentDocument(Document parentDocument)
        {
            if (parentDocument is null)
            {
                throw new ArgumentNullException(nameof(parentDocument));
            }

            _parentDocument = parentDocument;
        }

        internal protected abstract void OnRender(PointF ScrollOffset, object DeviceContext);
    }
}
