using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutDocumentExtension
    {
        public static AutoLayoutDocument<T> GetDocument<T>(
            this T view,
            string name,
            string? title = null,
            object? tag = null,
            object? group = null)
            where T : INotifyPropertyChanged
        {
            return new AutoLayoutDocument<T>(
                name, title, view, tag, group);
        }
    }
}
