using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutComponents<T> where T : IViewController
    {
        public List<AutoLayoutComponent<T>>? Components { get; set; }
        public AutoLayoutComponent<T>? LastComponent { get; set; }
    }
}
