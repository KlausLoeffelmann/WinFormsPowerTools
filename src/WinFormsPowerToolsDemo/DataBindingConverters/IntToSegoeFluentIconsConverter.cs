using System;
using System.ComponentModel;
using System.Globalization;
using WinForms.PowerTools.Controls;
using static WinForms.PowerTools.Components.BindingTypeConverterExtender;

namespace WinForms.PowerToolsDemo.DataBindingConverters;

[BindingConverter("FluentIconsConverter")]
public class IntToSegoeFluentIconsConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(int) || base.CanConvertTo(context, destinationType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is int intValue)
        {
            if (Enum.IsDefined(typeof(SegoeFluentIcons), intValue))
            {
                return (SegoeFluentIcons)intValue;
            }

            throw new ArgumentOutOfRangeException(nameof(value), $"Cannot convert '{intValue}' to {nameof(SegoeFluentIcons)}.");
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is SegoeFluentIcons enumValue && destinationType == typeof(int))
        {
            return (int)enumValue;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}
