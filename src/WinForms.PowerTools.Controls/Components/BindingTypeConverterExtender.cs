using System.ComponentModel;
using System.Globalization;

namespace WinForms.PowerTools.Components;

// Usage in an extender provider:
[ProvideProperty("BindingTypeConverters", typeof(IBindableComponent))]
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

            return new([new BindingConverterSettingPropertyDescriptor(converterSetting)]);
        }
    }

    [TypeConverter(typeof(BindingConverterSettingConverter))]
    public class BindingConverterSetting
    {
        public BindingConverterSetting(string propertyName, Type? typeConverterType)
        {
            PropertyName = propertyName;
            TypeConverterType = typeConverterType;
        }

        public string PropertyName { get; set; }
        public Type? TypeConverterType { get; set; }
    }

    public BindingTypeConverterExtender()
    {
    }

    public bool CanExtend(object extendee)
    {
        if (extendee is IBindableComponent bindableComponent)
        {
            if (!_propertyStorage.ContainsKey(bindableComponent))
            {
                var bindingTypeConverterCollection = new BindingConverterSettingCollection();
                foreach (Binding bindingItem in bindableComponent.DataBindings)
                {
                    bindingTypeConverterCollection.Add(new BindingConverterSetting(bindingItem.PropertyName, null));
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
    public BindingConverterSettingCollection GetBindingTypeConverters(IBindableComponent bindableComponent)
    {
        if (_propertyStorage.TryGetValue(bindableComponent, out BindingConverterSettingCollection? bindingConverterCollection))
        {
            return bindingConverterCollection;
        }

        return [];
    }

    public void SetBindingTypeConverters(IBindableComponent bindableComponent, BindingConverterSettingCollection bindingTypeConverterCollection)
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
