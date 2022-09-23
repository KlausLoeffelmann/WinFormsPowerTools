using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutDocument<T>
        : AutoLayoutComponent<T> where T : INotifyPropertyChanged
    {
        public AutoLayoutDocument(
           string? name = "document1",
           string? title = "document1",
           AutoLayoutComponent<T>? content = default,
           T? dataContext = default) : base(name)
        {
            Title = title;
            DataContext = dataContext;
            Content = content;
        }

        public string? Title { get; }
        public AutoLayoutComponent<T>? Content { get; set; }
    }
}
