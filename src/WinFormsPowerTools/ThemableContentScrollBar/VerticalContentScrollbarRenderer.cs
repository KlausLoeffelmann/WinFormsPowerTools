    using System;
    using System.Drawing;

    namespace WinFormsPowerTools.ThemedScrollBars
    {
        public struct VsThumbInfo
        {
            public int ThumbHeight { get; set; }
            public int ThumbY { get; set; }
        }

    public class VerticalContentScrollbarRenderer
    {
        private static readonly Color s_DarkTrackColor = Color.FromArgb(55, 55, 55);
        private static readonly Color s_LightTrackColor = Color.FromArgb(200, 200, 200);
        private static readonly Color s_DarkThumbColor = Color.FromArgb(80, 80, 80);
        private static readonly Color s_LightThumbColor = Color.FromArgb(220, 220, 220);

        public bool IsDarkMode { get; private set; }
        public ScrollbarParameters Parameters { get; private set; }
        public HoverArea MouseOverArea { get; private set; }
        public float ThumbValue { get; private set; }

        public void Update(
            ScrollbarParameters? parameters = default,
            bool? isDarkMode = default,
            HoverArea? hoverArea = default,
            float? thumbValue = default)
        {
            if (parameters.HasValue) Parameters = parameters.Value;
            if (hoverArea.HasValue) MouseOverArea = hoverArea.Value;
            if (thumbValue.HasValue) ThumbValue = thumbValue.Value;
            if (isDarkMode.HasValue) IsDarkMode = isDarkMode.Value;
        }

        public VerticalContentScrollbarRenderer(bool isDarkMode, ScrollbarParameters parameters)
        {
            IsDarkMode = isDarkMode;
            Parameters = parameters;
            MouseOverArea = HoverArea.None;
        }

        private Color BaseTrackColor() => IsDarkMode ? s_DarkTrackColor : s_LightTrackColor;

        private Color BaseThumbColor() => IsDarkMode ? s_DarkThumbColor : s_LightThumbColor;

        private Color HighlightColor() => IsDarkMode
            ? Color.FromArgb(90, 90, 90)
            : Color.FromArgb(230, 230, 230);

        private Color ArrowColor(bool isLeftArrow)
        {
            var baseArrowColor = IsDarkMode ? s_LightTrackColor : s_DarkTrackColor;
            var highlightColor = IsDarkMode
                ? Color.FromArgb(210, 210, 210)
                : Color.FromArgb(45, 45, 45);

            return (isLeftArrow && MouseOverArea == HoverArea.LeftUpArrow) ||
                   (!isLeftArrow && MouseOverArea == HoverArea.DownRightArrow)
                ? highlightColor
                : baseArrowColor;
        }

        public void Render(Graphics g, int value)
        {
            // 1. Draw track
            DrawTrack(g);

            // 2. Calculate and draw the thumb
            DrawThumb(g, value);

            // 3. Calculate arrow button positions and draw them
            var upArrowRect = new Rectangle(
                0,
                Parameters.Position,
                Parameters.ScrollbarSize.Width,
                Parameters.ThumbWidth);

            var downArrowRect = new Rectangle(
                0,
                Parameters.Position + Parameters.ScrollbarSize.Height - Parameters.ThumbWidth,
                Parameters.ScrollbarSize.Width,
                Parameters.ThumbWidth);

            DrawArrowButton(g, upArrowRect, true);
            DrawArrowButton(g, downArrowRect, false);
        }

        public void DrawTrack(Graphics g)
        {
            var trackColor = MouseOverArea == HoverArea.Track ? HighlightColor() : BaseTrackColor();

            using var brush = new SolidBrush(trackColor);
            g.FillRectangle(
                brush, 
                new Rectangle(
                    new Point(0, Parameters.Position), 
                    Parameters.ScrollbarSize));
        }

        public VsThumbInfo GetThumbInfo(float value)
        {
            if (value < Parameters.Minimum || value > Parameters.Maximum)
                throw new ArgumentException("Value is out of range");

            var availableHeight = Parameters.ScrollbarSize.Height - 2 * Parameters.ThumbWidth;

            var thumbHeight = (int)((float)availableHeight
                    * Parameters.LargeChange
                    / (Parameters.Maximum - Parameters.Minimum));

            var thumbY = Parameters.Position + Parameters.ThumbWidth
                    + (int)((float)(availableHeight - thumbHeight)
                    * (value - Parameters.Minimum)
                    / (Parameters.Maximum - Parameters.Minimum));

            return new VsThumbInfo { ThumbHeight = thumbHeight, ThumbY = thumbY };
        }

        public void DrawThumb(Graphics g, int value)
        {
            var thumbInfo = GetThumbInfo(value);

            var thumbColor = MouseOverArea == HoverArea.Thumb ? HighlightColor() : BaseThumbColor();

            using var brush = new SolidBrush(thumbColor);

            g.FillRectangle(
                brush,
                new Rectangle(
                    new Point(0, thumbInfo.ThumbY),
                    new Size(Parameters.ThumbWidth, thumbInfo.ThumbHeight)));
        }

        public void DrawArrowButton(Graphics g, Rectangle rect, bool isUpArrow)
        {
            var arrowColor = ArrowColor(isUpArrow);

            using var brush = new SolidBrush(arrowColor);
            DrawArrowShape(g, brush, rect, isUpArrow);  // Now using DrawArrowShape for drawing
        }

        private void DrawArrowShape(Graphics g, SolidBrush brush, Rectangle rect, bool isUpArrow, float? scaleFactor = null)
        {
            var middleX = rect.Left + rect.Width / 2;
            var middleY = rect.Top + rect.Height / 2;

            scaleFactor ??= Math.Min(rect.Width, rect.Height) / 10.0f;
            var arrowSize = (int)(4 * scaleFactor.Value);

            Point[] points = isUpArrow
                ? new[]
                {
                        new Point(middleX, middleY - arrowSize),
                        new Point(middleX - arrowSize, middleY + arrowSize),
                        new Point(middleX + arrowSize, middleY + arrowSize)
                }
                : new[]
                {
                        new Point(middleX, middleY + arrowSize),
                        new Point(middleX - arrowSize, middleY - arrowSize),
                        new Point(middleX + arrowSize, middleY - arrowSize)
                };

            g.FillPolygon(brush, points);
        }
    }
}
