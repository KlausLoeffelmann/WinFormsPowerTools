using System.Collections.Generic;
using System.Windows.Forms;

public static class TableLayoutPanelExtension
{
    public static void AddRange(this TableLayoutColumnStyleCollection columnStyleCollection, IEnumerable<ColumnStyle> columnStyles)
    {
        foreach (var columnStyle in columnStyles) 
        {
            columnStyleCollection.Add(columnStyle);
        }
    }

    public static void AddRange(this TableLayoutRowStyleCollection rowStyleCollection, IEnumerable<RowStyle> rowStyles)
    {
        foreach (var rowStyle in rowStyles)
        {
            rowStyleCollection.Add(rowStyle);
        }
    }
}
