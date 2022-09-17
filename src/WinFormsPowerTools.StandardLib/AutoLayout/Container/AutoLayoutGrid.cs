using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WinFormsPowerTools.AutoLayout
{
    public class AutoLayoutGrid<T>
        : AutoLayoutContainer<T> where T : IViewController
    {
        private ObservableCollection<IAutoLayoutElement<T>> _children;
        private Dictionary<(int row, int column), (GridInfo gridInfo, IAutoLayoutElement<T> layoutElement)> _griddedChildren = new();

        private (int lastRow, int lastColumn) _maxCellPosition;

        public AutoLayoutGrid(string name, object? tag, object? group)
            : base(name, tag, group)
        {
            _children = new ObservableCollection<IAutoLayoutElement<T>>();
            _children.CollectionChanged += Children_CollectionChanged;
        }

        // This gets called when ever the Children collection got changed.
        // So, either items have been added here via AddChild, and in that 
        // case, there is a RowColumnTag Object instead of the original Tag
        // object in the Tag, and we can now synchronize the Row and the
        // column info.
        // Or the children have been added directly. In this case, we assume
        // lastrow+1, column=0, and again, we sync. Nothing to do with the tag then.
        private void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (IAutoLayoutElement<T> item in e.NewItems)
            {
                GridInfo gridInfo;

                if (item.Tag is GridInfo gridInfoItem)
                {
                    item.Tag = gridInfoItem.Tag;
                    gridInfo = gridInfoItem;
                }
                else
                {
                    gridInfo = new GridInfo(_maxCellPosition.lastRow + 1, 0, 1, 1, default);
                }

                _griddedChildren?.Add((gridInfo.Row, gridInfo.Column), (gridInfo, item));
            }
        }

        public override ICollection<IAutoLayoutElement<T>> Children
            => _children;

        public void AddChild(
            int row,
            int column,
            IAutoLayoutElement<T> child,
            int rowSpan = 1,
            int columnSpan = 1)
        {
            // Check, if that cell if already occupied:
            if (_griddedChildren.ContainsKey((row, column)))
            {
                throw new ArgumentException($"Cell {row}/{column} does already exist.");
            }

            // We need to use the element's tag, so we copy that to the GridInfo's tag.
            var rowColumnTag = new GridInfo(row, column, rowSpan, columnSpan, child.Tag);

            // And then use the element's tag to store the GridInfo. Temporarily, until
            // the CollectionChange event occurs in which we sync everything with out
            // internal Layout dictionary.
            child.Tag = rowColumnTag;
            _children.Add(child);
            _maxCellPosition = (
                Math.Max(row, _maxCellPosition.lastRow),
                Math.Max(column, _maxCellPosition.lastColumn));
        }

        public int LastRow => _maxCellPosition.lastRow;
        public int LastColumn => _maxCellPosition.lastColumn;

        [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
        public class GridInfo
        {
            public GridInfo(int row, int column, int rowSpan, int columnSpan, object? tag = null)
            {
                Row = row;
                Column = column;
                RowSpan = rowSpan;
                ColumnSpan = columnSpan;
                Tag = tag;
            }

            public int Row { get; }
            public int Column { get; }
            public int RowSpan { get; }
            public int ColumnSpan { get; }
            public object? Tag { get; }

            private string GetDebuggerDisplay()
                => $"Row:{Row} Column:{Column} Tag:{Tag ?? "N/D"}";
        }
    }

    public static class AutoLayoutGridExtensions
    {
        public static AutoLayoutGrid<T> AddRow<T>(
            this AutoLayoutGrid<T> grid,
            params IAutoLayoutElement<T>[] children) where T : IViewController
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
            int columnSpan = 1) where T : IViewController
        {
            grid.AddChild(child, row, column, rowSpan, columnSpan);
            return grid;
        }
    }
}
