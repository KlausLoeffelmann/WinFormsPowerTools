using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace WinForms.PowerTools.Controls;

/// <summary>
///  Represents a menu item with a symbol.
/// </summary>
[ToolboxBitmap(typeof(ToolStripMenuItem))]
public class ToolStripSymbolDropDownButton : ToolStripDropDownButton, IToolStripItemSymbolProvider
{
    private Color DefaultSymbolColor = SystemColors.ControlDarkDark;

    private Color? _symbolColor = default;
    private static readonly Color s_transparentColor = Color.Transparent;

    private Size? _symbolSize = new Size(48, 48);
    private Size _symbolOffset;
    private int _symbolScaling = 100;

    public ToolStripSymbolDropDownButton() : base()
    {
        AutoSize = false;
        ImageScaling = ToolStripItemImageScaling.None;
        TextImageRelation = TextImageRelation.ImageAboveText;

        IToolStripItemSymbolProvider.SymbolSourceSetter(
            this,
            new SymbolSource<SegoeFluentIcons>("Segoe Fluent Icons"));

        DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
    }

    /// <inheritdoc/>
    public event EventHandler? SymbolChanged;

    /// <inheritdoc/>
    public event EventHandler? SymbolColorChanged;

    /// <inheritdoc/>
    public event EventHandler? SymbolSizeChanged;

    /// <inheritdoc/>
    public event EventHandler? SymbolOffsetChanged;

    /// <inheritdoc/>
    public event EventHandler? SymbolScalingChanged;

    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool AutoSize { get; set; } = false;

    /// <inheritdoc/>
    public new ToolStripItemImageScaling ImageScaling
    {
        get => base.ImageScaling;
        set => base.ImageScaling = value;
    }

    private bool ShouldSerializeImageScaling() => !(ImageScaling == ToolStripItemImageScaling.None);

    private void ResetImageScaling() => ImageScaling = ToolStripItemImageScaling.None;

    /// <inheritdoc/>
    public new TextImageRelation TextImageRelation
    {
        get => base.TextImageRelation;
        set => base.TextImageRelation = value;
    }

    private bool ShouldSerializeTextImageRelation() => !(TextImageRelation == TextImageRelation.ImageAboveText);

    private void ResetTextImageRelation() => TextImageRelation = TextImageRelation.ImageAboveText;


    [AllowNull]
    SymbolSource<SegoeFluentIcons> IToolStripItemSymbolProvider.SymbolSource { get; set; }

    SymbolImageFactory? IToolStripItemSymbolProvider.SymbolImageFactory { get; set; }

    private SymbolSource<SegoeFluentIcons> SymbolSource
        => IToolStripItemSymbolProvider.SymbolSourceGetter(this);

    /// <inheritdoc/>
    [Bindable(BindableSupport.Default, BindingDirection.OneWay)]
    public SegoeFluentIcons? Symbol
    {
        get => SymbolSource.HasSymbolValue
            ? SymbolSource.Symbol
            : null;

        set
        {
            IToolStripItemSymbolProvider.SymbolSetter(
            provider: this,
            onSymbolChanged: OnSymbolChanged,
            value: value);
        }
    }

    private bool ShouldSerializeSymbol()
        => SymbolSource.HasSymbolValue;

    private void ResetSymbol() => Symbol = null;

    /// <inheritdoc/>
    [DefaultValue(100)]
    public int SymbolScaling
    {
        get => _symbolScaling;
        set => IToolStripItemSymbolProvider.SymbolScalingSetter(
            provider: this,
            onSymbolScalingChanged: OnSymbolScalingChanged,
            value: value,
            ref _symbolScaling);
    }

    /// <inheritdoc/>
    public Color SymbolColor
    {
        get => IToolStripItemSymbolProvider.GetSymbolColor(_symbolColor, this.Owner);

        set => IToolStripItemSymbolProvider.SymbolColorSetter(
            provider: this,
            onSymbolColorChanged: OnSymbolColorChanged,
            value: value,
            ref _symbolColor,
            IToolStripItemSymbolProvider.ParentToolStrip(this));
    }

    private bool ShouldSerializeSymbolColor() => false;

    private void ResetSymbolColor() => SymbolColor = DefaultSymbolColor;

    /// <inheritdoc/>
    public Size? SymbolSize
    {
        get => _symbolSize;

        set => IToolStripItemSymbolProvider.SymbolSizeSetter(
            provider: this,
            onSymbolSizeChanged: OnSymbolSizeChanged,
            value: value,
            ref _symbolSize);
    }

    private bool ShouldSerializeSymbolSize() => _symbolSize.HasValue;

    private void ResetSymbolSize() => SymbolSize = null;

    protected override void OnOwnerChanged(EventArgs e)
    {
        base.OnOwnerChanged(e);
        SymbolColor = SymbolColor;
    }

    /// <inheritdoc/>
    public Size SymbolOffset
    {
        get => _symbolOffset;

        set => IToolStripItemSymbolProvider.SymbolOffsetSetter(
            provider: this,
            onSymbolOffsetChanged: OnSymbolOffsetChanged,
            value: value,
            ref _symbolOffset);
    }

    private bool ShouldSerializeSymbolOffset()
        => _symbolOffset != default;

    private void ResetSymbolOffset()
        => SymbolOffset = default;

    /// <inheritedDoc/>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Image? Image
    {
        get => base.Image;
        set => base.Image = value;
    }

    /// <summary>
    ///  Raises the <see cref="SymbolSizeChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolSizeChanged(EventArgs e)
        => SymbolSizeChanged?.Invoke(this, e);

    /// <summary>
    ///  Raises the <see cref="SymbolChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolChanged(EventArgs e)
        => SymbolChanged?.Invoke(this, e);

    /// <summary>
    ///  Raises the <see cref="SymbolColorChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolColorChanged(EventArgs e)
        => SymbolColorChanged?.Invoke(this, e);

    /// <summary>
    ///  Raises the <see cref="SymbolOffsetChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolOffsetChanged(EventArgs e)
        => SymbolOffsetChanged?.Invoke(this, e);

    /// <summary>
    ///  Raises the <see cref="SymbolScalingChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolScalingChanged(EventArgs e)
        => SymbolScalingChanged?.Invoke(this, e);
}
