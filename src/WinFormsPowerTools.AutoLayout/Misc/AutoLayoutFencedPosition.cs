using System;
using System.Diagnostics;

namespace WinFormsPowerTools.AutoLayout
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public record struct AutoLayoutFencedPosition
    {
        private bool _overflew;
        
        public AutoLayoutFencedPosition(int row = 0, int column = 0, int rowSpan = 1, int columnSpan = 1)
        {
            Row = row;
            Column = column;
            RowSpan = rowSpan;
            ColumnSpan = columnSpan;
        }

        public AutoLayoutFencedPosition(AutoLayoutPosition position, AutoLayoutSize size)
        {
            if (size.IsZeroOrSmaller)
            {
                throw new ArgumentException("Size must be greater than zero.", nameof(size));
            }

            Row = position.Row;
            Column = position.Column;
            RowSpan = size.Height;
            ColumnSpan = size.Width;
        }

        public int Row { get; }
        public int Column { get; }
        public int RowSpan { get; }
        public int ColumnSpan { get; }

        public AutoLayoutPosition Position
           => new AutoLayoutPosition(Row, Column);
        
        public static AutoLayoutFencedPosition operator++(AutoLayoutFencedPosition position)
        {
            int column= position.Column;
            int row = position.Row;
            bool overflew = false;
            
            column++;
            if (column >= position.ColumnSpan)
            {
                column = 0;
                
                row++;
                if (row >= position.RowSpan)
                {
                    row = 0;
                    overflew = true;
                }
            }

            return new AutoLayoutFencedPosition(row, column, position.RowSpan, position.ColumnSpan)
            {
                Overflew = overflew
            };
        }
        
        public bool Overflew
        {
            get
            {
                try
                {
                    return _overflew;
                }
                finally
                {
                    _overflew = false;
                }
            }
            
            private set => _overflew = value;
        }

        private string GetDebuggerDisplay()
            => $"Row:{Row} Column:{Column} RowSpan:{RowSpan} ColumnSpan:{ColumnSpan}";
    }
}
