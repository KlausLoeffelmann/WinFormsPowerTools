using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsPowerTools.ThemedScrollBars;

namespace WinFormsPowerToolsDemo;

public partial class TestForm : Form
{

    // Fields
    private readonly ThemedChildForm _childForm = new ThemedChildForm();

    public TestForm()
    {
        InitializeComponent();

        // Set properties for ChildForm to be used as control
        _childForm.TopLevel = false;

        // Add ChildForm to TestForm
        Controls.Add(_childForm);

        _childForm.Show();

        DoubleBuffered = true;
    }
}

public class ChildForm : Form
{

}

public class ThemedChildForm : Form
{
    // Fields
    private readonly int _cornerRadius = 12;
    private readonly int _titleBarHeight = 30;
    private Point _lastMousePosition;
    private Rectangle _fixedFormPadding = new Rectangle(1, 1, 1, 1);
    private bool _ignoreSetClientSize;
    private Bitmap? _backgroundBitmap;

    private readonly Size _initialSize = new Size(400, 300);
    private Size _clientSize;

    // Constructor
    public ThemedChildForm()
    {
        _clientSize = _initialSize;
        InitializeComponents();
        DoubleBuffered = true;

    }

    // Methods
    private void InitializeComponents()
    {
        // Set properties for ChildForm
        Text = "My Custom Title";
        Width = _initialSize.Width;
        Height = _initialSize.Height;
        FormBorderStyle = FormBorderStyle.None;

        // Make the form's background transparent for rounded corners
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        // Add the Themed vertical scrollbar
        Controls.Add(new ThemedVerticalScrollbar()
        {
            Dock = DockStyle.Right
        }); ;
    }

    private void RegenerateBackgroundBitmap()
    {
        Bitmap backgroundBitmap = new Bitmap(Width, Height);

        using (Graphics g = Graphics.FromImage(backgroundBitmap))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Fill the "left-over shape" of each outer corner curve area with the color fuchsia
            g.Clear(TransparencyKey);

            // Create and fill rounded rectangle path for inner part
            GraphicsPath path = new GraphicsPath();
            path.AddRoundedRectangle(new Rectangle(0, 0, Width, Height), _cornerRadius);
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillPath(brush, path);
            }

            // Draw custom title bar
            using (SolidBrush titleBarBrush = new SolidBrush(Color.DarkBlue))
            {
                // Adjusted to start after the rounded corner and end before the other rounded corner
                GraphicsPath titleBarPath = new GraphicsPath();
                titleBarPath.AddRoundedRectangle(new Rectangle(1, 1, Width - 2, _titleBarHeight), _cornerRadius);
                g.FillPath(titleBarBrush, titleBarPath);
            }

            using (SolidBrush textBrush = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, textBrush, _cornerRadius + 10, 5);
            }

            // Draw rounded border
            using (Pen borderPen = new Pen(Color.DimGray, 2))
            {
                GraphicsPath borderPath = new GraphicsPath();
                borderPath.AddRoundedRectangle(new Rectangle(1, 1, Width - 2, Height - 2), _cornerRadius);
                g.DrawPath(borderPen, borderPath);
            }
        }

        BackgroundImage = backgroundBitmap;
        Invalidate();
    }

    protected override void OnClientSizeChanged(EventArgs e)
    {
        if (_clientSize == Size)
        {
            var clientWidth = Width - _fixedFormPadding.Left - _fixedFormPadding.Right;
            var clientHeight = Height - _fixedFormPadding.Top - _fixedFormPadding.Bottom - _titleBarHeight;
            _clientSize = new Size(clientWidth, clientHeight);

            base.UpdateBounds(
                Location.X,
                Location.Y,
                Width,
                Height,
                clientWidth,
                clientHeight);

            return;
        }

        base.OnClientSizeChanged(e);
        Debug.Print($"ClientSize: {ClientSize}");
    }

    // Overridden Methods
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        RegenerateBackgroundBitmap();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Y <= _titleBarHeight)
        {
            _lastMousePosition = e.Location;
        }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (e.Button == MouseButtons.Left && e.Y <= _titleBarHeight)
        {
            Location = new Point(
                Location.X + e.X - _lastMousePosition.X,
                Location.Y + e.Y - _lastMousePosition.Y);

            Invalidate();
        }
    }
}

[Flags]
public enum RoundedCorners
{
    None = 0,
    TopLeft = 1,
    TopRight = 2,
    BottomLeft = 4,
    BottomRight = 8,
    All = TopLeft | TopRight | BottomLeft | BottomRight
}

public static class GraphicsPathExtensions
{
    public static void AddRoundedRectangle(this GraphicsPath path, Rectangle rect, int cornerRadius, RoundedCorners corners = RoundedCorners.All)
    {
        if (path is null) throw new ArgumentNullException(nameof(path));

        if (corners.HasFlag(RoundedCorners.TopLeft))
            path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
        else
            path.AddLine(rect.X, rect.Y, rect.X, rect.Y);

        if (corners.HasFlag(RoundedCorners.TopRight))
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
        else
            path.AddLine(rect.Right, rect.Y, rect.Right, rect.Y);

        if (corners.HasFlag(RoundedCorners.BottomRight))
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
        else
            path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom);

        if (corners.HasFlag(RoundedCorners.BottomLeft))
            path.AddArc(rect.X, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
        else
            path.AddLine(rect.X, rect.Bottom, rect.X, rect.Bottom);

        path.CloseFigure();
    }
}
