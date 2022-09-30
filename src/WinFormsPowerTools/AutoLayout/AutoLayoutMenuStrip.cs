using System;
using System.ComponentModel;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;

#nullable enable

public abstract class AutoLayoutMenuStrip<T> : MenuStrip where T : INotifyPropertyChanged
{
    private BindingSource _bindingSource;

    public AutoLayoutMenuStrip()
    {
        _bindingSource = new BindingSource();
        _bindingSource.DataSource = typeof(T);
        GenerateComponents();
    }

    private void InitializeComponentsInternal()
    {
        //AutoSize = true;
        //AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }

    private void GenerateComponents()
    {
        var menu = GetMenu();

        foreach (AutoLayoutMenuItem<T> item in menu.Components)
        {
            this.Items.Add(GenerateMenuItem(item));
        }
    }

    private ToolStripMenuItem GenerateMenuItem(AutoLayoutMenuItem<T> menuItem)
    {
        ToolStripMenuItem tsMenuItem = new ToolStripMenuItem();
        tsMenuItem.Text = menuItem.Text;
        tsMenuItem.Name = menuItem.Name;
        tsMenuItem.Checked = menuItem.IsChecked;
        tsMenuItem.Enabled = true;
        tsMenuItem.Command = menuItem.Command;

        if (menuItem.Bindings.TryGetBinding(nameof(ToolStripMenuItem.Text), out var textBinding))
        {
            tsMenuItem.DataBindings.Add(nameof(ToolStripMenuItem.Text), _bindingSource, textBinding!.BindingPath);
        }

        if (menuItem.Bindings.TryGetBinding(nameof(ToolStripMenuItem.Command), out var commandBinding))
        {
            tsMenuItem.DataBindings.Add(nameof(ToolStripMenuItem.Command), _bindingSource, commandBinding!.BindingPath);
        }

        foreach (AutoLayoutMenuItem<T> subMenuItem in menuItem.Components)
        {
            tsMenuItem.DropDownItems.Add(GenerateMenuItem(subMenuItem));
        }

        return tsMenuItem;
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        _bindingSource.DataSource = DataContext;
    }

    public new T? DataContext
    {
        get => (T?)base.DataContext;
        set => base.DataContext = value;
    }

    public abstract AutoLayoutMenu<T> GetMenu();

    public AutoLayoutMenu<T> GetNewMenu()
        => new AutoLayoutMenu<T>();

    public AutoLayoutMenuItem<T> GetNewMenuItem(
        string? name = default,
        string? text = default,
        bool isChecked = false,
        bool isEnabled = true,
        System.Windows.Input.ICommand? command = default)
            => new AutoLayoutMenuItem<T>(name, text, command, isEnabled, isChecked);
}
