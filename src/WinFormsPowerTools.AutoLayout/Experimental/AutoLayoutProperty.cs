using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    /// <summary>
    /// Just as an idea.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutoLayoutProperty<T> where T : class
    {
        public AutoLayoutProperty(T @object, string? propertyname)
        {
            PropertyName = PropertyName;
        }

        public string? PropertyName { get; }

        public AutoLayoutPropertyDescriptor? PropertyDescriptor
        {
            get
            {
                if (!string.IsNullOrEmpty(PropertyName))
                {
                    return new AutoLayoutPropertyDescriptor(TypeDescriptor.GetProperties(typeof(T))[PropertyName]!, null);
                }

                return null;
            }
        }
    }
}
