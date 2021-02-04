using System.ComponentModel;

namespace DataEntryForms.AutoLayout
{
    public class AutoLayoutProperty<T> where T : class
    {
        public AutoLayoutProperty(object @object, string propertyname)
        {
            PropertyName = PropertyName;
        }

        public string PropertyName { get; }

        public AutoLayoutPropertyDescriptor PropertyDescriptor
        {
            get
            {
                return new AutoLayoutPropertyDescriptor(TypeDescriptor.GetProperties(typeof(T))[PropertyName], null);
            }
        }
    }
}
