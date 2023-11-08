using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    [TypeConverter(typeof(BindingTypeConverterCollectionConverter))]
    public class BindingConverterSettingCollection : Collection<BindingConverterSetting>
    {
        public override string ToString()
        {
            return "(TypeConverters)";
        }
    }
}
