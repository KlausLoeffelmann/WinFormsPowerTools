namespace WinForms.PowerTools.Components;

public partial class BindingTypeConverterExtender
{
    public class BindingConverterAttribute : Attribute
    {
        public BindingConverterAttribute(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
    }
}
