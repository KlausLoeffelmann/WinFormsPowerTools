namespace System.Windows.Forms.Documents;

internal interface IDocument
{
    event EventHandler SizeChanged;

    event EventHandler DisplayBoundsChanged;

    SizeF Size { get; set; }

    /// <summary>
    ///  Gets the display bounds of the document which reflects the transformed 
    ///  location by the scroll offset and the size of the host control.
    /// </summary>
    /// <value>
    ///  The display bounds of the document.
    /// </value>
    RectangleF DisplayBounds { get; }

    /// <summary>
    ///  Reference to the host control of the document.
    /// </summary>
    /// <value>
    ///  The host control of the document.
    /// </value>
    IDocumentControl HostControl { get; set; }
}
