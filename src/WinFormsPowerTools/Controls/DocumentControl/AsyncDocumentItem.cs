using System.Drawing;
using System.Threading.Tasks;

#nullable enable

namespace System.Windows.Forms.Documents
{
    public abstract class AsyncDocumentItem
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

        protected abstract Task VisibilityStateChangedAsync(VisibilityState visibilityState);

        internal abstract protected Task OnRenderAsync(PointF ScrollOffset, IDeviceContext deviceContext);
    }
}
