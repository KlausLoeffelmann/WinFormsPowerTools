using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutLabel<T> 
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutLabel(string name, string text) : base(name)
        {
            base.Text = text;
        }
    }
}
