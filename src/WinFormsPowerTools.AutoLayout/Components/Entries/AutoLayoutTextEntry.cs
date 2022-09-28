using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTextEntry<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutTextEntry(
            string? name = "textEntry1",
            string? text = default,
            bool isReadOnly = default,
            params AutoLayoutBinding[] bindings) : base(name, text, bindings)
        {
            IsReadOnly = isReadOnly;
        }

        public bool IsReadOnly { get; init; }
    }
}
