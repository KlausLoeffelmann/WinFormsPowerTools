﻿using System.Windows.Forms;
using System.Windows.Forms.Documents;

namespace WinFormsPowerToolsDemo;

public partial class DocumentTestForm : Form
{
    public DocumentTestForm()
    {
        InitializeComponent();
        Document doc = new();
        documentControl1.MainDocument = doc;

        doc.SuspendUpdates();
        doc.ResumeUpdates();
    }

    private void button1_Click(object sender, System.EventArgs e)
    {
    }
}

//public class AsyncTestDocumentItem : AsyncDocumentItem
//{
//    protected override async Task OnRenderAsync(PointF scrollOffset, IDeviceContext deviceContext)
//    {
//        if (deviceContext is not Graphics graphics)
//        {
//            return;
//        }

//        using Matrix matrix = new();
//        matrix.Translate(-scrollOffset.X, -scrollOffset.Y);
//        graphics.Transform = matrix;

//        var documentSize = new SizeF(ParentDocument!.Width, ParentDocument!.Height);
//        for (int x = 0; x < 800; x += 10)
//        {
//            graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(x, 600));
//        }

//        await Task.FromResult(false);
//    }

//    protected override async Task VisibilityStateChangedAsync(VisibilityChangeState visibilityState)
//    {
//        await Task.FromResult(false);
//    }
//}
