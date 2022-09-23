using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDocument<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDocument(
           string name,
           string title) : this(name, title, default)
        { 
        }

        internal AutoLayoutDocument(
            string? name = default,
            string? title = default,
            T? dataContext = default) : base(name)
        {
            Title = title;
            DataContext = dataContext;
        }

        public string? Title { get; }
        public AutoLayoutComponent<T>? Content { get; set; }
    }
}
