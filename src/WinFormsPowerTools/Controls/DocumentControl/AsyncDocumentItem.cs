namespace System.Windows.Forms.Documents;

/// <summary>
///  Represents an abstract base class for asynchronous document items.
/// </summary>
public abstract class AsyncDocumentItem : IDisposable
{
    private static int s_index;

    private readonly int _id = s_index++;

    private PointF _location;
    private SizeF _size;
    private VisibilityChangeState _visibilityChangeState;
    private VisibilityChangeState _previousVisibilityChangeState;

    private IDocument _parentDocument;
    private SizeF _parentViewSize;
    private nint _parentControlHandle;

    private bool _disposedValue;

    internal AsyncDocumentItem(IDocument parentDocument, nint parentControlHandle)
    {
        _parentControlHandle = parentControlHandle;
        _parentDocument = parentDocument ?? throw new ArgumentNullException(nameof(parentDocument));
    }

    protected int Id => _id;

    /// <summary>
    ///  Gets or sets the location of the document item.
    /// </summary>
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

    protected nint ParentControlHandle => _parentControlHandle;

    public RectangleF Bounds => new(
        _location, 
        _size);

    public RectangleF ClientRectangle => new(
        x: 0,
        y: 0,
        width: _size.Width - Margin.Right - Margin.Left,
        height: _size.Height - Margin.Bottom - Margin.Top);

    public PaddingF Margin { get; set; }

    public SizeF ClientSize => new(
        _size.Width - Margin.Right - Margin.Left,
        _size.Height - Margin.Top - Margin.Bottom);

    /// <summary>
    ///  Gets or sets the size of the document item.
    /// </summary>
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

    /// <summary>
    ///  Gets the visibility change state of the document item.
    /// </summary>
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

    public string DebugInfo => $"{_id:0000} - Location:{this.Location} Size:{this.Size}";

    /// <summary>
    ///  Updates the visibility change state of the document item.
    /// </summary>
    public void UpdateVisibilityChangeState(PointF offset)
    {
        _parentViewSize = _parentDocument.Size;

        bool isNowFullyVisible = IsFullyVisible(offset);
        bool isNowPartiallyVisible = IsPartiallyVisible(offset);
        bool isNowFullyInvisible = IsFullyInvisible(offset);

        bool wasFullyVisible =
            _previousVisibilityChangeState == VisibilityChangeState.GotFullyVisible
            || _previousVisibilityChangeState == VisibilityChangeState.StillFullyVisible;

        bool wasPartiallyVisible =
            _previousVisibilityChangeState == VisibilityChangeState.GotPartiallyVisible
            || _previousVisibilityChangeState == VisibilityChangeState.StillPartiallyVisible;

        bool wasFullyInvisible =
            _previousVisibilityChangeState == VisibilityChangeState.GotFullyInvisible
            || _previousVisibilityChangeState == VisibilityChangeState.StillFullyInvisible;

        VisibilityChangeState = DetermineVisibilityChangeState(
            isNowFullyVisible,
            isNowPartiallyVisible,
            isNowFullyInvisible,
            wasFullyVisible,
            wasPartiallyVisible,
            wasFullyInvisible);

        _previousVisibilityChangeState = VisibilityChangeState;

        VisibilityChangeState DetermineVisibilityChangeState(bool isNowFullyVisible, bool isNowPartiallyVisible, bool isNowFullyInvisible, bool wasFullyVisible, bool wasPartiallyVisible, bool wasFullyInvisible)
        {
            if (isNowFullyVisible)
            {
                return wasFullyVisible 
                    ? VisibilityChangeState.StillFullyVisible 
                    : VisibilityChangeState.GotFullyVisible;
            }

            if (isNowPartiallyVisible)
            {
                return wasPartiallyVisible 
                    ? VisibilityChangeState.StillPartiallyVisible 
                    : VisibilityChangeState.GotPartiallyVisible;
            }

            if (isNowFullyInvisible)
            {
                return wasFullyInvisible 
                    ? VisibilityChangeState.StillFullyInvisible 
                    : VisibilityChangeState.GotFullyInvisible;
            }

            throw new InvalidOperationException("Invalid visibility state transition.");
        }
    }

    public bool IsPartiallyVisible(PointF offset)
    {
        // Assuming this means the item is either fully
        // or partially visible but not fully invisible
        return !IsFullyInvisible(offset) && !IsFullyVisible(offset);
    }

    public bool IsFullyInvisible(PointF offset)
    {
        RectangleF viewRect = new(PointF.Empty, _parentViewSize);
        RectangleF itemRect = Bounds;
        itemRect.Offset(offset);

        return !viewRect.IntersectsWith(itemRect);
    }

    public bool IsFullyVisible(PointF offset)
    {
        RectangleF viewRect = new(PointF.Empty, _parentViewSize);
        RectangleF itemRect = Bounds;
        itemRect.Offset(offset);

        return viewRect.Contains(itemRect);
    }

    /// <summary>
    ///  Called when the location of the document item changes.
    /// </summary>
    protected virtual void OnLocationChanged() { }

    /// <summary>
    ///  Called when the size of the document item changes.
    /// </summary>
    protected virtual void OnSizeChanged() { }

    /// <summary>
    ///  Called when the parent view size changes.
    /// </summary>
    protected virtual void OnParentViewSizeChanged() { }

    /// <summary>
    ///  Called when the visibility change state of the document item changes.
    /// </summary>
    /// <param name="visibilityChangeState">The new visibility change state.</param>
    protected void OnVisibilityChangedStateChanged(VisibilityChangeState visibilityChangeState)
    {
        if (visibilityChangeState == VisibilityChangeState.GotFullyInvisible)
        {
            // We need to signal the instance which holds the graphics object, that it can be disposed now.
            DeviceContext?.Dispose();
            DeviceContext = null;
        }
    }

    /// <summary>
    ///  Renders the document item asynchronously.
    /// </summary>
    /// <param name="scrollOffset">The scroll offset.</param>
    /// <param name="deviceContext">The device context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous rendering operation.</returns>
    protected abstract internal Func<PointF, CancellationToken, Task> OnGetRenderPredicate();

    protected IDeviceContext? DeviceContext { get; set; }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            _disposedValue = true;
        }
    }

    /// <summary>
    ///  Releases all resources used by the <see cref="AsyncDocumentItem"/> object.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public override string ToString()
        => $"#: {DebugInfo} V:{VisibilityChangeState}";
}
