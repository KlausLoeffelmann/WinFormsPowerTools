using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace WinForms.PowerTools.Components;

// Usage in an extender provider:
[ProvideProperty("BindingConverterSettings", typeof(IBindableComponent))]
public partial class BindingTypeConverterExtender : Component, IExtenderProvider
{
    private readonly Dictionary<IBindableComponent, BindingConverterSettingCollection> _propertyStorage = [];

    public class BindingConverterSettingConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
            => destinationType == typeof(string) && value is BindingConverterSetting converterSetting
                ? $"{converterSetting.PropertyName} ({converterSetting.TypeConverterType?.GetType().Name})"
                : base.ConvertTo(context, culture, value, destinationType);

        public override PropertyDescriptorCollection GetProperties(
            ITypeDescriptorContext? context,
            object? value,
            Attribute[]? attributes)
        {
            if (value is not BindingConverterSetting converterSetting)
            {
                return new PropertyDescriptorCollection([]);
            }

            return new([new BindingConverterSettingPropertyDescriptor(
                converterSetting.PropertyName,
                converterSetting.TargetComponent)]);
        }
    }

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
        BindingConverterSettingCollection bindingTypeConverterCollection)
    {
        // Create a set of current binding property names
        var currentBindings = new HashSet<string>(
            bindableComponent.DataBindings.Cast<Binding>().Select(b => b.PropertyName));

        // Remove converter settings for bindings that no longer exist
        var entriesToRemove = bindingTypeConverterCollection
            .Where(setting => !currentBindings.Contains(setting.PropertyName))
            .ToList();

        foreach (var entry in entriesToRemove)
        {
            bindingTypeConverterCollection.Remove(entry);
        }

        // Add new settings for bindings that are not in the converter collection
        foreach (Binding bindingItem in bindableComponent.DataBindings)
        {
            if (bindingTypeConverterCollection.All(setting => setting.PropertyName != bindingItem.PropertyName))
            {
                bindingTypeConverterCollection.Add(
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

    private bool ShouldSerializeBindingConverterSettings(IBindableComponent bindableComponent)
    {
        if (_propertyStorage.TryGetValue(bindableComponent, out BindingConverterSettingCollection? bindingConverterCollection))
        {
            return bindingConverterCollection.Count > 0;
        }

        return false;
    }
}
