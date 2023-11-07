using System.ComponentModel;

namespace WinForms.PowerTools.Controls;

/// <summary>
///  Represents a menu item with a symbol.
/// </summary>
[ToolboxBitmap(typeof(ToolStripMenuItem))]
public class ToolStripSymbolMenuItem : ToolStripMenuItem
{
    private Color _symbolColor=Color.Black;
    private readonly Color _transparentColor = Color.Transparent;
    private Size? _symbolSize = new Size(32, 32);
    private Size _symbolOffset;
    private int _scalePercentage = 100;
    private readonly SymbolSource<SegoeFluentIcons> _symbolSource;

    public ToolStripSymbolMenuItem() : base()
    {
        base.ImageScaling = ToolStripItemImageScaling.None;
        base.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
        base.TextImageRelation = TextImageRelation.ImageAboveText;
        _symbolSource = new SymbolSource<SegoeFluentIcons>("Segoe Fluent Icons");
    }

    /// <summary>
    ///  Occurs when the <see cref="Symbol"/> property value changes.
    /// </summary>
    public event EventHandler? SymbolChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolColor"/> property value changes.
    /// </summary>
    public event EventHandler? SymbolColorChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolSize"/> property value changes.
    /// </summary>
    public event EventHandler? SymbolSizeChanged;

    /// <summary>
    ///  Occurs when the <see cref="SymbolOffset"/> property value changes.
    /// </summary>
    public event EventHandler? SymbolOffsetChanged;

    public event EventHandler? ScalePercentageChanged;

    private SymbolImageFactory? _symbolImageFactory;

    /// <inheritdoc/>
    public new ToolStripItemImageScaling ImageScaling
    {
        get => base.ImageScaling;
        set => base.ImageScaling = value;
    }

    private bool ShouldSerializeImageScaling() => !(ImageScaling == ToolStripItemImageScaling.None);

    private void ResetImageScaling() => ImageScaling = ToolStripItemImageScaling.None;

    private bool ShouldSerializeTextImageRelation() => !(TextImageRelation == TextImageRelation.ImageAboveText);

    private void ResetTextImageRelation() => TextImageRelation = TextImageRelation.ImageAboveText;

    public int ScalePercentage
    {
        get => _scalePercentage;
        set
        {
            if (_scalePercentage == value)
            {
                return;
            }

            if (value < 25 || value > 300)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be between 25 and 300.");
            }

            _scalePercentage = value;
            OnScalePercentageChanged(EventArgs.Empty);
        }
    }

    /// <summary>
    ///  Gets or sets the symbol character.
    /// </summary>
    [Bindable(BindableSupport.Default, BindingDirection.OneWay)]
    public SegoeFluentIcons? Symbol
    {
        get => _symbolSource.Symbol;

        set
        {
            if (_symbolSource.Symbol == value)
            {
                return;
            }

            if (!value.HasValue)
            {
                _symbolSource.SetSymbolNull();
                _symbolImageFactory = null;
                OnSymbolChanged(EventArgs.Empty);
                return;
            }

            _symbolSource.Symbol = value.Value;
            OnSymbolChanged(EventArgs.Empty);
        }
    }

    private bool ShouldSerializeSymbol() 
        => _symbolSource.HasSymbolValue;

    private void ResetSymbol() => Symbol = null;

    /// <summary>
    ///  Gets or sets the color of the symbol.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///  Thrown when <see cref="Symbol"/> is <see langword="null"/>.
    /// </exception>
    public Color SymbolColor
    {
        get => _symbolColor;

        set
        {
            if (_symbolColor == value)
            {
                return;
            }

            _symbolColor = value;
            OnSymbolColorChanged(EventArgs.Empty);
        }
    }

    private bool ShouldSerializeSymbolColor() => _symbolColor != Color.Black;

    private void ResetSymbolColor() => SymbolColor = Color.Black;

    /// <summary>
    ///  Gets or sets the size of the symbol.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///  Thrown when <see cref="Symbol"/> is <see langword="null"/>.
    /// </exception>
    public Size? SymbolSize
    {
        get => _symbolSize;

        set
        {
            if (_symbolSize == value)
            {
                return;
            }

            _symbolSize = value;
            OnSymbolSizeChanged(EventArgs.Empty);
        }
    }

    private bool ShouldSerializeSymbolSize() => _symbolSize.HasValue;

    private void ResetSymbolSize() => SymbolSize = null;

    /// <summary>
    ///  Gets or sets the offset of the symbol.
    /// </summary>
    public Size SymbolOffset
    {
        get => _symbolOffset;

        set
        {
            if (_symbolOffset == value)
            {
                return;
            }

            _symbolOffset = value;
            OnSymbolOffsetChanged(EventArgs.Empty);
        }
    }

    private bool ShouldSerializeSymbolOffset() => _symbolOffset != default;

    private void ResetSymbolOffset() => SymbolOffset = default;

    /// <inheritedDoc/>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Image? Image
    {
        get => base.Image;
        set=> base.Image = value;
    }

    /// <summary>
    ///  Raises the <see cref="SymbolSizeChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolSizeChanged(EventArgs e)
    {
        UpdateSymbolImageFactory();
        SymbolSizeChanged?.Invoke(this, e);
    }

    /// <summary>
    ///  Raises the <see cref="SymbolChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolChanged(EventArgs e)
    {
        UpdateSymbolImageFactory();
        SymbolChanged?.Invoke(this, e);
    }

    /// <summary>
    ///  Raises the <see cref="SymbolColorChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
    protected virtual void OnSymbolColorChanged(EventArgs e)
    {
        UpdateSymbolImageFactory();
        SymbolColorChanged?.Invoke(this, e);
    }

    protected virtual void OnSymbolOffsetChanged(EventArgs e)
    {
        UpdateSymbolImageFactory();
        SymbolOffsetChanged?.Invoke(this, e);
    }

    protected virtual void OnScalePercentageChanged(EventArgs e)
    {
        UpdateSymbolImageFactory();
        ScalePercentageChanged?.Invoke(this, e);
    }

    private void UpdateSymbolImageFactory()
    {
        if (!(_symbolSource.HasSymbolValue) || !(_symbolSize.HasValue))
        {
            _symbolImageFactory = null;
            Image = null!;
            return;
        }

        _symbolImageFactory = new SymbolImageFactory(
            (char) _symbolSource.Symbol,
            _symbolSource.FontName,
            _symbolSize.Value.Width,
            _symbolSize.Value.Height,
            _scalePercentage,
            _symbolColor,
            _transparentColor,
            _symbolOffset.Width,
            _symbolOffset.Height);

        Image = _symbolImageFactory.SymbolImage;
    }
}
