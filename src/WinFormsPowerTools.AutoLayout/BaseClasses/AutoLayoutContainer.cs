using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutContainer<T> :
        AutoLayoutComponent<T>, IAutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutContainer(
            string? name = "container1",
            string? text = default,
            string? bindingPath = default) : base(name, text, bindingPath)
        {
        }

        public AutoLayoutPadding Padding { get; set; }
        public abstract ICollection<AutoLayoutComponent<T>>? Children { get; }
    }
}
