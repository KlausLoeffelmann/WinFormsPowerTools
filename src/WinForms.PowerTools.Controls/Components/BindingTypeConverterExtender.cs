using System.ComponentModel;
using static WinForms.PowerTools.Components.BindingTypeConverterExtender;

namespace WinForms.PowerTools.Components;

// Usage in an extender provider:
[ProvideProperty("BindingTypeConverters", typeof(IBindableComponent))]
public partial class BindingTypeConverterExtender : Component, IExtenderProvider
{
    private readonly Dictionary<IBindableComponent, BindingTypeConverterCollection> _propertyStorage = [];

    public BindingTypeConverterExtender()
    {
    }

    public bool CanExtend(object extendee)
    {
        if (extendee is IBindableComponent bindableComponent)
        {
            if (!_propertyStorage.ContainsKey(bindableComponent))
            {
                var bindingTypeConverterCollection = new BindingTypeConverterCollection();
                foreach (Binding bindingItem in bindableComponent.DataBindings)
                {
                    bindingTypeConverterCollection.Add(new TypeConverterPropertyDescriptor(bindingItem.PropertyName));
                }

                _propertyStorage.Add(bindableComponent, bindingTypeConverterCollection);
            }

            return true;
        }

        return false;
    }

    [ParenthesizePropertyName(true)]
    [DisplayName("BindingTypeConverters")]
    [Description("The collection of binding converters for every bound class.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BindingTypeConverterCollection GetBindingTypeConverters(IBindableComponent bindableComponent)
    {
        if (_propertyStorage.TryGetValue(bindableComponent, out BindingTypeConverterCollection? bindingConverterCollection))
        {
            return bindingConverterCollection;
        }

        return [];
    }

    public void SetBindingTypeConverters(IBindableComponent bindableComponent, BindingTypeConverterCollection bindingTypeConverterCollection)
    {
        if (_propertyStorage.ContainsKey(bindableComponent))
        {
            _propertyStorage[bindableComponent] = bindingTypeConverterCollection;
        }
        else
        {
            _propertyStorage.Add(bindableComponent, bindingTypeConverterCollection);
        }
    }
}
