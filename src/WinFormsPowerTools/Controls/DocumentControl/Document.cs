using System.Collections.Generic;

#nullable enable

namespace System.Windows.Forms.Documents
{
    public class Document
    {
        private List<AsyncDocumentItem>? _documentItems;
        private bool _suspendUpdates;

        public float Width { get; set; } = 800;
        public float Height { get; set; } = 600;

        internal DocumentControl? HostControl { get; set; }

        internal List<AsyncDocumentItem> DocumentItems
            => _documentItems ??= new List<AsyncDocumentItem>();

        public void SuspendUpdates()
        {
            if (_suspendUpdates)
            {
                throw new InvalidOperationException("Updates are already suspended!");
            }

            _suspendUpdates = true;
        }

        public void ResumeUpdates()
        {
            if (!_suspendUpdates)
            {
                throw new InvalidOperationException("Updates are not suspended!");
            }

            _suspendUpdates = false;
            HostControl?.Invalidate();
        }

        public void AddDocumentItem(AsyncDocumentItem documentItem)
        {
            documentItem.SetParentDocument(this);
            DocumentItems.Add(documentItem);
        }
    }
}
