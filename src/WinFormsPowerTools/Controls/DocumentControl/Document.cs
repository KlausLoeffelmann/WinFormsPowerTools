using System.Collections.ObjectModel;

namespace System.Windows.Forms.Documents;

public abstract class Document<tDocItem> : IDocument, IDisposable
    where tDocItem : AsyncDocumentItem
{
    private bool disposedValue;
    private SizeF _size;
    private IDocumentControl? _hostControl;

    public event EventHandler? SizeChanged;

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

    IDocumentControl? IDocument.HostControl
        {
        get => _hostControl;
        set
        {
            if (_hostControl is not null)
            {
                throw new InvalidOperationException("HostControl is already set!");
            }

            _hostControl = value;
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
