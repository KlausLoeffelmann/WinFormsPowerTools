using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    [TypeConverter(typeof(BindingTypeConverterCollectionConverter))]
    public class BindingConverterSettingCollection : Collection<BindingConverterSetting>
    {
        //public void Add(BindingConverterSetting bindingConverterSetting)
        //{
        //    List.Add(bindingConverterSetting);
        //}

        //public void Remove(BindingConverterSetting bindingConverterSetting)
        //{
        //    List.Remove(bindingConverterSetting);
        //}

        //public BindingConverterSetting this[int index]
        //{
        //    get => (BindingConverterSetting)List[index]!;
        //    set => List[index] = value;
        //}

        public override string ToString()
        {
            return "(TypeConverters)";
        }
    }
}
