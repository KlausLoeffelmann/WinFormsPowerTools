using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGroup<T>
        : AutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        private List<AutoLayoutComponent<T>>? _components;

        public AutoLayoutGroup(
            string? name = "group1",
            string? text = default,
            params AutoLayoutBinding[] bindings)
            : base(name, text, bindings)
        {
        }

        public override IEnumerable<AutoLayoutComponent<T>> Components 
            => _components ??= new List<AutoLayoutComponent<T>>();

        protected override void OnAddComponent(AutoLayoutComponent<T> component)
        {
            _components ??= new List<AutoLayoutComponent<T>>();
            _components.Add(component);
        }
    }
}
