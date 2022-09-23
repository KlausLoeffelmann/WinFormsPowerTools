using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WinFormsPowerTools.AutoLayout.AutoLayout.Misc;

namespace WinFormsPowerTools.AutoLayout
{
    public partial class AutoLayoutGrid<T>
        : AutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        private ObservableCollection<AutoLayoutComponent<T>> _children;
        private Dictionary<AutoLayoutPosition, (GridInfo gridInfo, IAutoLayoutElement<T> layoutElement)> _griddedChildren = new();

        private AutoLayoutPosition _maxCellPosition;
        private AutoLayoutPosition _currentPosition;

        public AutoLayoutGrid(
            string? name = "grid1", 
            string? bindingPath = default)
            : base(name, bindingPath: bindingPath)
        {
            _children = new ObservableCollection<AutoLayoutComponent<T>>();
            _children.CollectionChanged += Children_CollectionChanged;
        }

        // This gets called when ever the Children collection got changed.
        // So, either items have been added here via AddChild, and in that 
        // case, there is a RowColumnTag Object instead of the original Tag
        // object in the Tag, and we can now synchronize the Row and the
        // column info.
        // Or the children have been added directly. In this case, we assume
        // lastrow+1, column=0, and again, we sync. Nothing to do with the tag then.
        private void Children_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems is null)
            {
                return;
            }
            
            foreach (IAutoLayoutElement<T> item in e.NewItems)
            {
                GridInfo gridInfo;

                if (item.Tag is GridInfo gridInfoItem)
                {
                    gridInfo = gridInfoItem;
                }
                else
                {
                    gridInfo = new GridInfo(_maxCellPosition.Row + 1, 0, 1, 1);
                }

                _griddedChildren?.Add(new AutoLayoutPosition(gridInfo.Row, gridInfo.Column), (gridInfo, item));
            }
        }

        public override ICollection<AutoLayoutComponent<T>> Children
            => _children;

        public void AddComponent(
            int row,
            int column,
            AutoLayoutComponent<T> child,
            int rowSpan = 1,
            int columnSpan = 1)
        {
            // Check, if that cell if already occupied:
            if (_griddedChildren.ContainsKey(new AutoLayoutPosition(row, column)))
            {
                throw new ArgumentException($"Cell {row}/{column} does already exist.");
            }

            // We need to use the element's tag, so we copy that to the GridInfo's tag.
            var rowColumnTag = new GridInfo(row, column, rowSpan, columnSpan);

            // And then use the element's tag to store the GridInfo. Temporarily, until
            // the CollectionChange event occurs in which we sync everything with out
            // internal Layout dictionary.
            child.Tag = rowColumnTag;
            _children.Add(child);
            _maxCellPosition = new AutoLayoutPosition(
                Math.Max(row, _maxCellPosition.Row),
                Math.Max(column, _maxCellPosition.Column));
        }

        public int LastRow => _maxCellPosition.Row;
        public int LastColumn => _maxCellPosition.Column;
    }
}
