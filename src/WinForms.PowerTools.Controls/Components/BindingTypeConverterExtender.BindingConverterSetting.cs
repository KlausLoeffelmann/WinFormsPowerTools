using System.ComponentModel;

namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    [TypeConverter(typeof(BindingConverterSettingConverter))]
    public class BindingConverterSetting
    {
        public BindingConverterSetting(IBindableComponent targetComponent, string propertyName, Type? typeConverterType)
        {
            TargetComponent = targetComponent;
            PropertyName = propertyName;
            TypeConverterType = typeConverterType;
        }

        public IBindableComponent TargetComponent { get; set; }
        public string PropertyName { get; set; }
        public Type? TypeConverterType { get; set; }
    }
}
