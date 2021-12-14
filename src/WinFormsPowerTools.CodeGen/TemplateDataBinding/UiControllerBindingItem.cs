namespace System.Windows.Forms.TemplateBinding
{
    public class UiControllerBindingItem
    {
        public UiControllerBindingItem()
        {
            Name = "Name1";
        }

        public string? BindableComponentPropertyPath { get; set; }
        public string? UiControllerPropertyPath { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
