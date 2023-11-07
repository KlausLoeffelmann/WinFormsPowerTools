using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    [TypeConverter(typeof(BindingTypeConverterCollectionConverter))]
    public class BindingTypeConverterCollection : Collection<TypeConverterPropertyDescriptor>
    {
        public override string ToString()
        {
            return "(TypeConverters)";
        }
    }
}
