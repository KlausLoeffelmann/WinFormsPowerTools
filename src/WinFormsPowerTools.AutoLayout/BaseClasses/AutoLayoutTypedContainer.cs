using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutTypedContainer<T, U> :
        AutoLayoutComponent<T>, IAutoLayoutContainer<T> 
        where T : INotifyPropertyChanged
        where U : AutoLayoutComponent<T>
    {
        public AutoLayoutTypedContainer(
            string? name = "container1",
            string? text = default,
            params AutoLayoutBinding[] bindings) : base(name, text, bindings) { }

        public AutoLayoutPadding Padding { get; set; }
        public abstract IEnumerable<AutoLayoutComponent<T>> Components { get; }

        public abstract void AddComponent(AutoLayoutComponent<T> component);

        public abstract void Add(U component);
    }
}
