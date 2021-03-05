using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutComponents<T> where T : IFormsController
    {
        public List<AutoLayoutComponent<T>> Components { get; set; }
        public AutoLayoutComponent<T> LastComponent { get; set; }
    }
}
