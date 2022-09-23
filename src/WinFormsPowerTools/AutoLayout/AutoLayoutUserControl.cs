using System.ComponentModel;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;

public abstract class AutoLayoutUserControl<T> : UserControl where T : INotifyPropertyChanged
{
    private readonly AutoLayoutDocument<T> _document;

    public AutoLayoutUserControl()
    {
        string name, text;
        _document = CreateDocument(out name, out text);
        this.Name = name;
        this.Text = text;
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

    protected abstract AutoLayoutDocument<T> CreateDocument(out string name, out string text);
}
