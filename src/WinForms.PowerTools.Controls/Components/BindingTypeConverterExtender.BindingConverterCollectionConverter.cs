using System.ComponentModel;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    public class BindingTypeConverterCollectionConverter : ExpandableObjectConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext? context) => true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
        {
            if (value is BindingTypeConverterCollection bindingTypeConverterCollection)
            {
                PropertyDescriptor[] propertyDescriptors = new PropertyDescriptor[bindingTypeConverterCollection.Count];
                for (int i = 0; i < bindingTypeConverterCollection.Count; i++)
                {
                    propertyDescriptors[i] = bindingTypeConverterCollection[i];
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
