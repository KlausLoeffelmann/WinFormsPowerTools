using System.Windows.Forms.Design;

namespace FeatureDemo.Controls.ToolStrip;

[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class ToolStripClockPanel : ToolStripItem
{
    private DateTime _timeAndDate;
    private Color _clockFaceColor;
    private Pen? _drawingPen;
    private SolidBrush? _backColorBrush;

    public ToolStripClockPanel()
    {
        _timeAndDate = DateTime.Now;
        _clockFaceColor = Application.SystemColors.Window;
    }

    public Color ClockFaceColor
    {
        get { return _clockFaceColor; }
        set { _clockFaceColor = value; }
    }

    private bool ShouldSerializeClockFaceColor()
    {
        return _clockFaceColor != Application.SystemColors.ControlDarkDark;
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

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        _drawingPen ??= new Pen(ClockFaceColor, 2);
        _backColorBrush ??= new SolidBrush(BackColor);

        // Calculate the center point of the clock panel
        int centerX = Width / 2;
        int centerY = Height / 2;

        // Calculate the radius of the clock face
        int radius = Math.Min(centerX, centerY) - 10;

        // Draw the clock face
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        e.Graphics.FillEllipse(_backColorBrush, centerX - radius, centerY - radius, 2 * radius, 2 * radius);
        e.Graphics.DrawEllipse(_drawingPen, centerX - radius, centerY - radius, 2 * radius, 2 * radius);

        // Draw the hour hand
        int hour = TimeAndDate.Hour % 12;
        double hourAngle = (hour + TimeAndDate.Minute / 60.0) * 30;
        double hourHandLength = radius * 0.5;
        int hourHandX = centerX + (int)(hourHandLength * Math.Sin(hourAngle * Math.PI / 180));
        int hourHandY = centerY - (int)(hourHandLength * Math.Cos(hourAngle * Math.PI / 180));
        e.Graphics.DrawLine(_drawingPen, centerX, centerY, hourHandX, hourHandY);

        // Draw the minute hand
        int minute = TimeAndDate.Minute;
        double minuteAngle = (minute + TimeAndDate.Second / 60.0) * 6;
        double minuteHandLength = radius * 0.7;
        int minuteHandX = centerX + (int)(minuteHandLength * Math.Sin(minuteAngle * Math.PI / 180));
        int minuteHandY = centerY - (int)(minuteHandLength * Math.Cos(minuteAngle * Math.PI / 180));
        e.Graphics.DrawLine(_drawingPen, centerX, centerY, minuteHandX, minuteHandY);
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
