namespace System.Windows.Forms.Documents;

/// <summary>
///  Represents an abstract base class for asynchronous document items.
/// </summary>
public abstract class AsyncDocumentItem : IDisposable
{
    private static int _index;

    private readonly int _id = _index++;
    private PointF _location;
    private PointF _offset;
    private SizeF _size;
    private VisibilityChangeState _visibilityChangeState;
    private VisibilityChangeState _previousVisibilityChangeState;

    private IDocument _parentDocument;
    private SizeF _parentViewSize;

    private bool _disposedValue;

    private bool? _isFullyInvisible;

    internal AsyncDocumentItem(IDocument parentDocument, WindowsFormsSynchronizationContext syncContext)
    {
        SyncContext = syncContext ?? throw new ArgumentNullException(nameof(syncContext));
        _parentDocument = parentDocument ?? throw new ArgumentNullException(nameof(parentDocument));
    }

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

    public PaddingF Padding { get; set; }

    public PaddingF Margin { get; set; }

    /// <summary>
    ///  Gets or sets the offset of the document item.
    /// </summary>
    public PointF Offset
    {
        get => _offset;
        set
        {
            if (_offset == value) return;

            _offset = value;
            OnOffsetChanged();
        }
    }

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

    /// <summary>
    ///  Gets the synchronization context associated with the document item.
    /// </summary>
    public WindowsFormsSynchronizationContext SyncContext { get; }

    /// <summary>
    ///  Updates the visibility change state of the document item.
    /// </summary>
    public void UpdateVisibilityChangeState()
    {
        _isFullyInvisible = null;
        _parentViewSize = _parentDocument!.Size;

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

    public bool IsPartiallyVisible()
    {
        // Assuming this means the item is either fully
        // or partially visible but not fully invisible
        return !IsFullyInvisible();
    }

    public bool IsFullyInvisible()
    {
        return _isFullyInvisible ??= IsItemFullyVisible();

        bool IsItemFullyVisible()
        {
            RectangleF itemRect = new RectangleF(
                new PointF(_location.X + _offset.X, _location.Y + _offset.Y),
                _size);

            RectangleF viewRect = new(PointF.Empty, _parentViewSize);

            return !itemRect.IntersectsWith(viewRect);
        }
    }

    public bool IsFullyVisible()
    {
        RectangleF itemRect = new RectangleF(
            new PointF(_location.X + _offset.X, _location.Y + _offset.Y),
            _size);

        RectangleF viewRect = new(PointF.Empty, _parentViewSize);

        return viewRect.Contains(itemRect);
    }

    /// <summary>
    ///  Called when the location of the document item changes.
    /// </summary>
    protected virtual void OnLocationChanged()
        => UpdateVisibilityChangeState();

    /// <summary>
    ///  Called when the size of the document item changes.
    /// </summary>
    protected virtual void OnSizeChanged()
        => UpdateVisibilityChangeState();

    /// <summary>
    ///  Called when the parent view size changes.
    /// </summary>
    protected virtual void OnParentViewSizeChanged()
        => UpdateVisibilityChangeState();

    /// <summary>
    ///  Called when the offset of the document item changes.
    /// </summary>
    protected virtual void OnOffsetChanged()
        => UpdateVisibilityChangeState();

    /// <summary>
    ///  Called when the visibility change state of the document item changes.
    /// </summary>
    /// <param name="visibilityChangeState">The new visibility change state.</param>
    protected abstract void OnVisibilityChangedStateChanged(VisibilityChangeState visibilityChangeState);

    /// <summary>
    ///  Renders the document item asynchronously.
    /// </summary>
    /// <param name="scrollOffset">The scroll offset.</param>
    /// <param name="deviceContext">The device context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous rendering operation.</returns>
    internal abstract Task OnRenderAsync(PointF scrollOffset, IDeviceContext deviceContext, CancellationToken cancellationToken);

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
}
