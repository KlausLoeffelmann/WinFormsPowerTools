using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutContentExtensions
    {
        public static AutoLayoutGrid<T> SetGridContent<T>(
            this IAutoLayoutContent<T> contentContainer,
            string gridName,
            object? tag = null,
            object? group = null) where T : INotifyPropertyChanged
        {
            var grid = new AutoLayoutGrid<T>(gridName, tag, group);
            contentContainer.Content = grid;
            return grid;
        }
    }
}
