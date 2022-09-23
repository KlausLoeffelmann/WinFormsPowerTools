namespace WinFormsPowerTools.AutoLayout.AutoLayout.Misc
{
    internal record struct AutoLayoutPosition
    {
        public AutoLayoutPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }
    }
}
