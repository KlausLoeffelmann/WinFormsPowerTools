using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutDocumentExtension
    {
        public static AutoLayoutDocument<T> GetDocument<T>(
            this T view,
            string name,
            string? title = default,
            object? tag = default,
            object? group = default)
            where T : INotifyPropertyChanged
        {
            return new AutoLayoutDocument<T>(name, title);
        }
    }
}
