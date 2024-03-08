using System.Collections.Specialized;
using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridViewDocument : Document<GridViewItem>
{
    private readonly GridView _gridView;

    internal GridViewDocument(IDocumentControl hostControl) : base(hostControl)
    {
        // Subscribe to the Items collection changed event to update the layout when the collection changes.
        Items.CollectionChanged += OnItemsChanged;

        if (((IDocument)this).HostControl is GridView gridView)
        {
            _gridView = gridView;

            // Subscribe to the SizeChanged event of the GridView to update the size of the document.
            _gridView.SizeChanged += (s, e) =>
                Size = new SizeF(
                    _gridView.ClientSize.Width,
                    _gridView.ClientSize.Height);
        }
        else
        {
            throw new InvalidOperationException("The host control must be a GridView.");
        }
    }

    protected override void OnSizeChanged()
    {
        // We do not need to do anything here, because a size change of the document
        // automatically triggers a layout update of the items.
    }

    internal void LayoutInternal()
    {
        if (((IDocument)this).HostControl is GridView gridView)
        {
            if (Items.Count == 0)
            {
                return;
            }

            float currentX = Items[0].Margin.Left + gridView.Padding.Left;
            float currentY = Items[0].Margin.Top + gridView.Padding.Top;

            for (int i = 0; i < Items.Count; i++)
            {
                if (gridView.Orientation == Orientation.Horizontal)
                {
                    // We are in horizontal mode, so place the items next to each other, starting from the left,
                    // either until we reach the right border or or we run out of items.
                    if (currentX + Items[i].Size.Width > Size.Width)
                    {
                        currentX = 0;
                        currentY += Items[i].Size.Height;
                    }

                    Items[i].Location = new PointF(currentX, currentY);
                    currentX += Items[i].Size.Width;
                }
                else
                {
                    // We are in vertical mode, so place the items below each other, starting from the top,
                    // either until we reach the bottom border or we run out of items.
                    if (currentY + Items[i].Size.Height > Size.Height)
                    {
                        currentY = 0;
                        currentX += Items[i].Size.Width;
                    }

                    Items[i].Location = new PointF(currentX, currentY);
                    currentY += Items[i].Size.Height;
                }

                Items[i].HasBeenLayout = true;
            }

            Invalidate();
        }
    }

    private void OnItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                if (e.NewItems is null)
                {
                    return;
                }

                _gridView.PerformLayout();
                break;

            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems is null)
                {
                    return;
                }

                _gridView.PerformLayout();
                break;
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
 
        if (disposing)
        {
            Items.CollectionChanged -= OnItemsChanged;
        }
    }
}
