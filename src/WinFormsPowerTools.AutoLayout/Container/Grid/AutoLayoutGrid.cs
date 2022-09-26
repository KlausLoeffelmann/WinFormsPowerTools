using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WinFormsPowerTools.AutoLayout
{
    public partial class AutoLayoutGrid<T>
        : AutoLayoutContainer<T> where T : INotifyPropertyChanged
    {
        private Dictionary<AutoLayoutPosition, AutoLayoutComponent<T>> _components = new();
        private Dictionary<AutoLayoutComponent<T>, AutoLayoutFencedPosition> _positionLookup = new();
        private List<AutoLayoutComponent<T>>? _cachedComponents;

        private AutoLayoutFencedPosition _currentPosition;
        private AutoLayoutFencedPosition? _passedGridPosition;

        public AutoLayoutGrid(
            string? name = "grid1",
            string? bindingPath = default,
            AutoLayoutRowDefinitions? rowDefinitions = default,
            AutoLayoutColumnDefinitions? columnDefinitions = default)
            : base(name, bindingPath: bindingPath)
        {
            ColumnDefinitions = columnDefinitions ?? new("Auto", "*:<200");
            RowDefinitions = rowDefinitions ?? new("*");

            _currentPosition = new(0, 0, RowDefinitions.Count, ColumnDefinitions.Count);
        }

        public AutoLayoutFencedPosition? GetFencedPosition(AutoLayoutComponent<T> component)
        {
            if (_positionLookup.TryGetValue(component, out var value))
            {
                return value;
            }

            return null;
        }

        public override IEnumerable<AutoLayoutComponent<T>> Components
        {
            get
            {
                if (_cachedComponents is null) 
                {
                    _cachedComponents= _components.Select(item=>item.Value).ToList();
                }

                return _cachedComponents;
            }
        }

        public void AddComponent(
            AutoLayoutComponent<T> component,
            AutoLayoutFencedPosition? gridPosition)
        {
            _passedGridPosition = gridPosition;
            ((IAutoLayoutContainer<T>)this).AddComponent(component);
        }

        public void AddComponent(
            AutoLayoutComponent<T> component,
            int row,
            int column,
            int rowSpan = 1,
            int columnSpan = 1)
        {
            _passedGridPosition = new AutoLayoutFencedPosition(row, column, rowSpan, columnSpan);
            ((IAutoLayoutContainer<T>)this).AddComponent(component);
        }

        public void AddComponent(
            AutoLayoutComponent<T> component,
            AutoLayoutPosition? gridPosition,
            int rowSpan = 1,
            int columnSpan = 1)
        {
            if (!gridPosition.HasValue)
            {
                _passedGridPosition = new AutoLayoutFencedPosition(_currentPosition.Row, _currentPosition.Column, rowSpan, columnSpan);
            }
            else
            {
                _passedGridPosition = new AutoLayoutFencedPosition(gridPosition.Value.Row, gridPosition.Value.Column, rowSpan, columnSpan);
            }

            ((IAutoLayoutContainer<T>)this).AddComponent(component);
        }

        protected override void OnAddComponent(AutoLayoutComponent<T> component)
        {
            if (!_passedGridPosition.HasValue)
            {
                _currentPosition++;
                if (_currentPosition.Overflew)
                {
                    throw new ArgumentException("Grid is full.");
                }

                _passedGridPosition = new AutoLayoutFencedPosition(_currentPosition.Position, new(1, 1));
            }

            var position = _passedGridPosition.Value.Position;

            // Check, if that cell if already occupied:
            if (_components.ContainsKey(position))
            {
                throw new ArgumentException($"A control at Cell {position.Row}/{position.Column} does already exist.");
            }

            _cachedComponents = null;
            _components.Add(position, component);
            _positionLookup.Add(component, _passedGridPosition.Value);
        }

        public AutoLayoutRowDefinitions RowDefinitions { get; }
        public AutoLayoutColumnDefinitions ColumnDefinitions { get; } 
        public int LastRow => _currentPosition.RowSpan;
        public int LastColumn => _currentPosition.ColumnSpan;
        public int CurrentRow => _currentPosition.Row;
        public int CurrentColumn => _currentPosition.Column;
    }
}
