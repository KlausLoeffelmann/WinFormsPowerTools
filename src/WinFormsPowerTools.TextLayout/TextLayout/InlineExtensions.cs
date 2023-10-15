namespace System.Windows.Forms.TextLayout;

public static class InlineExtensions
{
    /// <summary>
    ///  Converts an IEnumerable of Inline objects to Inlines object.
    /// </summary>
    /// <param name="inlines">The IEnumerable of Inline objects to convert.</param>
    /// <returns>The Inlines object.</returns>
    public static Inlines ToInlines(this IEnumerable<Inline> inlines)
        => new(inlines);

    /// <summary>
    ///  Converts an IEnumerable of Run objects to Inlines object.
    /// </summary>
    /// <param name="runs">The IEnumerable of Run objects to convert.</param>
    /// <returns>The Inlines object.</returns>
    public static Inlines ToInlines(this IEnumerable<Run> runs)
        => new(runs);
}
