namespace System.Windows.Forms.Documents;

internal interface IDocument
{     
    event EventHandler SizeChanged;
    SizeF Size { get; set; }
    IDocumentControl HostControl { get; set; }
}
