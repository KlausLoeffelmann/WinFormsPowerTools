using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;

#nullable enable

public abstract class AutoLayoutUserControl<T> : UserControl where T : INotifyPropertyChanged
{
    private BindingSource _bindingSource;

    public AutoLayoutUserControl()
    {
        _bindingSource = new BindingSource();
        _bindingSource.DataSource = typeof(T);
        InitializeComponentsInternal();
        GenerateComponents();
    }

    private void InitializeComponentsInternal()
    {
        //AutoSize = true;
        //AutoSizeMode = AutoSizeMode.GrowAndShrink;
    }

    private void GenerateComponents()
    {
        var document = GetDocument();
        this.Padding = new(10, 10, 10, 10);

        if (document.Content is not null )
        {
            this.Controls.Add(GenerateComponentsInternal(document.Content));
        }
    }

    private Control GenerateComponentsInternal(AutoLayoutComponent<T> component)
    {
        return component switch
        {
            AutoLayoutGrid<T> grid => GenerateGrid(grid),
            AutoLayoutLabel<T> label => GenerateLabel(label),
            AutoLayoutDateEntry<T> dateEntry => GenerateTextBox(dateEntry),
            AutoLayoutTextEntry<T> textEntry => GenerateTextBox(textEntry),
            _ => throw new System.NotImplementedException()
        };
    }

    private Control GenerateGrid(AutoLayoutGrid<T> grid)
    {
        var control = new TableLayoutPanel();
        control.SuspendLayout();

        control.AutoSize = true;
        control.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        control.Dock= DockStyle.Fill;
        control.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

        control.ColumnCount = grid.ColumnDefinitions.Count;
        control.RowCount = grid.RowDefinitions.Count;
        control.Padding = grid.Padding.ToWinFormsPadding();
        control.ColumnStyles.Clear();
        control.RowStyles.Clear();
        control.ColumnStyles.AddRange(grid.ColumnDefinitions.Select(c => c.ToTableLayoutPanelColumnStyle()));
        control.RowStyles.AddRange(grid.RowDefinitions.Select(r => r.ToTableLayoutPanelRowStyle()));

        foreach (var component in grid.Components)
        {
            var childControl = GenerateComponentsInternal(component);

            // TODO: Introduce control alignments like MAUI.

            control.Controls.Add(childControl);
            var fencedPosition = grid.GetFencedPosition(component)!.Value;

            control.SetCellPosition(childControl, fencedPosition.ToTableLayoutPanelCellPosition());
            control.SetColumnSpan(childControl, fencedPosition.ColumnSpan);
            control.SetRowSpan(childControl, fencedPosition.RowSpan);
            childControl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        }

        control.ResumeLayout(false);
        return control;
    }

    private Control GenerateLabel(AutoLayoutLabel<T> label)
    {
        var control = new Label();
        control.Name = label.Name;
        control.Text = label.Text;

        if (label.Bindings.TryGetBinding(nameof(control.Text), out var textBinding))
        {
            control.DataBindings.Add(nameof(Label.Text), _bindingSource, textBinding!.BindingPath);
        }

        return control;
    }

    private Control GenerateTextBox(AutoLayoutTextEntry<T> textBox)
    {
        var control = new TextBox();
        control.Name = textBox.Name;
        control.Text = textBox.Text;

        if (textBox.Bindings.TryGetBinding(nameof(control.Text), out var controlBinding))
        {
            control.DataBindings.Add(nameof(TextBox.Text), _bindingSource, controlBinding!.BindingPath);
        }

        return control;
    }
    
    // TODO: Wire up Format Component extenders.
    private Control GenerateTextBox(AutoLayoutDateEntry<T> dateEntry)
    {
        var control = new TextBox();
        control.Name = dateEntry.Name;
        control.Text = dateEntry.Value.ToString();

        if (dateEntry.Bindings.TryGetBinding(nameof(control.Text), out var controlBinding))
        {
            control.DataBindings.Add(nameof(TextBox.Text), _bindingSource, controlBinding!.BindingPath);
        }

        return control;
    }
    
    private Control GenerateButton(AutoLayoutButton<T> button)
    {
        var control = new Button();
        control.Name = button.Name;
        control.Text = button.Text;

        if (button.Bindings.TryGetBinding(nameof(button.Command), out var commandBinding))
        {
            control.DataBindings.Add(nameof(Button.Command), _bindingSource, commandBinding!.BindingPath);
        }

        if (button.Bindings.TryGetBinding(nameof(button.Text), out var textBinding))
        {
            control.DataBindings.Add(nameof(TextBox.Text), _bindingSource, textBinding!.BindingPath);
        }

        return control;
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

    protected static AutoLayoutDocument<T> NewDocument
        => new AutoLayoutDocument<T>();

    public abstract AutoLayoutDocument<T> GetDocument();
}
