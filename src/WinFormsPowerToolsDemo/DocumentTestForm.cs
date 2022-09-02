using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Documents;

namespace WinFormsPowerToolsDemo
{
    public partial class DocumentTestForm : Form
    {
        public DocumentTestForm()
        {
            InitializeComponent();
            Document doc = new();
            documentControl1.MainDocument = doc;

            doc.SuspendUpdates();
            doc.AddDocumentItem(new TestDocumentItem());
            doc.ResumeUpdates();
        }
    }

    public class TestDocumentItem : GraphicsDocumentItem
    {
        protected override void OnRender(PointF scrollOffset, Graphics graphics)
        {
            using Matrix matrix = new();
            matrix.Translate(-scrollOffset.X, -scrollOffset.Y);
            graphics.Transform = matrix;

            var documentSize = new SizeF(ParentDocument!.Width, ParentDocument!.Height);
            for (int x = 0; x < 800; x += 10)
            {
                graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(x, 600));
            }
        }
    }
}
