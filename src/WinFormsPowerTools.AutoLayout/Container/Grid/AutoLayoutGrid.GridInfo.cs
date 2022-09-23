using System.ComponentModel;
using System.Diagnostics;

namespace WinFormsPowerTools.AutoLayout
{
    public partial class AutoLayoutGrid<T> where T : INotifyPropertyChanged
    {
        [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
        public struct GridInfo
        {
            public GridInfo(int row, int column, int rowSpan, int columnSpan)
            {
                Row = row;
                Column = column;
                RowSpan = rowSpan;
                ColumnSpan = columnSpan;
            }

            public int Row { get; }
            public int Column { get; }
            public int RowSpan { get; }
            public int ColumnSpan { get; }

            private string GetDebuggerDisplay()
                => $"Row:{Row} Column:{Column} RowSpan:{RowSpan} ColumnSpan:{ColumnSpan}";
        }
    }
}
