using System.ComponentModel;
using System.Windows.Forms.DataEntryForms.Components;

namespace WinForms.PowerTools.Controls;

[ProvideProperty("FormatterComponent", typeof(TextBox))]
public class DataEntryPanel : Panel, IExtenderProvider
{
    private readonly Dictionary<TextBox, IDataEntryFormatterComponent> _propertyStorage = [];

    bool IExtenderProvider.CanExtend(object extendee)
    {
        // We only want to extend TextBoxes:
        return extendee is TextBox 
            && extendee is Control control
            && IsThisAncestorOf(control);

        bool IsThisAncestorOf(Control control) 
            => control.Parent == this 
                || (control.Parent is not null && IsThisAncestorOf(control.Parent));
    }

    // This is the method the DataEntryFormatterComponent will call to determine if
    // it can extend the TextBox.
    internal bool CanDataEntryComponentExtend(TextBox textBox)
        => _propertyStorage.ContainsKey(textBox);

    [DisplayName("FormatterComponent")]
    [ParenthesizePropertyName(true)]
    [RefreshProperties(RefreshProperties.All)]
    public IDataEntryFormatterComponent? GetFormatterComponent(TextBox textBox)
    {
        if (_propertyStorage.TryGetValue(textBox, out IDataEntryFormatterComponent? value))
        {
            return value;
        }

        return null;
    }

    public void SetFormatterComponent(TextBox textBox, IDataEntryFormatterComponent? formatterComponent)
    {
        if (formatterComponent is null)
        {
            _propertyStorage.Remove(textBox);
        }
        else
        {
            _propertyStorage[textBox] = formatterComponent;
        }
    }
}
