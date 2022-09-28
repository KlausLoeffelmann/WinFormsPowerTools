using System;
using System.ComponentModel;

namespace WinFormsPowerTools.AutoLayout
{
    public static class AutoLayoutGridExtensions
    {
        public static AutoLayoutGrid<T> AddComponent<T>(
            this AutoLayoutGrid<T> grid,
            AutoLayoutComponent<T> component,
            AutoLayoutFencedPosition? position = default) where T : INotifyPropertyChanged
        {
            grid.AddComponent(component, position);
            return grid;
        }

        public static AutoLayoutGrid<T> AddLabel<T>(
            this AutoLayoutGrid<T> grid,
            int row,
            int column,
            string? name = "label1",
            string? text = default,
            int rowSpan = 1,
            int columnSpan = 1,
            params AutoLayoutBinding[] bindings) where T : INotifyPropertyChanged
        {
            var label = new AutoLayoutLabel<T>(name: name, text: text, bindings: bindings);
            grid.AddComponent(label, row, column, rowSpan, columnSpan);
            return grid;
        }

        public static AutoLayoutGrid<T> AddTextEntry<T>(
            this AutoLayoutGrid<T> grid,
            int row,
            int column,
            string? name = "textEntry1",
            string? text = default,
            int rowSpan = 1,
            int columnSpan = 1,
            params AutoLayoutBinding[] bindings) where T : INotifyPropertyChanged
        {
            var textEntry = new AutoLayoutTextEntry<T>(name: name, text: text, bindings: bindings);
            grid.AddComponent(textEntry, row, column, rowSpan, columnSpan);
            return grid;
        }

        public static AutoLayoutGrid<T> AddDateEntry<T>(
            this AutoLayoutGrid<T> grid,
            int row,
            int column,
            string? name = "textEntry1",
            DateTime? value = default,
            int rowSpan = 1,
            int columnSpan = 1,
            params AutoLayoutBinding[] bindings) where T : INotifyPropertyChanged
        {
            var dateEntry = new AutoLayoutDateEntry<T>(name: name, value: value, bindings: bindings);
            grid.AddComponent(dateEntry, row, column, rowSpan, columnSpan);
            return grid;
        }
    }
}
