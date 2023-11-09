using Microsoft.DotNet.DesignTools.Serialization;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using WinForms.PowerTools.Controls.Designer;

namespace WinForms.PowerTools.Components;

// Usage in an extender provider:
[ProvideProperty("BindingConverterSettings", typeof(IBindableComponent))]
public partial class BindingTypeConverterExtender : Component, IExtenderProvider
{
    private readonly Dictionary<IBindableComponent, BindingConverterSettingCollection> _propertyStorage = [];

    public BindingTypeConverterExtender()
    {
    }

#nullable enable

    public bool CanExtend(object extendee)
    {
        if (extendee is IBindableComponent bindableComponent)
        {
            if (!_propertyStorage.TryGetValue(bindableComponent, out var bindingTypeConverterCollection))
            {
                bindingTypeConverterCollection = new BindingConverterSettingCollection();
                _propertyStorage.Add(bindableComponent, bindingTypeConverterCollection);
            }

            SyncBindingConverterCollection(bindableComponent, bindingTypeConverterCollection);

            return true;
        }

        return false;
    }

    private void SyncBindingConverterCollection(
        IBindableComponent bindableComponent,
        BindingConverterSettingCollection bindingConverterSettings)
    {
        // Create a set of current binding property names
        var currentBindings = new HashSet<string>(
            bindableComponent.DataBindings.Cast<Binding>().Select(b => b.PropertyName));

        // Remove converter settings for bindings that no longer exist
        var entriesToRemove = bindingConverterSettings.Cast<BindingConverterSetting>()
            .Where(setting => !currentBindings.Contains(setting.PropertyName))
            .ToList();

        foreach (var entry in entriesToRemove)
        {
            bindingConverterSettings.Remove(entry);
        }

        // Add new settings for bindings that are not in the converter collection
        foreach (Binding bindingItem in bindableComponent.DataBindings)
        {
            if (bindingConverterSettings
                .Cast<BindingConverterSetting>()
                .All(setting => setting.PropertyName != bindingItem.PropertyName))
            {
                bindingConverterSettings.Add(
                    new BindingConverterSetting(
                        targetComponent: bindableComponent,
                        propertyName: bindingItem.PropertyName,
                        typeConverterType: null));
            }
        }
    }

    [ParenthesizePropertyName(true)]
    [DisplayName("BindingTypeConverters")]
    [Description("The collection of binding converters for every bound class.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BindingConverterSettingCollection GetBindingConverterSettings(IBindableComponent bindableComponent)
    {
        if (_propertyStorage.TryGetValue(bindableComponent, out BindingConverterSettingCollection? bindingConverterCollection))
        {
            return bindingConverterCollection;
        }

        return [];
    }

    public void SetBindingConverterSettings(IBindableComponent bindableComponent, BindingConverterSettingCollection bindingTypeConverterCollection)
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

    private bool ShouldSerializeBindingConverterSettings(IBindableComponent bindableComponent) => true;
}
