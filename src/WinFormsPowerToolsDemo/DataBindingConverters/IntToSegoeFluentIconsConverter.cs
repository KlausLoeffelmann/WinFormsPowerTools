using System;
using System.ComponentModel;
using System.Globalization;
using WinForms.PowerTools.Controls;
using static WinForms.PowerTools.Components.BindingTypeConverterExtender;

namespace Converters;

public abstract class BindingConverter<FromType, ToType> : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(FromType) || base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(ToType) || base.CanConvertTo(context, destinationType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is FromType fromType)
        {
            try
            {
                return Convert(fromType);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Cannot convert '{fromType}' to {typeof(ToType)}.", ex);
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is ToType toType)
        {
            try
            {
                return ConvertBack(toType);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Cannot convert '{toType}' back to {typeof(FromType)}.", ex);
            }
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }

    protected abstract ToType Convert(FromType fromType);
    protected abstract FromType ConvertBack(ToType toType);

}

[BindingConverter("FluentIconsConverter")]
public class IconsConverter : TypeConverter
{
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

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    => sourceType == typeof(int) || base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(int) || base.CanConvertTo(context, destinationType);
}
