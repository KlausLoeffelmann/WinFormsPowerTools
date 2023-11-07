using System.Collections;
using System.ComponentModel;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    public class TypeConverterPropertyDescriptor : PropertyDescriptor
    {
        private readonly Hashtable _values = new();

        public TypeConverterPropertyDescriptor(string name) : base(name, new Attribute[0])
        {
        }

        public override bool CanResetValue(object component) => true;
        public override Type ComponentType => typeof(IExtenderProvider);
        public override bool IsReadOnly => false;
        public override Type PropertyType => typeof(TypeConverter);

        public override string DisplayName => base.DisplayName;

        // Return the associated TypeConverter for the property
        public override TypeConverter Converter => new TypeConverterTypeConverter();

        public override object? GetValue(object? component)
            => component is null
            ? null
            : _values[component];

        public override void SetValue(object? component, object? value)
        {
            if (component is null)
            {
                return;
            }

            _values[component] = value;
            OnValueChanged(component, EventArgs.Empty);
        }

        public override bool ShouldSerializeValue(object component) => _values.ContainsKey(component);

        public override void ResetValue(object component) => _values.Remove(component);
    }
}
