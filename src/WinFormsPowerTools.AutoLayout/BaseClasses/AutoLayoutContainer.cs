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
            params AutoLayoutBinding[] bindings) : base(name, text, bindings) { }

        public AutoLayoutPadding Padding { get; set; }

        public abstract IEnumerable<AutoLayoutComponent<T>> Components { get; }

        protected abstract void OnAddComponent(AutoLayoutComponent<T> component);

        void IAutoLayoutContainer<T>.AddComponent(AutoLayoutComponent<T> component, string? bindingPath)
        {
            component.BindingPath = bindingPath;
            OnAddComponent(component);
        }
    }
}
