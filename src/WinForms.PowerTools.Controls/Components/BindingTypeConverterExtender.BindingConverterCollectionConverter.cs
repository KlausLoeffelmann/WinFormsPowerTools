using System.ComponentModel;
using System.Globalization;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    public class BindingConvertersConverter : ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            if (value is BindingConverters bindingConverters)
            {
                PropertyDescriptor[] propertyDescriptors = new PropertyDescriptor[bindingConverters.Count];
                for (int i = 0; i < bindingConverters.Count; i++)
                {
                    propertyDescriptors[i] = new BindingConverterSettingPropertyDescriptor(
                        bindingConverters[i].PropertyName,
                        bindingConverters[i].TargetComponent);
                }

                return new PropertyDescriptorCollection(propertyDescriptors);
            }

            // Return the Property Descriptors.
            return base.GetProperties(context, value, attributes);
        }

        public override string ToString()
        {
            return "(Binding Converters)";
        }
    }
}
