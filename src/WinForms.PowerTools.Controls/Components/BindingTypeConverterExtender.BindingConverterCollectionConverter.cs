using System.ComponentModel;
using System.Globalization;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    public class BindingTypeConverterCollectionConverter : ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            if (value is BindingConverterSettingCollection bindingTypeConverterCollection)
            {
                PropertyDescriptor[] propertyDescriptors = new PropertyDescriptor[bindingTypeConverterCollection.Count];
                for (int i = 0; i < bindingTypeConverterCollection.Count; i++)
                {
                    propertyDescriptors[i] = new BindingConverterSettingPropertyDescriptor(
                        bindingTypeConverterCollection[i].PropertyName,
                        bindingTypeConverterCollection[i].TargetComponent);
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
