using System.ComponentModel;
using System.Globalization;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
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
}
