namespace System.Windows.Forms.Documents;

public abstract class AsyncDocumentItem
{
    private SizeF _parentViewSize;
    private PointF _location;
    private SizeF _size;
    private VisibilityChangeState _visibilityChangeState;
    private VisibilityChangeState _previousVisibilityChangeState;
    private Document? _parentDocument;

    public PointF Location
    {
        get => _location;
        set
        {
            if (_location == value) return;

            _location = value;
            OnLocationChanged();
        }
    }

    public SizeF Size
    {
        get => _size;
        set
        {
            if (_size == value) return;

            _size = value;
            OnSizeChanged();
        }
    }

    public SizeF ParentViewSize
    {
        get => _parentViewSize;
        internal set
        {
            if (_parentViewSize == value) return;

            _parentViewSize = value;
            OnParentViewSizeChanged();
        }
    }

    public VisibilityChangeState VisibilityChangeState
    {
        get => _visibilityChangeState;
        private set
        {
            if (_visibilityChangeState == value) return;

            _previousVisibilityChangeState = _visibilityChangeState;
            _visibilityChangeState = value;
            OnVisibilityChangedStateChanged(value);
        }
    }

    public Document? ParentDocument => _parentDocument;

    internal void SetParentDocument(Document parentDocument)
    {
        _parentDocument = parentDocument ?? throw new ArgumentNullException(nameof(parentDocument));
    }

    public void UpdateVisibilityChangeState()
    {
        bool isNowFullyVisible = IsFullyVisible();
        bool isNowPartiallyVisible = IsPartiallyVisible();
        bool isNowFullyInvisible = IsFullyInvisible();

        bool wasFullyVisible = 
            _previousVisibilityChangeState == VisibilityChangeState.GotFullyVisible
            || _previousVisibilityChangeState == VisibilityChangeState.GotPartiallyInvisible;

        VisibilityChangeState = (
            isNowFullyVisible, 
            isNowPartiallyVisible, 
            isNowFullyInvisible, 
            wasFullyVisible) switch
        {
            (true, _, _, false) => VisibilityChangeState.GotFullyVisible,
            (_, true, _, true) => VisibilityChangeState.GotPartiallyInvisible,
            (_, true, true, _) => VisibilityChangeState.GotPartiallyVisible,
            (_, _, true, _) => VisibilityChangeState.GotFullyInvisible,
            _ => _visibilityChangeState // No change
        };
    }

    private bool IsPartiallyVisible()
    {
        // Assuming this means the item is either fully
        // or partially visible but not fully invisible
        return !IsFullyInvisible();
    }

    private bool IsFullyInvisible()
    {
        RectangleF itemRect = new(_location, _size);
        RectangleF viewRect = new(PointF.Empty, _parentViewSize);

        return !itemRect.IntersectsWith(viewRect);
    }

    private bool IsFullyVisible()
    {
        RectangleF itemRect = new(_location, _size);
        RectangleF viewRect = new(PointF.Empty, _parentViewSize);

        return viewRect.Contains(itemRect);
    }

    protected virtual void OnLocationChanged() 
        => UpdateVisibilityChangeState();

    protected virtual void OnSizeChanged() 
        => UpdateVisibilityChangeState();

    protected virtual void OnParentViewSizeChanged() 
        => UpdateVisibilityChangeState();

    protected abstract void OnVisibilityChangedStateChanged(VisibilityChangeState visibilityChangeState);

    internal abstract Task OnRenderAsync(PointF scrollOffset, IDeviceContext deviceContext, CancellationToken cancellationToken);
}
