using System.ComponentModel;
using System.Windows.Input;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutButton<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutButton(string name, string text, ICommand command) : base(name)
        {
            base.Text = text;
        }
    }
}
