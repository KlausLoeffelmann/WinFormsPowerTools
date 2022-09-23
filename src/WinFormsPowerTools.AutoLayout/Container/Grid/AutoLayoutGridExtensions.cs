using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutGridExtensions
    {
        public static AutoLayoutGrid<T> AddComponent<T>(
            this AutoLayoutGrid<T> grid,
            AutoLayoutComponent<T> child,
            int row,
            int column,
            int rowSpan = 1,
            int columnSpan = 1) where T : INotifyPropertyChanged
        {
            grid.AddComponent(row, column, child, rowSpan, columnSpan);
            return grid;
        }
    }
}
