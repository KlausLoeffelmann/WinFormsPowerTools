namespace WinFormsPowerTools.AutoLayout
{
    public abstract class AutoLayoutContent<T>
        : IAutoLayoutContent<T> where T : IViewController
    {
        public AutoLayoutContent(
            string name, 
            object? tag = null, 
            object? group = null)
        {
            Name = name;
            Tag = tag;
            Group = group;
        }

        public string Name { get; set; }
        public object? Tag { get; set; }
        public object? Group { get; set; }
        public IAutoLayoutElement<T>? Content { get; set; }
    }

    public class AutoLayoutDocument<T>
        : AutoLayoutContent<T> where T : IViewController
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
            : base(name, tag, group)
        {
            Title = title;
            DataContext = dataContext;
        }

        public string? Title { get; }
        public T? DataContext { get; }
    }

    public static class AutoLayoutDocumentExtension
    {
        public static AutoLayoutDocument<T> GetDocument<T>(
            this T view,
            string name,
            string? title = null,
            object? tag = null,
            object? group = null)
            where T : IViewController
        {
            return new AutoLayoutDocument<T>(
                name, title, view, tag, group);
        }
    }

    public static class AutoLayoutContentExtensions
    {
        public static AutoLayoutGrid<T> SetGridContent<T>(
            this IAutoLayoutContent<T> contentContainer,
            string gridName,
            object? tag = null,
            object? group = null) where T : IViewController
        {
            var grid = new AutoLayoutGrid<T>(gridName, tag, group);
            contentContainer.Content = grid;
            return grid;
        }
    }
}
