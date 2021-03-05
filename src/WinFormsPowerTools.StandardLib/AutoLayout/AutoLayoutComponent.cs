using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutComponent<T> where T : IFormsController
    {
        public AutoLayoutComponent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Caption { get; set; }
        public string ComponentTypename { get; set; }
        public AutoLayoutPadding Margin { get; set; }
        public PropertyDescriptor Binding { get; set; }
    }
}
