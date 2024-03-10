using System.Diagnostics;
using WinForms.PowerTools.Controls;

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

    private bool _disposedValue;

    internal AsyncDocumentItem(IDocument parentDocument, WindowsFormsSynchronizationContext syncContext)
    {
        SyncContext = syncContext ?? throw new ArgumentNullException(nameof(syncContext));
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

    public PointF EffectiveLocation => new(
        _location.X + Margin.Left,
        _location.Y + Margin.Top);

    public RectangleF Bounds => new(
        _location, 
        _size);

    public PaddingF Padding { get; set; }

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

    /// <summary>
    ///  Gets the synchronization context associated with the document item.
    /// </summary>
    public WindowsFormsSynchronizationContext SyncContext { get; }

    public string DebugInfo => $"{_id:0000} - Location:{this.Location} Size:{this.Size}";

    /// <summary>
    ///  Updates the visibility change state of the document item.
    /// </summary>
    public void UpdateVisibilityChangeState(PointF offset)
    {
        _parentViewSize = _parentDocument.Size;

        bool isNowFullyVisible = IsFullyVisible(offset);
        bool isNowPartiallyVisible = false;
        bool isNowFullyInvisible = false;

        if (isNowFullyVisible) goto actualMethod;
        if (isNowPartiallyVisible = IsPartiallyVisible(offset)) goto actualMethod;
        isNowFullyInvisible = IsFullyInvisible(offset);

actualMethod:

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
    protected abstract void OnVisibilityChangedStateChanged(VisibilityChangeState visibilityChangeState);

    /// <summary>
    ///  Renders the document item asynchronously.
    /// </summary>
    /// <param name="scrollOffset">The scroll offset.</param>
    /// <param name="deviceContext">The device context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous rendering operation.</returns>
    protected internal abstract void OnGetRenderPredicate(out Func<IDeviceContext, CancellationToken, Task> asyncRenderPredicate);

    public T? AsyncInvoke<T>(Func<Task<T>> asyncFunc)
        => ((Control)_parentDocument.HostControl).AsyncInvoke(asyncFunc);

    public Task InvokeAsync(Action syncFunc) 
        => ((Control)_parentDocument.HostControl).InvokeAsync(syncFunc);

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
