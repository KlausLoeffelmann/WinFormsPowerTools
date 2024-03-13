using System.Windows.Forms.Design;

namespace FeatureDemo.Controls.ToolStrip;

[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class ToolStripClockPanel : ToolStripItem
{
    private static readonly Color s_defaultClockFaceColor = Application.SystemColors.Window;
    private DateTime _timeAndDate;
    private Color _clockFaceColor;
    private Pen? _drawingPen;
    private SolidBrush? _backColorBrush;

    public ToolStripClockPanel()
    {
        _timeAndDate = DateTime.Now;
        _clockFaceColor = s_defaultClockFaceColor;
    }

    public Color ClockFaceColor
    {
        get { return _clockFaceColor; }
        set
        {
            if (_clockFaceColor != value)
            {
                _clockFaceColor = value;
                _drawingPen = null;
                _backColorBrush = null;
                Invalidate();
            }
        }
    }

    private bool ShouldSerializeClockFaceColor()
    {
        return _clockFaceColor != s_defaultClockFaceColor;
    }

    public DateTime TimeAndDate
    {
        get { return _timeAndDate; }
        set
        {
            _timeAndDate = value;
            Invalidate(); // Invalidate the control to trigger repainting
        }
    }

    protected override async void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (Parent is not Control parent || parent.IsAncestorSiteInDesignMode)
        {
            return;
        }

        var hdc = e.Graphics.GetHdc();
        var graphics=Graphics.FromHdc(hdc);
        e.Graphics.ReleaseHdc(hdc);

        await Task.Run(async () =>
        {
            await RenderAsync();
        });

        // Local methods run on a dedicated task.
        async Task RenderAsync()
        {
            // Create a new Graphics object from the captured Graphics object
            // We need to use the captured Graphics object, because by the time
            // this async method is executed, the original Graphics object may
            // have been already disposed.

            await Task.Delay(100);

            _drawingPen ??= new Pen(ClockFaceColor, 2);
            _backColorBrush ??= new SolidBrush(BackColor);

            // Calculate the center point of the clock panel
            int centerX = Width / 2;
            int centerY = Height / 2;

            // Calculate the radius of the clock face
            int radius = Math.Min(centerX, centerY) - 10;

            // Draw the clock face
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillEllipse(_backColorBrush, centerX - radius, centerY - radius, 2 * radius, 2 * radius);
            graphics.DrawEllipse(_drawingPen, centerX - radius, centerY - radius, 2 * radius, 2 * radius);

            // Draw the hour hand
            int hour = TimeAndDate.Hour % 12;
            double hourAngle = (hour + TimeAndDate.Minute / 60.0) * 30;
            double hourHandLength = radius * 0.5;
            int hourHandX = centerX + (int)(hourHandLength * Math.Sin(hourAngle * Math.PI / 180));
            int hourHandY = centerY - (int)(hourHandLength * Math.Cos(hourAngle * Math.PI / 180));
            graphics.DrawLine(_drawingPen, centerX, centerY, hourHandX, hourHandY);

            // Draw the minute hand
            int minute = TimeAndDate.Minute;
            double minuteAngle = (minute + TimeAndDate.Second / 60.0) * 6;
            double minuteHandLength = radius * 0.7;
            int minuteHandX = centerX + (int)(minuteHandLength * Math.Sin(minuteAngle * Math.PI / 180));
            int minuteHandY = centerY - (int)(minuteHandLength * Math.Cos(minuteAngle * Math.PI / 180));
            graphics.DrawLine(_drawingPen, centerX, centerY, minuteHandX, minuteHandY);

            // Draw the second hand
            int second = TimeAndDate.Second;
            double secondAngle = second * 6;
            double secondHandLength = radius * 0.9;
            int secondHandX = centerX + (int)(secondHandLength * Math.Sin(secondAngle * Math.PI / 180));
            int secondHandY = centerY - (int)(secondHandLength * Math.Cos(secondAngle * Math.PI / 180));
            graphics.DrawLine(_drawingPen, centerX, centerY, secondHandX, secondHandY);
        }
    }

    protected override Size DefaultSize
    {
        get
        {
            // Resize the panel to a reasonable size
            int size = Math.Min(Width, Height);
            return new Size(size, size);
        }
    }
}
