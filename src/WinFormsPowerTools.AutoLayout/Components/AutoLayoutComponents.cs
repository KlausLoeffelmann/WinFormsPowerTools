using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutComponents<T> where T : INotifyPropertyChanged
    {
        public List<AutoLayoutComponent<T>>? Components { get; set; }
        public AutoLayoutComponent<T>? LastComponent { get; set; }
    }
}
