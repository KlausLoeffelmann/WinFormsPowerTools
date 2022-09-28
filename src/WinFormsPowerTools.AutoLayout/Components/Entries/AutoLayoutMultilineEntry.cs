using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutMultilineEntry<T>
        : AutoLayoutTextEntry<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutMultilineEntry(
            string? name = "multilineEntry1",
            string? text = default,
            bool isReadOnly = default,
            params AutoLayoutBinding[] bindings) : base(name, text, isReadOnly, bindings)
        {
        }
    }
}
