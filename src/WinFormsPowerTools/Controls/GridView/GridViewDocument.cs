using System.Collections.Specialized;
using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public class GridViewDocument : Document<GridViewItem>
{
    internal GridViewDocument(IDocumentControl hostControl) : base(hostControl)
    {
    }

    internal void LayoutInternal()
    {
        if (((IDocument)this).HostControl is GridView gridView)
        {
            // Iterate through the items, and position them in logical rows and columns depending on the orientation and adjacent items count:
            // 1. If the orientation is horizontal, the items are positioned in rows, and the adjacent items count is the number of columns.
            // 2. If the orientation is vertical, the items are positioned in columns, and the adjacent items count is the number of rows.
            for (var i = 0; i < Items.Count; i++)
            {
                var adjacentItemsCount = gridView.AdjacentItemsCount;
                var padding = gridView.Padding;

                var item = Items[i];
                var row = i / adjacentItemsCount;
                var column = i % adjacentItemsCount;
                var x = column * (item.Size.Width + padding.Left + padding.Right);
                var y = row * (item.Size.Height + padding.Top + padding.Bottom);
                item.Location = new PointF(x, y);
            }
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

                LayoutInternal();
                break;

            case NotifyCollectionChangedAction.Remove:
                if (e.OldItems is null)
                {
                    return;
                }

                LayoutInternal();
                break;
        }
    }
}
