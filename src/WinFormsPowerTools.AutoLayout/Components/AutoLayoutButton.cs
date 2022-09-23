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
            string? bindingPath = default,
            ICommand? command = default) : base(name, text: text, bindingPath: bindingPath)
        {
            Command = command;
        }

        public ICommand? Command { get; set; }
    }
}
