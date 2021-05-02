using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGroup<T>
        : AutoLayoutContainer<T> where T : IViewController
    {
        public AutoLayoutGroup(string name, object tag, object group) : base(name, tag, group)
        {
        }

        public override ICollection<IAutoLayoutElement<T>> Children 
            => throw new System.NotImplementedException();
    }
}
