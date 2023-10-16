using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsPowerTools.ThemedScrollBars
{
    public class ThemedVerticalScrollbar : Control
    {
        public event EventHandler ValueChanged;
        public event EventHandler IsDarkModeChanged;

        private VerticalContentScrollbarRenderer _renderer;
        private bool _isDragging;
        private int _dragOffset;
        private int _thumbValue;
        private float _value;
        private bool _isDarkMode = false;

        public ThemedVerticalScrollbar() : base()
        {
            _renderer = new VerticalContentScrollbarRenderer(
                isDarkMode: false,
                parameters: new ScrollbarParameters(
                    thumbWidth: SystemInformation.VerticalScrollBarThumbHeight,
                    position: 0,
                    scrollbarSize: new Size(SystemInformation.VerticalScrollBarWidth, Size.Height),
                    smallChange: 10,
                    largeChange: 100,
                    minimum: 0,
                    maximum: 300));

            IsDarkMode = false;
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            _renderer.Update(_renderer.Parameters
                .WithScrollBarSize(new Size(SystemInformation.VerticalScrollBarWidth, Size.Height)));
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScrollbarParameters Parameters
        {
            get => _renderer.Parameters;
            set => _renderer.Update(parameters: value);
        }

        [DefaultValue(false)]
        public bool IsDarkMode
        {
            get => _renderer.IsDarkMode;

            set
            {
                if (_renderer.IsDarkMode == value)
                {
                    return;
                }

                _renderer.Update(isDarkMode: value);
                OnIsDarkModeChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnIsDarkModeChanged(EventArgs e)
        {
            IsDarkModeChanged?.Invoke(this, e);
        }

        public float Value
        {
            get => _value;
            set
            {
                if (value==_value)
                {
                    return;
                }

                if (value < Parameters.Minimum || value > Parameters.Maximum)
                    throw new ArgumentException("Value is out of range");

                _value = value;
                OnValueChanged(EventArgs.Empty);

                _thumbValue = CalculateThumbValue();
                Invalidate();
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _renderer.Update(
                _renderer.Parameters
                    .WithScrollBarSize(new Size(SystemInformation.VerticalScrollBarWidth, Size.Height)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _renderer.Render(e.Graphics, _thumbValue);
        }

        private int CalculateThumbValue()
        {
            // Calculate the thumb value based on the current position and range.
            // This example assumes a simple linear mapping.
            float range = Parameters.Maximum - Parameters.Minimum;
            int trackSize = Parameters.ScrollbarSize.Height - Parameters.ThumbWidth;
            int thumbValue = (int)((Value - Parameters.Minimum) / range * trackSize);
            return thumbValue;
        }

        private HoverArea GetHoverArea(int mouseY)
        {
            var thumbInfo = _renderer.GetThumbInfo(Value);

            int thumbTop = thumbInfo.ThumbY;
            int thumbBottom = thumbTop + thumbInfo.ThumbHeight;
            int upArrowBottom = Parameters.ThumbWidth;
            int downArrowTop = Height - Parameters.ThumbWidth;

            if (mouseY >= thumbTop && mouseY <= thumbBottom)
            {
                return HoverArea.Thumb;
            }
            if (mouseY < upArrowBottom)
            {
                return HoverArea.LeftUpArrow;
            }
            if (mouseY > downArrowTop)
            {
                return HoverArea.DownRightArrow;
            }
            return HoverArea.Track;
        }

        private void HandleMouseDown(HoverArea hoverArea, int mouseY)
        {
            var thumbInfo = _renderer.GetThumbInfo(Value);

            switch (hoverArea)
            {
                case HoverArea.Thumb:
                    _isDragging = true;
                    _dragOffset = mouseY - thumbInfo.ThumbY;
                    Capture = true;
                    break;
                case HoverArea.LeftUpArrow:
                    // Handle as SmallChange (ArrowUp)
                    // UpdateScrollbarValue(Parameters.Value - Parameters.SmallChange);
                    break;
                case HoverArea.DownRightArrow:
                    // Handle as SmallChange (ArrowDown)
                    // UpdateScrollbarValue(Parameters.Value + Parameters.SmallChange);
                    break;
                case HoverArea.Track:
                    // Handle as LargeChange (PageUp or PageDown)
                    // Implement logic similar to what you had for LargeChange.
                    break;
                default:
                    // Do nothing
                    break;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                var hoverArea = GetHoverArea(e.Y);
                HandleMouseDown(hoverArea, e.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left && _isDragging)
            {
                _isDragging = false;
                Capture = false;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _renderer.Update(hoverArea: HoverArea.None);
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _renderer.Update(hoverArea: GetHoverArea(PointToClient(MousePosition).Y));
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isDragging)
            {
                var thumbInfo = _renderer.GetThumbInfo(Value);

                // Calculate the new thumb position based on the mouse cursor's Y-coordinate.
                int newThumbTop = e.Y - _dragOffset;

                // Calculate the height of the Up and Down buttons.
                int buttonHeight = HeightOfUpAndDownButtons(); // Replace with your method to calculate button height.

                // Calculate the available track height (excluding the button heights).
                int trackHeight = ClientSize.Height - 2 * buttonHeight;

                // Ensure that the thumb stays within the valid range of positions.
                int minThumbTop = buttonHeight;  // Top of the thumb should not go above the Up button.
                int maxThumbTop = buttonHeight + trackHeight - thumbInfo.ThumbHeight;  // Maximum position within the track.

                // Clamp the newThumbTop value to stay within the valid range.
                newThumbTop = Math.Max(minThumbTop, Math.Min(maxThumbTop, newThumbTop));

                // Calculate the new _thumbValue based on the newThumbTop position.
                double thumbRatio = (double)(newThumbTop - minThumbTop) / (maxThumbTop - minThumbTop);

                _renderer.Update(hoverArea: HoverArea.Thumb);
                Value = (float)thumbRatio
                    * (_renderer.Parameters.Maximum - _renderer.Parameters.Minimum)
                    + _renderer.Parameters.Minimum;

                Debug.WriteLine($"Value: {Value}");

                // Update the scrollbar's value using _thumbValue.
                _thumbValue = (int)Value;
            }

            int HeightOfUpAndDownButtons()
            {
                // Replace with your method to calculate button height.
                return _renderer.Parameters.ThumbWidth;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            // Handle mouse wheel events and update the thumb position.
            // You will need to implement this based on your specific requirements.
            // You can adjust the _thumbValue and trigger a scroll event accordingly.
        }
    }
}
