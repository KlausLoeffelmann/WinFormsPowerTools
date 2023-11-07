using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable enable

namespace WinForms.PowerTools.Components;

/// <summary>
///  Manages the binding converters for an entire container control.
/// </summary>
public class BindingConverterManager : Component
{
    private ContainerControl? _parentContainer;
    private BindingContext? _parentBindingContext;

    public event ConvertEventHandler? Parse;
    public event ConvertEventHandler? Format;

    private void ParseHandler(object? sender, ConvertEventArgs e) 
        => Parse?.Invoke(sender, e);

    private void FormatHandler(object? sender, ConvertEventArgs e) 
        => Format?.Invoke(sender, e);

    // Method to add or remove the event handlers
    private void UpdateBindingEventHandlers(Control control, bool addHandlers)
    {
        // Iterate through all controls in the form recursively
        var t = control.Container?.Components;
        foreach (Control childControl in control.Controls)
        {
            UpdateBindingEventHandlers(childControl, addHandlers);
        }

        // Process the bindings of the current control
        foreach (Binding binding in control.DataBindings)
        {
            if (addHandlers)
            {
                binding.Parse += ParseHandler;
                binding.Format += FormatHandler;
            }
            else
            {
                binding.Parse -= ParseHandler;
                binding.Format -= FormatHandler;
            }
        }
    }

    /// <summary>
    /// Gets or sets the parent form.
    /// </summary>
    public ContainerControl? ParentContainer
    {
        get => _parentContainer;

        set
        {
            if (_parentContainer != null)
            {
                UpdateBindingEventHandlers(_parentContainer, false);
                _parentContainer.BindingContextChanged -= ParentContainer_BindingContextChanged;
            }

            _parentContainer ??= value;

            if (_parentContainer != null)
            {
                _parentContainer.BindingContextChanged += ParentContainer_BindingContextChanged; ;
            }
        }
    }

    private void ParentContainer_BindingContextChanged(object? sender, System.EventArgs e)
    {
        if (_parentBindingContext != null)
        {
            UpdateBindingEventHandlers(_parentContainer!, false);
        }

        _parentBindingContext = _parentContainer?.BindingContext;

        if (_parentBindingContext != null)
        {
            UpdateBindingEventHandlers(_parentContainer!, true);
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

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            ParentContainer = null;
        }
    }
}
