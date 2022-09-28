using System.ComponentModel;
using System.Windows.Input;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutButton<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutButton(
            string? name = "button1",
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
