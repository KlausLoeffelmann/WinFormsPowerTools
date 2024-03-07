using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridViewItem : AsyncDocumentItem
{
    public GridViewItem(WindowsFormsSynchronizationContext syncContext) : base(syncContext)
    {
    }

    protected override void OnVisibilityChangedStateChanged(VisibilityChangeState visibilityChangeState)
    {
    }

    internal override Task OnRenderAsync(PointF scrollOffset, IDeviceContext deviceContext, CancellationToken cancellationToken)
    {
        // Let's start a new Task, which counts from 1 to 100 and shows that in the middle of the grid item.
        return Task.Run(async () =>
        {
            if (deviceContext is not Graphics graphics)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                graphics.DrawString(
                    i.ToString(), 
                    SystemFonts.DefaultFont, 
                    Brushes.Black, 
                    new PointF(ParentViewSize.Width / 2, 
                    ParentViewSize.Height / 2));

                await Task.Delay(100, cancellationToken);
            }
        }, cancellationToken);
    }
}
