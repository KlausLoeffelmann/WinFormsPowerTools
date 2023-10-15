namespace System.Windows.Forms.TextLayout;

/// <summary>
///  Represents a run of glyphs in a text layout.
/// </summary>
internal class GlyphRun : Inline
{
    private readonly IDeviceContext _deviceContext;
    private readonly Inline _inline;

    /// <summary>
    ///  Initializes a new instance of the <see cref="GlyphRun"/> class.
    /// </summary>
    /// <param name="deviceContext">The device context.</param>
    /// <param name="inline">The inline.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="deviceContext"/> is <see langword="null"/>.
    /// -or-
    /// <paramref name="inline"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException"><paramref name="inline"/> is a <see cref="GlyphRun"/>.</exception>
    public GlyphRun(IDeviceContext deviceContext, Inline inline,PointF location=default, SizeF size=default)
        : base(inline?.Text)
    {
        if (deviceContext is null)
            throw new ArgumentNullException(nameof(deviceContext));

        if (inline is null)
            throw new ArgumentNullException(nameof(inline));

        if (inline is GlyphRun)
            throw new ArgumentException("GlyphRun cannot contain another GlyphRun.", nameof(inline));

        _deviceContext = deviceContext;
        _inline = inline;
        Location = location;
        Size = size;
    }

    /// <summary>
    ///  Gets the device context.
    /// </summary>
    /// <value>The device context.</value>
    public IDeviceContext DeviceContext => _deviceContext;

    /// <inheritdoc/>
    public override string ToString()
    {
        return _inline.ToString();
    }

    public PointF Location { get; private set; }
    public SizeF Size { get; private set; }
}
