using System.Collections.Generic;

// Contains the data structure foundation of the AutoLayout concept.
namespace WinFormsPowerTools.AutoLayout
{
    // We need this basically only for the mapping to real components/controls.
    public interface IAutoLayoutElement<T> where T : IViewController
    {
        string Name { get; set; }
        object? Tag { get; set; }
        object? Group { get; set; }
    }

    public interface IAutoLayoutContent<T>
        : IAutoLayoutElement<T> where T : IViewController
    {
        IAutoLayoutElement<T>? Content { get; set; }
    }

    public interface IAutoLayoutContainer<T>
        : IAutoLayoutElement<T> where T : IViewController
    {
        ICollection<IAutoLayoutElement<T>>? Children { get; }
    }
}
