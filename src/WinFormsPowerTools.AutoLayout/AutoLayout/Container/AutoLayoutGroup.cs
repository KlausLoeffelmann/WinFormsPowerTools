using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGroup<T>
        : AutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutGroup(string name, object tag, object group) : base(name, tag, group)
        {
        }

        public override ICollection<AutoLayoutComponent<T>> Children 
            => throw new System.NotImplementedException();
    }
}
