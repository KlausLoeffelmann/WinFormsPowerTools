using System.ComponentModel;
using System.Diagnostics;

namespace WinForms.PowerTools.Components;

// Usage in an extender provider:
[ProvideProperty("BindingConverters", typeof(IBindableComponent))]
public partial class BindingTypeConverterExtender : Component, IExtenderProvider
{
    private BindingConverterSettingsCollection _propertyStorage = [];

    public BindingTypeConverterExtender()
    {
    }

    public bool CanExtend(object extendee)
    {
        if (extendee is IBindableComponent bindableComponent)
        {
            if (!_propertyStorage.TryGetValue(bindableComponent.GetName(), out var bindingConverters))
            {
                bindingConverters = [];
                _propertyStorage.Add(bindableComponent.GetName(), bindingConverters);
            }

            SyncBindingConverterCollection(bindableComponent, bindingConverters);

            return true;
        }

        return false;
    }

    private static void SyncBindingConverterCollection(
        IBindableComponent bindableComponent,
        BindingConverters bindingConverters)
    {
        // Create a set of current binding property names
        var currentBindings = new HashSet<string>(
            bindableComponent.DataBindings.Cast<Binding>().Select(b => b.PropertyName));

        // Remove converter settings for bindings that no longer exist
        var entriesToRemove = bindingConverters.Cast<BindingConverterSetting>()
            .Where(setting => !currentBindings.Contains(setting.PropertyName))
            .ToList();

        foreach (var entry in entriesToRemove)
        {
            bindingConverters.Remove(entry);
        }

        // Add new settings for bindings that are not in the converter collection
        foreach (Binding bindingItem in bindableComponent.DataBindings)
        {
            if (bindingConverters
                .Cast<BindingConverterSetting>()
                .All(setting => setting.PropertyName != bindingItem.PropertyName))
            {
                bindingConverters.Add(
                    new BindingConverterSetting(
                        targetComponent: bindableComponent,
                        propertyName: bindingItem.PropertyName,
                        typeConverterType: null));
            }
        }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public BindingConverterSettingsCollection BindingConverterSettings
    {
        get
        {
            return _propertyStorage.GetUsedItems();
        }

        set => _propertyStorage = value;
    } 

    [ParenthesizePropertyName(true)]
    [DisplayName("BindingConverters")]
    [Description("The collection of binding converters for every bound class.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [TypeConverter(typeof(BindingConvertersConverter))]
    public BindingConverters GetBindingConverters(IBindableComponent bindableComponent)
    {
        //if (!Debugger.IsAttached) Debugger.Launch();

        if (!_propertyStorage.TryGetValue(bindableComponent.GetName(), out BindingConverters? bindingConverters))
        {
            return [];
        }

        return bindingConverters;
    }

    public void SetBindingConverters(IBindableComponent bindableComponent, BindingConverters bindingConverters)
    {
        if (!Debugger.IsAttached) Debugger.Launch();

        if (!_propertyStorage.TryAdd(bindableComponent.GetName(), bindingConverters))
        {
            _propertyStorage[bindableComponent.GetName()] = bindingConverters;
        }
    }
}

public static class ComponentExtensions
{
    public static string GetName(this IComponent component)
    {
        if (component.Site is null)
            throw new InvalidOperationException("Component is not sited.");

        if (component.Site.Name is null)
            throw new InvalidOperationException("Component is not named.");

        return component.Site.Name;
    }
}
