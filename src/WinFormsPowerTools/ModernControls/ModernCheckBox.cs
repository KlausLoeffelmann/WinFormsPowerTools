using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace WinForms.PowerTools.ModernControls;

#nullable enable

public class ModernCheckBox : CheckBox
{
    private float _animationProgress;
    private Timer? _animationTimer;
    private int _dpiScale;
    private ModernCheckBoxStyle _switchStyle;
    private TextPosition _textPosition;

    public enum ModernCheckBoxStyle
    {
        Rectangular,
        Rounded
    }

    public enum TextPosition
    {
        Left,
        Right
    }

    public ModernCheckBoxStyle SwitchStyle
    {
        get => _switchStyle;
        set { _switchStyle = value; Invalidate(); }
    }

    public TextPosition TextRenderPosition
    {
        get => _textPosition;
        set { _textPosition = value; Invalidate(); }
    }

    public ModernCheckBox()
    {
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        this.SetStyle(ControlStyles.ResizeRedraw, true);
        _switchStyle = ModernCheckBoxStyle.Rounded;
        _textPosition = TextPosition.Right;
        UpdateDpiScale();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        Render(pevent.Graphics);
    }

    protected override void OnCheckedChanged(EventArgs e)
    {
        base.OnCheckedChanged(e);
        StartAnimation();
    }

    private void StartAnimation()
    {
        _animationProgress = 0;
        _animationTimer?.Dispose();
        _animationTimer = new Timer
        {
            Interval = 16 // Approximately 60 FPS
        };
        _animationTimer.Tick += (sender, args) =>
        {
            _animationProgress += (float)_animationTimer.Interval / AnimationDuration;
            if (_animationProgress >= 1)
            {
                _animationProgress = 1;
                _animationTimer.Stop();
            }
            this.Invalidate();
        };
        _animationTimer.Start();
    }

    private void Render(Graphics g)
    {
        UpdateDpiScale();

        int switchWidth = 50 * _dpiScale;
        int switchHeight = 25 * _dpiScale;
        int circleDiameter = 20 * _dpiScale;

        var textSize = TextRenderer.MeasureText(Text, Font);
        var totalHeight = Math.Max(textSize.Height, switchHeight);

        int switchY = (totalHeight - switchHeight) / 2;
        int textY = (totalHeight - textSize.Height) / 2;

        g.Clear(this.BackColor);

        if (_textPosition == TextPosition.Right)
        {
            RenderSwitch(g, new Rectangle(0, switchY, switchWidth, switchHeight), circleDiameter);
            RenderText(g, new Point(switchWidth + 10 * _dpiScale, textY));
        }
        else
        {
            RenderText(g, new Point(0, textY));
            RenderSwitch(g, new Rectangle(textSize.Width + 10 * _dpiScale, switchY, switchWidth, switchHeight), circleDiameter);
        }
    }

    private void RenderSwitch(Graphics g, Rectangle rect, int circleDiameter)
    {
        var backgroundColor = this.Checked ? SystemColors.Highlight : SystemColors.ControlDark;
        var circleColor = SystemColors.ControlLightLight;
        var circlePosition = this.Checked
            ? (rect.Width - circleDiameter) * EaseOut(_animationProgress)
            : (rect.Width - circleDiameter) * (1 - EaseOut(_animationProgress));

        using var backgroundBrush = new SolidBrush(backgroundColor);
        using var circleBrush = new SolidBrush(circleColor);
        using var backgroundPen = new Pen(SystemColors.WindowFrame, 2 * _dpiScale);

        if (_switchStyle == ModernCheckBoxStyle.Rounded)
        {
            var radius = rect.Height / 2f;
            using var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();

            g.FillPath(backgroundBrush, path);
            g.DrawPath(backgroundPen, path);
        }
        else
        {
            g.FillRectangle(backgroundBrush, rect);
            g.DrawRectangle(backgroundPen, rect);
        }

        g.FillEllipse(circleBrush, rect.X + circlePosition, rect.Y + 2.5f * _dpiScale, circleDiameter, circleDiameter);
    }

    private void RenderText(Graphics g, Point position)
    {
        TextRenderer.DrawText(g, Text, Font, position, ForeColor);
    }

    private static float EaseOut(float t) => 1 - (1 - t) * (1 - t);

    private void UpdateDpiScale()
    {
        using var graphics = this.CreateGraphics();
        _dpiScale = (int)(graphics.DpiX / 96);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _animationTimer?.Dispose();
        }
        base.Dispose(disposing);
    }

    private static int AnimationDuration => 300; // milliseconds

    public override Size GetPreferredSize(Size proposedSize)
    {
        var textSize = TextRenderer.MeasureText(Text, Font);
        int switchWidth = 50 * _dpiScale;
        int switchHeight = 25 * _dpiScale;
        int totalWidth = textSize.Width + switchWidth + 20 * _dpiScale; // 10 dpi padding on each side
        int totalHeight = Math.Max(textSize.Height, switchHeight);
        return new Size(totalWidth, totalHeight);
    }
}
