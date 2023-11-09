using System.ComponentModel;
using System.Diagnostics;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    public class BindingConverterSettingPropertyDescriptor : PropertyDescriptor
    {
        private string _propertyName;
        private IBindableComponent? _targetComponent;

        public BindingConverterSettingPropertyDescriptor( string propertyName, IBindableComponent? targetComponent)
            : base(propertyName, new Attribute[0])
        {
            _propertyName = propertyName;
            _targetComponent = targetComponent;
        }

        public override bool CanResetValue(object component) => true;
        public override Type ComponentType => typeof(IExtenderProvider);
        public override bool IsReadOnly => false;
        public override Type PropertyType => typeof(TypeConverter);

        public override string DisplayName => base.DisplayName;

        // Return the associated TypeConverter for the property
        public override TypeConverter Converter => new TypeConverterTypeConverter();

        public override object? GetValue(object? component)
        {
            if (component is BindingConverterSettingCollection converterSettingCollection)
            {
                var setting = converterSettingCollection.Cast<BindingConverterSetting>()
                    .First(s => s.TargetComponent == _targetComponent &&
                                         s.PropertyName == _propertyName);

                // Return the TypeConverterType, or null if not found
                return setting?.TypeConverterType;
            }

            return null;
        }

        public override void SetValue(object? component, object? value)
        {
        //    if (!Debugger.IsAttached)
        //    {
        //        Debugger.Launch();
        //    }

            if (component is BindingConverterSettingCollection converterSettingCollection)
            {
                // Find the setting within the collection to update
                var settingToUpdate = converterSettingCollection.Cast<BindingConverterSetting>()
                    .First(s => s.TargetComponent == _targetComponent &&
                                         s.PropertyName == _propertyName);

                if (settingToUpdate is not null)
                {
                    if (value is null)
                    {
                        settingToUpdate.TypeConverterType = null;
                    }

                    if (value is Type converterType)
                    {
                        settingToUpdate.TypeConverterType = converterType;
                    }

                    // Notify that the value has been updated
                    OnValueChanged(component, EventArgs.Empty);
                }
            }
        }

        public override bool ShouldSerializeValue(object component) 
            => GetValue(component) is not null;

        public override void ResetValue(object component) 
            => SetValue(component, null);
    }
}
