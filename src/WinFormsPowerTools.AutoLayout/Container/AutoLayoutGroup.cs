using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGroup<T>
        : AutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutGroup(string? name = "group1", string? bindingPath = default)
            : base(name, bindingPath: bindingPath)
        {
        }

        public override ICollection<AutoLayoutComponent<T>> Children
            => throw new System.NotImplementedException();
    }
}
