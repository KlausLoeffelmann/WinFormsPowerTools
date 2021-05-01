using System.Windows.Input;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutButton<T>
        : AutoLayoutComponent<T> where T : IViewController
    {
        public AutoLayoutButton(string name, string text, ICommand command) : base(name)
        {
            base.Text = text;
        }
    }
}
