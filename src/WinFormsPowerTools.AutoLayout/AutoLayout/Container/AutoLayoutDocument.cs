using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDocument<T>
        : AutoLayoutContent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDocument(
           string name,
           string title) : this(name, title, default)
        { 
        }
        
        internal AutoLayoutDocument(
            string name,
            string? title,
            T? dataContext,
            object? tag = null,
            object? group = null)
            : base(name, tag)
        {
            Title = title;
            DataContext = dataContext;
        }

        public string? Title { get; }
        public T? DataContext { get; }
    }
}
