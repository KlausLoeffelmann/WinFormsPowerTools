using System.ComponentModel;

namespace WinForms.PowerTools.Controls;

internal class SortedEnumConverter : EnumConverter
{
    public SortedEnumConverter(Type typeOfEnum) : base(typeOfEnum) { }

    public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext? context)
    {
        var values = Enum.GetValues(EnumType).Cast<Enum>();
        var sortedValues = values.OrderBy(e => e.ToString()).ToArray();

        return new StandardValuesCollection(sortedValues);
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext? context) => true;

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext? context) => true;
}
