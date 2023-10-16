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

        private VerticalContentScrollbarRenderer _renderer;
        private bool _isDragging;
        private int _dragOffset;
        private int _thumbValue;
        private float _value;

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
            MouseDown += ThemedVerticalScrollbar_MouseDown;
            MouseUp += ThemedVerticalScrollbar_MouseUp;
            MouseMove += ThemedVerticalScrollbar_MouseMove;
            MouseWheel += ThemedVerticalScrollbar_MouseWheel;
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
            set => _renderer.Update(isDarkMode: value);
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
                Invalidate();
            }
        }

        private void SetValueInternal(float value)
        {
            if (value == _value)
            {
                return;
            }

            _value = value;
            OnValueChanged(EventArgs.Empty);
            Invalidate();
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
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

        private void ThemedVerticalScrollbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var thumbInfo = _renderer.GetThumbInfo(Value);
                // Check if the mouse is over the thumb.

                int thumbTop = thumbInfo.ThumbY;
                int thumbBottom = thumbTop + thumbInfo.ThumbHeight;

                if (e.Y >= thumbTop && e.Y <= thumbBottom)
                {
                    _isDragging = true;
                    _dragOffset = e.Y - thumbTop;
                    Capture = true;
                }
                else if (e.Y < thumbTop)
                {
                    // Clicked above the thumb, handle as LargeChange (PageUp).
                    // You can increase the value by a larger amount.

                    // Update the scrollbar value accordingly based on LargeChange.
                    // For example:
                    // newValue = Math.Max(Parameters.Minimum, Parameters.Value - Parameters.LargeChange);
                    // UpdateScrollbarValue(newValue);
                }
                else if (e.Y > thumbBottom)
                {
                    // Clicked below the thumb, handle as LargeChange (PageDown).
                    // You can decrease the value by a larger amount.

                    // Update the scrollbar value accordingly based on LargeChange.
                    // For example:
                    // newValue = Math.Min(Parameters.Maximum, Parameters.Value + Parameters.LargeChange);
                    // UpdateScrollbarValue(newValue);
                }
            }
        }

        private void ThemedVerticalScrollbar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isDragging)
            {
                _isDragging = false;
                Capture = false;
            }
        }

        private void ThemedVerticalScrollbar_MouseMove(object sender, MouseEventArgs e)
        {
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

                Value = (float) thumbRatio 
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

        private void ThemedVerticalScrollbar_MouseWheel(object sender, MouseEventArgs e)
        {
            // Handle mouse wheel events and update the thumb position.
            // You will need to implement this based on your specific requirements.
            // You can adjust the _thumbValue and trigger a scroll event accordingly.
        }
    }
}
