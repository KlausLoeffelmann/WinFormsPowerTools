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

    internal override Task OnRenderAsync(PointF scrollOffset, IDeviceContext deviceContext, CancellationToken cancellationToken)
    {
        if (!HasBeenLayout 
            || deviceContext is not Graphics graphics)
        {
            return Task.CompletedTask;
        }

        // Let's start a new Task, which counts from 1 to 100 and shows that in the middle of the grid item.
        return Task.Run(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                SyncContext.Post(_ =>
                {
                    var tempColor = Application.SystemColors.Control;

                    // Constrain everything to the bounds of this item:
                    graphics.SetClip(new RectangleF(Location, Size));

                    graphics.Clear(SystemColors.Control);

                    // draw frame around this item:
                    graphics.DrawRectangle(
                        new Pen(tempColor, 1), 
                        new RectangleF(Location, Size));

                    graphics.DrawString(
                        i.ToString(), 
                        SystemFonts.DefaultFont, 
                        Brushes.Black, 
                        new PointF(
                            Location.X + Size.Width / 2, 
                            Location.Y + Size.Height / 2));

                    graphics.ResetClip();
                }, null);

                await Task.Delay(100, cancellationToken);
            }
        }, cancellationToken);
    }
}
