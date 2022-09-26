using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutDocumentExtension
    {
        public static AutoLayoutDocument<T> GetDocument<T>(
            this T view,
            string name,
            string? title = default,
            object? tag = default)
            where T : INotifyPropertyChanged
        {
            return new AutoLayoutDocument<T>(
                name: name,
                title: title,
                dataContext: view)
            {
                Tag = tag
            };
        }

        public static AutoLayoutDocument<T> SetTitle<T>(
            this AutoLayoutDocument<T> document, 
            string title)
            where T : INotifyPropertyChanged
        {
            document.Title = title;
            return document;
        }

        public static AutoLayoutDocument<T> SetDataContext<T>(
            this AutoLayoutDocument<T> document,
            T? dataContext)
            where T : INotifyPropertyChanged
        {
            document.DataContext = dataContext;
            return document;
        }

        public static AutoLayoutGrid<T> SetGrid<T>(
            this AutoLayoutDocument<T> document,
            AutoLayoutColumnDefinitions columnDefinitions,
            AutoLayoutRowDefinitions? rowDefinitions=default,
            AutoLayoutPadding? padding = default)
            where T : INotifyPropertyChanged
        {
            rowDefinitions ??= AutoLayoutRowDefinitions.Default;

            padding ??= AutoLayoutPadding.Default;

            var grid = new AutoLayoutGrid<T>(
                columnDefinitions: columnDefinitions,
                rowDefinitions: rowDefinitions);
            
            grid.Padding = padding.Value;
            document.Content = grid;
            
            return (AutoLayoutGrid<T>)document.Content;
        }
    }
}
