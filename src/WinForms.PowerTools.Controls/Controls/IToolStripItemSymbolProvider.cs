using System.Diagnostics.CodeAnalysis;

namespace WinForms.PowerTools.Controls;

internal interface IToolStripItemSymbolProvider
{
    /// <summary>
    ///  Occurs when the <see cref="Symbol"/> property value changes.
    /// </summary>
    event EventHandler? SymbolChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolColor"/> property value changes.
    /// </summary>
    event EventHandler? SymbolColorChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolSize"/> property value changes.
    /// </summary>
    event EventHandler? SymbolSizeChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolOffset"/> property value changes.
    /// </summary>
    event EventHandler? SymbolOffsetChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolScaling"/> property value changes.
    /// </summary>
    event EventHandler? SymbolScalingChanged;

    private static readonly Color s_transparentColor = Color.Transparent;

    /// <summary>
    ///  Gets or sets the symbol character.
    /// </summary>
    SegoeFluentIcons? Symbol { get; set; }

    /// <inheritdoc/>
    Image? Image { get; set; }

    /// <summary>
    ///  Gets or sets the symbol scaling in percent (100 = no scaling.)
    /// </summary>
    int SymbolScaling { get; set; }

    /// <summary>
    ///  Gets or sets the symbol color.
    /// </summary>
    Color SymbolColor { get; set; }

    /// <summary>
    ///  Gets or sets the symbol size.
    /// </summary>
    Size? SymbolSize { get; set; }

    /// <summary>
    ///  Gets or sets the symbol offset.
    /// </summary>
    Size SymbolOffset { get; set; }

    [AllowNull]
    protected SymbolSource<SegoeFluentIcons> SymbolSource { get; set; }
    protected SymbolImageFactory? SymbolImageFactory { get; set; }

    protected static SymbolSource<SegoeFluentIcons> SymbolSourceGetter(IToolStripItemSymbolProvider provider)
        => provider.SymbolSource;

    protected static SymbolSource<SegoeFluentIcons> SymbolSourceSetter(
        IToolStripItemSymbolProvider provider,
        SymbolSource<SegoeFluentIcons> symbolSource)
            => provider.SymbolSource = symbolSource;

    protected static void SymbolSetter(
        IToolStripItemSymbolProvider provider,
        Action<EventArgs> onSymbolChanged,
        SegoeFluentIcons? value)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(onSymbolChanged);

        SegoeFluentIcons? symbol = provider.SymbolSource.HasSymbolValue
            ? provider.SymbolSource.Symbol
            : null;

        if (symbol == value)
        {
            return;
        }

        if (!value.HasValue)
        {
            provider.SymbolSource.SetSymbolNull();
            UpdateSymbolImageFactory(provider);
            onSymbolChanged(EventArgs.Empty);
            return;
        }

        provider.SymbolSource.Symbol = value.Value;
        UpdateSymbolImageFactory(provider);
        onSymbolChanged(EventArgs.Empty);
    }

    protected static void SymbolScalingSetter(
        IToolStripItemSymbolProvider provider,
        Action<EventArgs> onSymbolScalingChanged,
        int value,
        ref int symbolScaling)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(onSymbolScalingChanged);

        if (symbolScaling == value)
        {
            return;
        }

        if (value < 25 || value > 300)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 25 and 300.");
        }

        symbolScaling = value;
        UpdateSymbolImageFactory(provider);
        onSymbolScalingChanged(EventArgs.Empty);
    }

    protected static ToolStrip? ParentToolStrip(ToolStripItem item)
    {
        if (item.Owner is ToolStrip menu)
            return menu;

        if (item.OwnerItem is ToolStripMenuItem ownerItem)
            return ParentToolStrip(ownerItem);

        return null;
    }

    protected static Color GetSymbolColor(Color? currentColor, ToolStrip? root = default)
    {
        if (root is not null && !currentColor.HasValue)
        {
            return root.ForeColor;
        }

        return currentColor ?? Color.Black;
    }

    protected static void SymbolColorSetter(
        IToolStripItemSymbolProvider provider,
        Action<EventArgs> onSymbolColorChanged,
        Color value,
        ref Color? symbolColor,
        ToolStrip? root = default)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(onSymbolColorChanged);

        if (symbolColor == value)
        {
            return;
        }

        if (root is not null && value == root.ForeColor)
        {
            symbolColor = null;
            UpdateSymbolImageFactory(provider);
            onSymbolColorChanged(EventArgs.Empty);
            return;
        }

        symbolColor = value;
        UpdateSymbolImageFactory(provider);
        onSymbolColorChanged(EventArgs.Empty);
    }

    protected static void SymbolSizeSetter(
        IToolStripItemSymbolProvider provider,
        Action<EventArgs> onSymbolSizeChanged,
        Size? value,
        ref Size? size)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(onSymbolSizeChanged);

        if (size == value)
        {
            return;
        }

        size = value;
        UpdateSymbolImageFactory(provider);
        onSymbolSizeChanged(EventArgs.Empty);
    }

    protected static void SymbolOffsetSetter(
        IToolStripItemSymbolProvider provider,
        Action<EventArgs> onSymbolOffsetChanged,
        Size value,
        ref Size symbolOffset)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(onSymbolOffsetChanged);

        if (symbolOffset == value)
        {
            return;
        }

        symbolOffset = value;
        UpdateSymbolImageFactory(provider);
        onSymbolOffsetChanged(EventArgs.Empty);
    }

    protected static void UpdateSymbolImageFactory(IToolStripItemSymbolProvider provider)
    {
        if (!provider.SymbolSource.HasSymbolValue || !(provider.SymbolSize.HasValue))
        {
            provider.SymbolImageFactory = null;
            provider.Image = null!;
            return;
        }

        provider.SymbolImageFactory = new SymbolImageFactory(
            (char)provider.SymbolSource.Symbol,
            provider.SymbolSource.FontName,
            provider.SymbolSize.Value.Width,
            provider.SymbolSize.Value.Height,
            provider.SymbolScaling,
            provider.SymbolColor,
            s_transparentColor,
            provider.SymbolOffset.Width,
            provider.SymbolOffset.Height);

        provider.Image = provider.SymbolImageFactory.SymbolImage;
    }
}
