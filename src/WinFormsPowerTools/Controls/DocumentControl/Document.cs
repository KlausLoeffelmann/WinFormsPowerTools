using System.Collections.ObjectModel;

namespace System.Windows.Forms.Documents;

public abstract class Document<tDocItem> : IDocument, IDisposable
    where tDocItem : AsyncDocumentItem
{
    private bool disposedValue;
    private SizeF _size;
    private RectangleF _displayBounds;
    private IDocumentControl? _hostControl;

    public event EventHandler? SizeChanged;
    public event EventHandler? DisplayBoundsChanged;

    internal Document(IDocumentControl hostControl)
    {
        _hostControl = hostControl ?? throw new ArgumentNullException(nameof(hostControl));
    }

    public SizeF Size
    {
        get => _size;
        set
        {
            if (_size == value)
            {
                return;
            }

            _size = value;
            OnSizeChanged();
        }
    }

    public ObservableCollection<tDocItem> Items { get; }
        = new ObservableCollection<tDocItem>();

    IDocumentControl IDocument.HostControl
    {
        // Todo: This needs to be done more robust by introducing a ParkingParent. A document should at no point be without a host control.
        get => _hostControl!;
        set
        {
            if (_hostControl is not null)
            {
                throw new InvalidOperationException("HostControl is already set!");
            }

            _hostControl = value;
        }
    }

    public RectangleF DisplayBounds 
    { 
        get => _displayBounds;
        internal set
        {
            if (_displayBounds == value)
            {
                return;
            }

            _displayBounds = value;
            DisplayBoundsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    protected abstract void OnSizeChanged();

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                foreach (var item in Items)
                {
                    item.Dispose();
                }
            }

            disposedValue = true;
        }
    }

    protected void Invalidate()
    {
        _hostControl?.Invalidate();
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
