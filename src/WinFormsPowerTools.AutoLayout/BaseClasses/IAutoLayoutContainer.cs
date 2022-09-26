using System.Collections.Generic;
using System.ComponentModel;

// Contains the data structure foundation of the AutoLayout concept.
namespace WinFormsPowerTools.AutoLayout
{
    public interface IAutoLayoutContainer<T>
        : IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        IEnumerable<AutoLayoutComponent<T>> Components { get; }
        void AddComponent(AutoLayoutComponent<T> component, string? bindingPath = default);
        AutoLayoutPadding Padding { get; set; }
    }
}
