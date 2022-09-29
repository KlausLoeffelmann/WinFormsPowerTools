using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutMenu<T> : AutoLayoutTypedContainer<T, AutoLayoutMenuItem<T>>
        where T : INotifyPropertyChanged
    {
        private List<AutoLayoutComponent<T>>? _components;

        public AutoLayoutMenu(
            string? name = "menu1", 
            params AutoLayoutMenuItem<T>[] menuItems) : base(name)
        {
            foreach (var menuItem in menuItems)
            {
                Add(menuItem);
            }
        }

        public override IEnumerable<AutoLayoutComponent<T>> Components
            => _components ??= new List<AutoLayoutComponent<T>>();

        public override void Add(AutoLayoutMenuItem<T> component)
            => AddComponent(component);

        public override void AddComponent(AutoLayoutComponent<T> component)
        {
            _components ??= new List<AutoLayoutComponent<T>>();
            _components.Add(component);
        }
    }
}
