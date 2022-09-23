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
            string? bindingPath = default,
            ICommand? command = default) : base(name, text: text, bindingPath: bindingPath)
        {
            Command = command;
        }

        public ICommand? Command { get; set; }
    }
}
