using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutMenuItem<T>
     : AutoLayoutTypedContainer<T, AutoLayoutMenuItem<T>>
        where T : INotifyPropertyChanged
    {
        private List<AutoLayoutComponent<T>>? _components;

        public AutoLayoutMenuItem(
            string? name = "menuEntry1",
            string? text = default,
            ICommand? command = default,
            bool isEnabled = true,
            bool isChecked = false,
            params AutoLayoutBinding[] bindings)
            : base(name, text, bindings: bindings)
        {
            Command = command;
        }

        public ICommand? Command { get; set; }
        public bool IsChecked { get; set; }

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
