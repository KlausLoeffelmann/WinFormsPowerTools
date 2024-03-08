using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridViewItem : AsyncDocumentItem
{
    internal bool HasBeenLayouted;

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
        if (!HasBeenLayouted)
        {
            return Task.CompletedTask;
        }

        // Let's start a new Task, which counts from 1 to 100 and shows that in the middle of the grid item.
        return Task.Run(async () =>
        {
            if (deviceContext is not Graphics graphics)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                SyncContext.Post(_ =>
                {
                    graphics.Clear(SystemColors.Control);
                    graphics.DrawString(
                        i.ToString(), 
                        SystemFonts.DefaultFont, 
                        Brushes.Black, 
                        new PointF(
                            Size.Width / 2, 
                            Size.Height / 2));
                }, null);

                await Task.Delay(100, cancellationToken);
            }
        }, cancellationToken);
    }
}
