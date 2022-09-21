using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutLabeledComponent<T, U> : AutoLayoutComponent<T>
        where T : INotifyPropertyChanged 
        where U : AutoLayoutComponent<T>
    {
        public AutoLayoutLabeledComponent(AutoLayoutLabel<T> label, U component) : base(label.Name)
        {
            Label = label;
            Component = component;
        }

        public AutoLayoutLabeledComponent(string labelname, string labelText, U component) : base(labelname)
        {
            Label = new(labelname, labelText);
            Component = component;
        }

        public AutoLayoutLabel<T> Label { get; set; }
        public U Component { get; set; }
    }
}
