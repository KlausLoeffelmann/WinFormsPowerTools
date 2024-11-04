using System.ComponentModel;
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

    [DefaultValue(ModernCheckBoxStyle.Rectangular)]
    public ModernCheckBoxStyle SwitchStyle
    {
        get => _switchStyle;
        set { _switchStyle = value; Invalidate(); }
    }

    [DefaultValue(TextPosition.Left)]
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
        var totalWidth = textSize.Width + switchWidth + Padding.Horizontal;

        g.Clear(this.BackColor);

        Rectangle switchRect, textRect;

        switch (CheckAlign)
        {
            case ContentAlignment.TopRight:
                switchRect = new Rectangle(Padding.Left, Padding.Top, switchWidth, switchHeight);
                textRect = new Rectangle(switchWidth + Padding.Left + 10 * _dpiScale, Padding.Top, textSize.Width, textSize.Height);
                break;
            case ContentAlignment.MiddleRight:
                switchRect = new Rectangle(Padding.Left, (totalHeight - switchHeight) / 2 + Padding.Top, switchWidth, switchHeight);
                textRect = new Rectangle(switchWidth + Padding.Left + 10 * _dpiScale, (totalHeight - textSize.Height) / 2 + Padding.Top, textSize.Width, textSize.Height);
                break;
            case ContentAlignment.BottomRight:
                switchRect = new Rectangle(Padding.Left, totalHeight - switchHeight + Padding.Top, switchWidth, switchHeight);
                textRect = new Rectangle(switchWidth + Padding.Left + 10 * _dpiScale, totalHeight - textSize.Height + Padding.Top, textSize.Width, textSize.Height);
                break;
            case ContentAlignment.TopLeft:
                textRect = new Rectangle(Padding.Left, Padding.Top, textSize.Width, textSize.Height);
                switchRect = new Rectangle(textSize.Width + Padding.Left + 10 * _dpiScale, Padding.Top, switchWidth, switchHeight);
                break;
            case ContentAlignment.MiddleLeft:
                textRect = new Rectangle(Padding.Left, (totalHeight - textSize.Height) / 2 + Padding.Top, textSize.Width, textSize.Height);
                switchRect = new Rectangle(textSize.Width + Padding.Left + 10 * _dpiScale, (totalHeight - switchHeight) / 2 + Padding.Top, switchWidth, switchHeight);
                break;
            case ContentAlignment.BottomLeft:
                textRect = new Rectangle(Padding.Left, totalHeight - textSize.Height + Padding.Top, textSize.Width, textSize.Height);
                switchRect = new Rectangle(textSize.Width + Padding.Left + 10 * _dpiScale, totalHeight - switchHeight + Padding.Top, switchWidth, switchHeight);
                break;
            default:
                throw new NotSupportedException($"CheckAlign {CheckAlign} is not supported.");
        }

        RenderSwitch(g, switchRect, circleDiameter);
        RenderText(g, textRect.Location);
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
        int totalWidth = textSize.Width + switchWidth + Padding.Horizontal + 10 * _dpiScale; // 10 dpi padding
        int totalHeight = Math.Max(textSize.Height, switchHeight) + Padding.Vertical;
        return new Size(totalWidth, totalHeight);
    }
}
