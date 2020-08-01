using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.DataEntryForms.EntryFormatters
{
    public class DataEntryFormatterTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return "(FormattingProperties)";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (value.GetType().GetInterface("IDataEntryFormatter`1") != null)
            {
                return TypeDescriptor.GetProperties(value, null);
            }

            return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
