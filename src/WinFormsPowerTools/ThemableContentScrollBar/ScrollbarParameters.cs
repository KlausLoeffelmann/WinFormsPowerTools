using System.Drawing;

namespace WinFormsPowerTools.ThemedScrollBars
{
    public struct ScrollbarParameters
    {
        public int ThumbWidth { get; }
        public int Position { get; }
        public Size ScrollbarSize { get; }
        public float SmallChange { get; }
        public float LargeChange { get; }
        public float Minimum { get; }
        public float Maximum { get; }

        public ScrollbarParameters(int thumbWidth, int position, Size scrollbarSize,
            float smallChange, float largeChange, float minimum, float maximum)
        {
            ThumbWidth = thumbWidth;
            Position = position;
            ScrollbarSize = scrollbarSize;
            SmallChange = smallChange;
            LargeChange = largeChange;
            Minimum = minimum;
            Maximum = maximum;
        }

        public ScrollbarParameters WithPosition(int position)
            => new(ThumbWidth, position, ScrollbarSize, SmallChange, LargeChange, Minimum, Maximum);

        public ScrollbarParameters WithScrollBarSize(Size scrollbarSize)
            => new(ThumbWidth, Position, scrollbarSize, SmallChange, LargeChange, Minimum, Maximum);

        public ScrollbarParameters WithSmallChange(float smallChange)
            => new(ThumbWidth, Position, ScrollbarSize, smallChange, LargeChange, Minimum, Maximum);

        public ScrollbarParameters WithLargeChange(float largeChange)
            => new(ThumbWidth, Position, ScrollbarSize, SmallChange, largeChange, Minimum, Maximum);

        public ScrollbarParameters WithMinimum(float minimum)
            => new(ThumbWidth, Position, ScrollbarSize, SmallChange, LargeChange, minimum, Maximum);

        public ScrollbarParameters WithMaximum(float maximum)
            => new(ThumbWidth, Position, ScrollbarSize, SmallChange, LargeChange, Minimum, maximum);

        public ScrollbarParameters WithThumbWidth(int thumbWidth)
            => new(thumbWidth, Position, ScrollbarSize, SmallChange, LargeChange, Minimum, Maximum);
    }
}
