using System.ComponentModel;
using System.Windows.Forms;

namespace DataEntryForms.AutoLayout
{
    public class AutoLayoutComponent
    {
        public AutoLayoutComponent(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Caption { get; set; }
        public string ComponentTypename { get; set; }
        public Padding Margin { get; set; }
        public PropertyDescriptor Binding { get; set; }
    }
}
