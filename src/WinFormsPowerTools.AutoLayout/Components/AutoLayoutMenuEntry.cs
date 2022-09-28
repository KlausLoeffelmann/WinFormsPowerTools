using System.ComponentModel;
using System.Windows.Input;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutMenuEntry<T>
    : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutMenuEntry(
            string? name = "menuEntry1",
            string? text = default,
            ICommand? command = default,
            params AutoLayoutBinding[] bindings)
            : base(name, text, bindings: bindings)
        {
            Command = command;
        }

        public ICommand? Command { get; set; }
    }
}
