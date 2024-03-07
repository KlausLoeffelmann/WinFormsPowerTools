using System.Collections.ObjectModel;

namespace System.Windows.Forms.Documents;

internal interface IDocument
{     
    float Width { get; set; }
    float Height { get; set; }
    IDocumentControl? HostControl { get; set; }
}

public class Document<tDocItem> : IDocument, IDisposable
    where tDocItem : AsyncDocumentItem
{
    private bool _suspendUpdates;
    private bool disposedValue;
    private float _width;
    private float _height;
    private IDocumentControl? _hostControl;

    internal Document(IDocumentControl hostControl)
    {
        _hostControl = hostControl ?? throw new ArgumentNullException(nameof(hostControl));
    }

    public float Width
    {
        get => _width;
        set
        {
            if (_width == value) return;

            _width = value;
            OnWidthChanged();
        }
    }

    protected virtual void OnWidthChanged()
    {
    }

    public float Height
    {
        get => _height;
        set
        {
            if (_height == value) return;

            _height = value;
            OnHeightChanged();
        }
    }

    protected virtual void OnHeightChanged()
    {
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

    public void SuspendUpdates()
    {
        if (_suspendUpdates)
        {
            throw new InvalidOperationException("Updates are already suspended!");
        }

        _suspendUpdates = true;
    }

    public void ResumeUpdates()
    {
        if (!_suspendUpdates)
        {
            throw new InvalidOperationException("Updates are not suspended!");
        }

        _suspendUpdates = false;
    }

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

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
