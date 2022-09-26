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

        public override IEnumerable<AutoLayoutComponent<T>> Components 
            => throw new System.NotImplementedException();

        protected override void OnAddComponent(AutoLayoutComponent<T> component)
        {
            throw new System.NotImplementedException();
        }
    }
}
