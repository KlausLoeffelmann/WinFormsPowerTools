using System.Diagnostics;
using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridViewItem : AsyncDocumentItem
{
    internal bool HasBeenLayout;

    internal GridViewItem(GridViewDocument parentDocument, WindowsFormsSynchronizationContext syncContext) 
        : base(parentDocument, syncContext)
    {
    }

    public object? Tag { get; internal set; }

    protected override void OnVisibilityChangedStateChanged(VisibilityChangeState visibilityChangeState)
    {
    }

    public Rectangle ClipArea
    {
        get
        {
            var location = EffectiveLocation;
            var size = ClientSize;
            return new Rectangle(
                (int)location.X - 1,
                (int)location.Y - 1,
                (int)size.Width + 2,
                (int)size.Height + 2);
        }
    }

    private Font _bigFont = new(SystemFonts.DefaultFont.FontFamily, 20);
    private static readonly Random s_random = new Random();
    private static readonly StringFormat s_counterStringFormat= new StringFormat
    {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
    };

    protected internal override void OnGetRenderPredicate(out Func<IDeviceContext, CancellationToken , Task> asyncRenderPredicate)
    {
        asyncRenderPredicate = async (deviceContext, cancellationToken) =>
        {
            if (!HasBeenLayout
                || deviceContext is not Graphics graphics)
            {
                await Task.CompletedTask;
                return;
            }

            // Let's start a new Task, which counts from 1 to 100 and shows that in the middle of the grid item.
            for (int i = 0; i < 10; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                // We need to signal the instance which holds the graphics object, that it cannot be disposed right now.

                var tempColor = Color.LightBlue;

                // Restrict the drawing to the clip area
                graphics.SetClip(ClipArea);

                // Clear the Background
                graphics.Clear(tempColor);

                // draw frame around this item:
                graphics.DrawRectangle(
                    new Pen(tempColor, 1),
                    new RectangleF(EffectiveLocation, ClientSize));

                graphics.DrawString(
                    i.ToString(),
                    _bigFont,
                    Brushes.Black,
                    new PointF(
                        EffectiveLocation.X + ClientSize.Width / 2 - 5,
                        EffectiveLocation.Y + ClientSize.Height / 2 - 5),
                    s_counterStringFormat);

                graphics.DrawString(
                    $"Id:{Id}, L: {Location}",
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    new PointF(
                        EffectiveLocation.X,
                        EffectiveLocation.Y));

                string sizeString = $"Size: {Size}";

                // Measure the string:
                var size = graphics.MeasureString(sizeString, SystemFonts.DefaultFont);

                graphics.DrawString(
                    sizeString,
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    new PointF(
                        EffectiveLocation.X + ClientSize.Width - size.Width,
                        EffectiveLocation.Y + ClientSize.Height - size.Height));

                await Task.Delay(s_random.Next(200), cancellationToken);
            }
        };
    }
}
