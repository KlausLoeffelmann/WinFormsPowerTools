using System.Collections.Generic;

namespace System.Windows.Forms.Documents
{
    public class Document
    {
        public float Width { get; set; } = 800;
        public float Height { get; set; } = 600;
        public IEnumerable<IDocumentItem> DocumentItems { get; }
    }
}
