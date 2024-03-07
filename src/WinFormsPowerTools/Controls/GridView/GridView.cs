using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridView : DocumentControl<GridViewDocument, GridViewItem>
{
    private Type? _viewTemplateType;
    private Orientation _orientation;
    private int _adjacentItemsCount;

    public GridView()
    {
        _orientation = Orientation.Horizontal;
        _adjacentItemsCount = 1;
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

    public int AdjacentItemsCount
    {
        get => _adjacentItemsCount;

        set
        {
            if (_adjacentItemsCount == value)
            {
                return;
            }

            _adjacentItemsCount = value;
            OnAdjacentItemsCountChanged();
            Invalidate();
        }
    }

    private void OnAdjacentItemsCountChanged()
    {
        MainDocument?.LayoutInternal();
    }

    private void OnOrientationChanged()
    {
        MainDocument?.LayoutInternal();
    }

    private void OnViewTemplateChanged()
    {
        MainDocument?.LayoutInternal();
    }

    protected override void OnPaddingChanged(EventArgs e)
    {
        base.OnPaddingChanged(e);
        MainDocument?.LayoutInternal();
    }

    protected override void OnClientSizeChanged(EventArgs e)
    {
        base.OnClientSizeChanged(e);
        MainDocument?.LayoutInternal();
    }
}
