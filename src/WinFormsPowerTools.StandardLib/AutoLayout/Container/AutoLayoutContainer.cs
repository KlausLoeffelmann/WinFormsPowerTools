using System.Collections.Generic;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutContainer<T> : 
        IAutoLayoutContainer<T> where T : IViewController
    {
        public AutoLayoutContainer(string name, object? tag, object? group)
        {
            Name = name;
            Tag = tag;
            Group = group;
        }

        public virtual string Name { get; set; }
        public virtual object? Tag { get; set; }
        public virtual object? Group { get; set; }
        public AutoLayoutPadding Padding { get; set; }
        public abstract ICollection<IAutoLayoutElement<T>>? Children { get; }
    }
}
