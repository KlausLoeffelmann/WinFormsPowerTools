using System.ComponentModel;
using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public partial class GridView 
    : DocumentControl<GridViewDocument, GridViewItem>, ICollection<GridViewItem>
{
    private Type? _viewTemplateType;
    private Orientation _orientation;
    private int _minimumAdjacentItemsCount;
    private int _borderWidth = 1;
    private Color _borderColor;
    private WindowsFormsSynchronizationContext _syncContext;

#if DEBUG
    private readonly bool _drawPadding = true;
    private readonly Color _paddingColor = Color.Red;
#endif

    public GridView()
    {
        _orientation = Orientation.Horizontal;
        _minimumAdjacentItemsCount = 1;
        _borderColor = ForeColor;
        _syncContext = (WindowsFormsSynchronizationContext)(SynchronizationContext.Current
            ?? throw new NullReferenceException("Could not retrieve a synchronization context when creating a GridView item."));

        MainDocument = new GridViewDocument(this);
    }

    public Type? ViewTemplate
    {
        get => _viewTemplateType;

        set
        {
            if (_viewTemplateType == value)
            {
                return;
            }

            _viewTemplateType = value;
            OnViewTemplateChanged();
            Invalidate();
        }
    }

    public Orientation Orientation
    {
        get => _orientation;
        set
        {
            if (_orientation == value)
            {
                return;
            }

            _orientation = value;
            OnOrientationChanged();
            Invalidate();
        }
    }

    public int MinimumAdjacentItemsCount
    {
        get => _minimumAdjacentItemsCount;

        set
        {
            if (_minimumAdjacentItemsCount == value)
            {
                return;
            }

            _minimumAdjacentItemsCount = value;
            OnAdjacentItemsCountChanged();
            Invalidate();
        }
    }

    [DefaultValue(1)]
    public int BorderWidth
    {
        get => _borderWidth;
        set
        {
            if (_borderWidth == value)
            {
                return;
            }

            _borderWidth = value;
            Invalidate();
        }
    }

    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            if (_borderColor == value)
            {
                return;
            }

            _borderColor = value;
            Invalidate();
        }
    }

    int ICollection<GridViewItem>.Count => throw new NotImplementedException();

    bool ICollection<GridViewItem>.IsReadOnly => throw new NotImplementedException();

    private bool ShouldSerializeBorderColor() => _borderColor != ForeColor;

    private void ResetBorderColor() => BorderColor = ForeColor;

    private void OnAdjacentItemsCountChanged()
    {
        PerformLayout();
    }

    private void OnOrientationChanged()
    {
        PerformLayout();
    }

    private void OnViewTemplateChanged()
    {
        PerformLayout();
    }

    protected override void OnPaddingChanged(EventArgs e)
    {
        base.OnPaddingChanged(e);
        PerformLayout();
    }

    protected override void OnClientSizeChanged(EventArgs e)
    {
        base.OnClientSizeChanged(e);
        MainDocument!.Size= ClientSize;
        PerformLayout();
    }

    protected override void OnLayout(LayoutEventArgs layoutEventArgs)
    {
        base.OnLayout(layoutEventArgs);
        MainDocument?.LayoutInternal();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Draw the border:
        using var pen = new Pen(BorderColor, BorderWidth);
        e.Graphics.DrawRectangle(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
    }

    public GridViewItem NewItem() => NewItem(new PaddingF(20), new SizeF(100, 100), null);
    public GridViewItem NewItem(SizeF size) => NewItem(new PaddingF(20), size, null);
    public GridViewItem NewItem(float allMargin, SizeF size) => NewItem(new PaddingF(allMargin), size, null);
    public GridViewItem NewItem(object? tag) => NewItem(new PaddingF(20), new SizeF(100, 100), tag);
    public GridViewItem NewItem(SizeF size, object? tag) => NewItem(new PaddingF(20), size, tag);
    public GridViewItem NewItem(float allMargin, SizeF size, object? tag) => NewItem(new PaddingF(allMargin), size, tag);
    public GridViewItem NewItem(PaddingF margin, SizeF size, object? tag)
    {
        var item = new GridViewItem(MainDocument!, _syncContext) 
        { 
            Margin = margin, 
            Size=size, 
            Tag = tag 
        };

        return item;
    }
}
