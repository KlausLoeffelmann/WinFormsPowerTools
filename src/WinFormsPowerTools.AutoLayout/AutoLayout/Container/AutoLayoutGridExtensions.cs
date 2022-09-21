using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutGridExtensions
    {
        public static AutoLayoutGrid<T> AddRow<T>(
            this AutoLayoutGrid<T> grid,
            params IAutoLayoutElement<T>[] children) where T : INotifyPropertyChanged
        {
            var row = grid.LastRow + 1;
            int column = 0;

            foreach (var child in children)
            {
                grid.AddChild(row, column++, child);
            }

            return grid;
        }

        public static AutoLayoutGrid<T> AddChild<T>(
            this AutoLayoutGrid<T> grid,
            IAutoLayoutElement<T> child,
            int row,
            int column,
            int rowSpan = 1,
            int columnSpan = 1) where T : INotifyPropertyChanged
        {
            grid.AddChild(child, row, column, rowSpan, columnSpan);
            return grid;
        }
    }
}
