using System.Collections;
using System.Windows.Forms.Documents;

namespace WinForms.PowerTools.Controls;

public partial class GridView 
    : DocumentControl<GridViewDocument, GridViewItem>, ICollection<GridViewItem>
{
    public void Add(GridViewItem item) 
        => MainDocument?.Items.Add(item);

    public void Clear() 
        => MainDocument?.Items.Clear();

    public bool Contains(GridViewItem item) 
        => MainDocument?.Items.Contains(item) ?? false;

    void ICollection<GridViewItem>.CopyTo(GridViewItem[] array, int arrayIndex) 
        => MainDocument?.Items.CopyTo(array, arrayIndex);

    public bool Remove(GridViewItem item) 
        => MainDocument?.Items.Remove(item) ?? false;

    public int Count 
        => MainDocument?.Items.Count ?? 0;

    IEnumerator<GridViewItem> IEnumerable<GridViewItem>.GetEnumerator() 
        => MainDocument?.Items.GetEnumerator() ?? ((IEnumerable<GridViewItem>)[]).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() 
        => MainDocument?.Items.GetEnumerator() ?? Array.Empty<GridViewItem>().GetEnumerator();
}
