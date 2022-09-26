using System.ComponentModel;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;

#nullable enable

public abstract class AutoLayoutUserControl<T> : UserControl where T : INotifyPropertyChanged
{
    private readonly AutoLayoutDocument<T> _document;

    public AutoLayoutUserControl()
    {
        _document = GetDocument();
        GenerateComponents();
    }

    private void GenerateComponents()
    {
    }

    public new T? DataContext
    {
        get => (T?)base.DataContext;
        set => base.DataContext = value;
    }

    protected static AutoLayoutDocument<T> NewDocument
        => new AutoLayoutDocument<T>();

    public abstract AutoLayoutDocument<T> GetDocument();
}
