using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTextEntry<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutTextEntry(
            string? name = "textEntryName1",
            string? text = default,
            string? bindingPath = default)
            : base(name, text, bindingPath)
        {
        }

        public bool IsReadOnly { get; set; }
    }
}
