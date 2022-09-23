using System.ComponentModel;

// Contains the data structure foundation of the AutoLayout concept.
namespace WinFormsPowerTools.AutoLayout
{
    // We need this basically only for the mapping to real components/controls.
    public interface IAutoLayoutElement<T> where T : INotifyPropertyChanged
    {
        string? Name { get; set; }
        string? BindingPath { get; set; }
        T? DataContext { get; set; } 
        object? Tag { get; set; }
    }
}
