using System.Collections.Specialized;
using System.Diagnostics;
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

    internal SizeF LayoutInternal()
    {
        if (((IDocument)this).HostControl is GridView gridView)
        {
            Debug.Print("--> LayoutInternal");

            if (Items.Count == 0)
            {
                return Size;
            }

            float currentX = Items[0].Margin.Left + gridView.Padding.Left;
            float currentY = Items[0].Margin.Top + gridView.Padding.Top;
            float highestItemHeight = Items[0].Bounds.Bottom;

            Items[0].Location = new PointF(
                currentX,
                currentY);

            Items[0].HasBeenLayout = true;

            if (Items.Count==1)
            {
                return Size;
            }

            for (int i = 1; i < Items.Count; i++)
            {
                if (gridView.Orientation == Orientation.Horizontal)
                {
                    // We are in horizontal mode, so place the items next to each other, starting from the left,
                    // either until we reach the right border or or we run out of items.
                    currentX += Items[i-1].Size.Width + Items[i-1].Margin.Right;

                    if (currentX + Items[i].Size.Width > Size.Width)
                    {
                        currentX = Items[i].Margin.Left + gridView.Padding.Left;
                        currentY += Items[i].Margin.Top + highestItemHeight;
                        highestItemHeight = Items[i].Size.Height;
                    }

                    Items[i].Location = new PointF(currentX, currentY);

                    if (Items[i].Size.Height > highestItemHeight)
                    {
                        highestItemHeight = Items[i].Size.Height;
                    }

                }
                else
                {
                    // We are in vertical mode, so place the items below each other, starting from the top,
                    // either until we reach the bottom border or we run out of items.
                    currentY += Items[i].Size.Height + Items[i].Margin.Bottom;

                    if (Items[i].Margin.Right > highestItemHeight)
                    {
                        highestItemHeight = Items[i].Margin.Right;
                    }

                    if (currentY > Size.Height)
                    {
                        currentY = Items[i].Margin.Top + gridView.Padding.Top;
                        currentX += Items[i].Size.Width + Items[i].Margin.Left + highestItemHeight;
                        highestItemHeight = 0;
                    }

                    Items[i].Location = new PointF(currentX, currentY);
                }

                Items[i].HasBeenLayout = true;
            }

            Size = new SizeF(
                Size.Width,
                currentY + highestItemHeight + gridView.Padding.Bottom);

        }

        Debug.Print($"--> LayoutInternal - S:{Size}");

        return Size;
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
