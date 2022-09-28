using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutBinding
    {
        public AutoLayoutBinding(string componentPropertyName, string bindingPath)
        {
            ComponentPropertyName = componentPropertyName;
            BindingPath = bindingPath;
        }

        public string ComponentPropertyName { get; set; }
        public string BindingPath { get; set; }
    }
}
