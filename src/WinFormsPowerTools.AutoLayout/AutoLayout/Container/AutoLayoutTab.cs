using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutTab<T> : AutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutTab(string name, object tag, object group) : base(name, tag, group)
        {
        }

        public override ICollection<IAutoLayoutElement<T>> Children 
            => throw new System.NotImplementedException();
    }
}
