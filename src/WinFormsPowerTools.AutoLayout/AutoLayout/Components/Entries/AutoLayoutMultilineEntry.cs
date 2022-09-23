using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutMultilineEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutMultilineEntry(
            string? name = "multilineEntryName1",
            string? text = default,
            string? bindingPath = default)
            : base(name, text: text, bindingPath: bindingPath)
        {
        }
    }
}
