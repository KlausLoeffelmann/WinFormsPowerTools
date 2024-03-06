using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Documents;
using WinForms.PowerTools.Controls;

namespace WinFormsPowerToolsDemo;

public partial class DocumentTestForm : Form
{
    public DocumentTestForm()
    {
        InitializeComponent();
        Document doc = new();
        documentControl1.MainDocument = doc;

        doc.SuspendUpdates();
        doc.AddDocumentItem(new AsyncTestDocumentItem());
        doc.ResumeUpdates();
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
        label1.Text = label1.AsyncInvoke<string>(
            async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
            }

            throw new System.Exception("Test exception");

            // return "Done!";
        });
    }
}

public class AsyncTestDocumentItem : AsyncDocumentItem
{
    protected override async Task OnRenderAsync(PointF scrollOffset, IDeviceContext deviceContext)
    {
        if (deviceContext is not Graphics graphics)
        {
            return;
        }

        using Matrix matrix = new();
        matrix.Translate(-scrollOffset.X, -scrollOffset.Y);
        graphics.Transform = matrix;

        var documentSize = new SizeF(ParentDocument!.Width, ParentDocument!.Height);
        for (int x = 0; x < 800; x += 10)
        {
            graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(x, 600));
        }

        await Task.FromResult(false);
    }

    protected override async Task VisibilityStateChangedAsync(VisibilityState visibilityState)
    {
        await Task.FromResult(false);
    }
}
