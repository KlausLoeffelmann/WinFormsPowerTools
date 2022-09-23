using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutLabel<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutLabel(
            string? name = "label1",
            string? text = default,
            string? bindingPath = default)
            : base(name, text: text, bindingPath: bindingPath)
        {
        }
    }
}
