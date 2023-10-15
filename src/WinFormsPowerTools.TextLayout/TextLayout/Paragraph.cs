namespace System.Windows.Forms.TextLayout;

/// <summary>
/// Represents a paragraph of text.
/// </summary>
public class Paragraph : Inline
{
    private readonly Inlines _inlines;
    private readonly Font _font;
    private readonly StringAlignment _stringAlignment;

    public static readonly Distance DefaultParagraphWidth = new(600, Dimension.Pixel);

    /// <summary>
    ///  Creates a new paragraph.
    /// </summary>
    /// <param name="inlines">The Inlines contained within the paragraph.</param>
    /// <param name="font">The font used for the paragraph.</param>
    /// <param name="stringAlignment">The string alignment used for the paragraph.</param>
    /// <param name="rightIndention">The right indention used for the paragraph.</param>
    /// <param name="leftIndention">The left indention used for the paragraph.</param>
    /// <param name="firstLineIndent">The first line indent used for the paragraph.</param>
    /// <param name="hanging">The hanging used for the paragraph.</param>
    /// <param name="lineSpacing">The line spacing used for the paragraph.</param>
    /// <param name="before">The spacing before the paragraph.</param>
    /// <param name="after">The spacing after the paragraph.</param>
    public Paragraph(
        Inlines inlines,
        Font font,
        StringAlignment stringAlignment = StringAlignment.Near,
        Distance rightIndention = default,
        Distance leftIndention = default,
        Distance? firstLineIndent = default,
        Distance? hanging = default,
        LineSpacing lineSpacing = default,
        LineSpacing before = default,
        LineSpacing after = default) : base(string.Empty)
    {
        _inlines = inlines;
        _font = font;
        Width = DefaultParagraphWidth;
        _stringAlignment = stringAlignment;
        RightIndention = rightIndention;
        LeftIndention = leftIndention;
        FirstLineIndent = firstLineIndent;
        Hanging = hanging;
        LineSpacing = lineSpacing;
        Before = before;
        After = after;
    }


    /// <summary>
    ///  Creates a new paragraph.
    /// </summary>
    /// <param name="inlines">The inlines contained within the paragraph.</param>
    /// <param name="font">The font used for the paragraph.</param>
    /// <param name="stringAlignment">The string alignment used for the paragraph.</param>
    /// <param name="rightIndention">The right indention used for the paragraph.</param>
    /// <param name="leftIndention">The left indention used for the paragraph.</param>
    /// <param name="firstLineIndent">The first line indent used for the paragraph.</param>
    /// <param name="hanging">The hanging used for the paragraph.</param>
    /// <param name="lineSpacing">The line spacing used for the paragraph.</param>
    /// <param name="before">The spacing before the paragraph.</param>
    /// <param name="after">The spacing after the paragraph.</param>
    public Paragraph(
        Inlines inlines,
        Font font,
        Distance width,
        StringAlignment stringAlignment = StringAlignment.Near,
        Distance rightIndention = default,
        Distance leftIndention = default,
        Distance? firstLineIndent = default,
        Distance? hanging = default,
        LineSpacing lineSpacing = default,
        LineSpacing before = default,
        LineSpacing after = default) : base(string.Empty)
    {
        _inlines = inlines;
        _font = font;
        Width = width;
        _stringAlignment = stringAlignment;
        RightIndention = rightIndention;
        LeftIndention = leftIndention;
        FirstLineIndent = firstLineIndent;
        Hanging = hanging;
        LineSpacing = lineSpacing;
        Before = before;
        After = after;
    }

    /// <summary>
    ///  Gets the <see cref="Font"/> used for the paragraph.
    /// </summary>
    public Font Font => _font;

    /// <summary>
    ///  Gets the <see cref="StringAlignment"/> used for the paragraph.
    /// </summary>
    public StringAlignment StringAlignment => _stringAlignment;

    public Distance Width { get; }

    /// <summary>
    ///  Gets the <see cref="Inlines"/> contained within the paragraph.
    /// </summary>
    public Inlines Inlines => _inlines;

    /// <summary>
    /// Gets the right indention used for the paragraph.
    /// </summary>
    public Distance RightIndention { get; }

    /// <summary>
    /// Gets the left indention used for the paragraph.
    /// </summary>
    public Distance LeftIndention { get; }

    /// <summary>
    /// Gets the first line indent used for the paragraph.
    /// </summary>
    public Distance? FirstLineIndent { get; }

    /// <summary>
    /// Gets the hanging used for the paragraph.
    /// </summary>
    public Distance? Hanging { get; }

    /// <summary>
    /// Gets the line spacing used for the paragraph.
    /// </summary>
    public LineSpacing LineSpacing { get; }

    /// <summary>
    /// Gets the spacing before the paragraph.
    /// </summary>
    public LineSpacing Before { get; }

    /// <summary>
    /// Gets the spacing after the paragraph.
    /// </summary>
    public LineSpacing After { get; }
}
