using System.Collections.Generic;
using System.ComponentModel;

// Contains the data structure foundation of the AutoLayout concept.
namespace WinFormsPowerTools.AutoLayout
{
    // We need this basically only for the mapping to real components/controls.
    public interface IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        string Name { get; set; }
        object? Tag { get; set; }
    }

    public interface IAutoLayoutContent<T>
        : IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        IAutoLayoutElement<T>? Content { get; set; }
    }

    public interface IAutoLayoutContainer<T>
        : IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        ICollection<IAutoLayoutElement<T>>? Children { get; }
    }
}
