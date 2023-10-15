using System.Diagnostics;

namespace System.Windows.Forms.TextLayout;

/// <summary>
/// Represents a text run within a paragraph, providing text content and formatting information.
/// </summary>
[DebuggerDisplay("{GetDebugDisplay(),nq}")]
public class Run : Inline
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Run"/> class.
    /// </summary>
    /// <param name="text">The text content of the run.</param>
    /// <param name="font">The font used to display the text.</param>
    /// <param name="parentParagraph">The parent paragraph containing this run. The default is null.</param>
    internal Run(
        string text,
        Font font,
        Paragraph? parentParagraph = null)
    {
        Text = text;
        Font = font;
        ParentParagraph = parentParagraph;
    }

    /// <summary>
    /// Gets the font used to display the text.
    /// </summary>
    public Font Font { get; }

    /// <summary>
    ///  Gets the parent paragraph containing this run.
    /// </summary>
    public Paragraph? ParentParagraph { get; }

    /// <summary>
    ///  Creates a new run with the same formatting but without text content.
    /// </summary>
    /// <returns>A new <see cref="Run"/> instance with empty text content.</returns>
    public Run CloneWithoutText()
    {
        return new Run(
            string.Empty,
            Font,
            ParentParagraph);
    }

    /// <summary>
    ///  Returns the string representation of the run when the text 
    ///  is <see langword="null"/> (<see langword="nothing"/> in VB).
    /// </summary>
    protected virtual string NullText
        => $"{{null}}";

    /// <summary>
    ///  Gets the string representation of the run.
    /// </summary>
    /// <returns>The string representation of the run.</returns>
    public override string ToString()
    {
        return Text ?? NullText;
    }

    internal Run WithText(string text) 
        => new(
            text,
            Font,
            ParentParagraph);
}
