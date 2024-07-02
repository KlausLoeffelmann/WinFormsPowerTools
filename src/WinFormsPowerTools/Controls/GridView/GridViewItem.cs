using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridViewItem : AsyncDocumentItem
{
    internal bool HasBeenLayout;

    internal GridViewItem(GridViewDocument parentDocument, nint parentControlHandle) 
        : base(parentDocument, parentControlHandle)
    {
    }

    public object? Tag { get; internal set; }

    private Font _bigFont = new(SystemFonts.DefaultFont.FontFamily, 20);
    private static readonly Random s_random = new Random();
    private static readonly StringFormat s_counterStringFormat= new StringFormat
    {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
    };

    protected internal override Func<PointF, CancellationToken, Task> OnGetRenderPredicate()
    {
        Func<PointF, CancellationToken, Task> asyncRenderPredicate = async (offset, cancellationToken) =>
        {
            if (!HasBeenLayout)
            {
                await Task.CompletedTask.ConfigureAwait(false);
                return;
            }

            var bounds = ClientRectangle;
            bounds.Offset(Location);
            bounds.Offset(offset);

            if (DeviceContext is Graphics graphics)
            {
                graphics.ResetTransform();
                graphics.TranslateTransform(bounds.X, bounds.Y);
            }
            else
            {
                DeviceContext = DeviceContext ??= Graphics.FromHwnd(ParentControlHandle);
                graphics = (Graphics)DeviceContext;
            }

            Debug.Print($"-----> Start rendering item {Id}.");
            // Let's start a new Task, which counts from 1 to 100 and shows that in the middle of the grid item.
            for (int i = 0; i < 10; i++)
            {
                // We need to signal the instance which holds the graphics object, that it cannot be disposed right now.

                var tempColor = Color.LightBlue;

                // Clear the Background
                graphics.Clear(tempColor);

                // draw frame around this item:
                graphics.DrawRectangle(
                    new Pen(tempColor, 1),
                    graphics.VisibleClipBounds);

                graphics.DrawString(
                    i.ToString(),
                    _bigFont,
                    Brushes.Black,
                    new PointF(
                        ClientSize.Width / 2 - 5,
                        ClientSize.Height / 2 - 5),
                    s_counterStringFormat);

                graphics.DrawString(
                    $"Id:{Id}, L: {Location}",
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    PointF.Empty);

                string sizeString = $"Size: {Size}";

                // Measure the string:
                var size = graphics.MeasureString(sizeString, SystemFonts.DefaultFont);

                graphics.DrawString(
                    sizeString,
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    new PointF(
                        ClientSize.Width - size.Width,
                        ClientSize.Height - size.Height));

                //await Task.Delay(s_random.Next(100)).ConfigureAwait(false);
                Debug.Print($"-----> End rendering item {Id}.");
            }
        };

        return asyncRenderPredicate;
    }
}
