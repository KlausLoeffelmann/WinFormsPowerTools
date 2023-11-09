using System.ComponentModel;
using System.ComponentModel.Design;

namespace WinForms.PowerTools.Components;

/// <summary>
///  Provides theming capabilities to a form.
/// </summary>
public partial class ThemingComponent : BindableComponent
{
    private ContainerControl? _parentContainer;
    private ThemingMode _themingMode;

    public event EventHandler<EventArgs>? ThemingModeChanged;
    public event EventHandler<ThemingEventArgs>? ApplyingTheme;

    private readonly ToolStripProfessionalRenderer _darkProfessionalRenderer =
        new(new ThemingColors.DarkProfessionalColors());

    public ThemingComponent() { }

    /// <summary>
    /// Gets or sets the parent form.
    /// </summary>
    public ContainerControl? ParentContainer
    {
        get => _parentContainer;
        set
        {
            _parentContainer ??= value;
        }
    }

    /// <summary>
    ///  Overrides the Site property to capture the ParentForm at design-time.
    /// </summary>
    public override ISite? Site
    {
        set
        {
            base.Site = value;
            if (value?.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
            {
                var rootComponent = designerHost.RootComponent;
                ParentContainer = rootComponent as ContainerControl;
            }
        }
    }

    /// <summary>
    ///  Applies the theme to all controls in the ParentForm.
    /// </summary>
    private void ApplyTheming()
    {
        if (ParentContainer != null)
        {
            var colorContainer = ThemingColors.GetColors(ThemingMode);

            ApplyThemingRecursive(
                ParentContainer,
                _themingMode,
                colorContainer);
        }
    }

    private void ApplyThemingRecursive(
        Control control,
        ThemingMode theme,
        ThemingColors colorContainer)
    {
        foreach (Control childControl in control.Controls)
        {
            ApplyThemingRecursive(childControl, theme, colorContainer);
        }

        var eventArgs = new ThemingEventArgs(control, theme, colorContainer);

        switch (control)
        {
            case Button button:
                OnApplyButtonTheming(eventArgs, button);
                break;
            case Label label:
                OnApplyLabelTheming(eventArgs, label);
                break;
            case TextBox textBox:
                OnApplyTextBoxTheming(eventArgs, textBox);
                break;
            case ListBox listBox:
                OnApplyListBoxTheming(eventArgs, listBox);
                break;
            case ComboBox comboBox:
                OnApplyComboBoxTheming(eventArgs, comboBox);
                break;
            case DateTimePicker dateTimePicker:
                OnApplyDateTimePickerTheming(eventArgs, dateTimePicker);
                break;
            case GroupBox groupBox:
                OnApplyGroupBoxTheming(eventArgs, groupBox);
                break;
            case ListView listView:
                OnApplyListViewTheming(eventArgs, listView);
                break;
            case DataGridView dataGridView:
                OnApplyDataGridViewTheming(eventArgs, dataGridView);
                break;
            case RadioButton radioButton:
                OnApplyRadioButtonTheming(eventArgs, radioButton);
                break;
            case TreeView treeView:
                OnApplyTreeViewTheming(eventArgs, treeView);
                break;
            case ToolStrip toolStrip:
                OnApplyToolStripTheming(eventArgs, toolStrip);
                break;

            default:
                OnApplyDefaultControlTheming(eventArgs);
                break;
        }

        if (eventArgs.Handled)
        {
            return;
        }

        OnApplyTheming(eventArgs);
        if (eventArgs.Handled)
        {
            return;
        }
    }

    /// <summary>
    ///  Raises the ApplyingTheme event.
    /// </summary>
    /// <param name="control">The control to which the theme is being applied.</param>
    /// <param name="theme">The theme being applied.</param>
    protected virtual void OnApplyTheming(ThemingEventArgs eventArgs)
        => ApplyingTheme?.Invoke(this, eventArgs);

    protected virtual void OnApplyDefaultControlTheming(ThemingEventArgs eventArgs)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    protected virtual void OnApplyLabelTheming(ThemingEventArgs eventArgs, Label label)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a Button control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyButtonTheming(ThemingEventArgs eventArgs, Button button)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a TextBox control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyTextBoxTheming(ThemingEventArgs eventArgs, TextBox textBox)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a ListBox control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyListBoxTheming(ThemingEventArgs eventArgs, ListBox listBox)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a ComboBox control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyComboBoxTheming(ThemingEventArgs eventArgs, ComboBox comboBox)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a CheckedListBox control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyCheckedListBoxTheming(ThemingEventArgs eventArgs, CheckedListBox checkedListBox)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a DateTimePicker control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyDateTimePickerTheming(ThemingEventArgs eventArgs, DateTimePicker dateTimePicker)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a GroupBox control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyGroupBoxTheming(ThemingEventArgs eventArgs, GroupBox groupBox)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a ListView control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyListViewTheming(ThemingEventArgs eventArgs, ListView listView)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a DataGridView control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyDataGridViewTheming(ThemingEventArgs eventArgs, DataGridView dataGridView)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a RadioButton control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyRadioButtonTheming(ThemingEventArgs eventArgs, RadioButton radioButton)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }


    /// <summary>
    /// Applies theming to a TreeView control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyTreeViewTheming(ThemingEventArgs eventArgs, TreeView treeView)
    {
        eventArgs.Control.ForeColor = eventArgs.ColorContainer.WindowText;
        eventArgs.Control.BackColor = eventArgs.ColorContainer.Window;
    }

    /// <summary>
    /// Applies theming to a TreeView control.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing theming information.</param>
    protected virtual void OnApplyToolStripTheming(ThemingEventArgs eventArgs, ToolStrip toolStrip)
    {
        if (eventArgs.ThemingMode == ThemingMode.DarkMode)
        {
            toolStrip.Renderer = _darkProfessionalRenderer;
            toolStrip.RenderMode = ToolStripRenderMode.Professional;
            toolStrip.BackColor = eventArgs.ColorContainer.Window;
            toolStrip.ForeColor = eventArgs.ColorContainer.WindowText;
        }
        else
        {
            toolStrip.Renderer = new ToolStripProfessionalRenderer();
            toolStrip.RenderMode = ToolStripRenderMode.Professional;
            toolStrip.BackColor = eventArgs.ColorContainer.Window;
            toolStrip.ForeColor = eventArgs.ColorContainer.WindowText;
        }
    }
}
