namespace System.Windows.Forms.Documents;

internal interface IDocument
{     
    SizeF Size { get; set; }
    IDocumentControl? HostControl { get; set; }
}
