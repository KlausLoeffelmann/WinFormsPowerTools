using WinFormsPowerTools.AutoLayout;

public static class AutoLayoutExtensions
{
    public static System.Windows.Forms.Padding ToWinFormsPadding(this AutoLayoutPadding padding)
        => new System.Windows.Forms.Padding((int)padding.Left, (int)padding.Top, (int)padding.Right, (int)padding.Bottom);

    public static System.Windows.Forms.ColumnStyle ToTableLayoutPanelColumnStyle(this AutoLayoutColumnDefinition columnDefinition)
        => new System.Windows.Forms.ColumnStyle(
            columnDefinition.Width.ToTableLayoutPanelSizeType(),
            columnDefinition.Width.Value is not null
                ? (float)columnDefinition.Width.Value
                : 0);

    public static System.Windows.Forms.SizeType ToTableLayoutPanelSizeType(this AutoLayoutGridLength gridLength)
        => gridLength switch
        {
            { IsAbsolut: true } => System.Windows.Forms.SizeType.Absolute,
            { IsAuto: true } => System.Windows.Forms.SizeType.AutoSize,
            _ => System.Windows.Forms.SizeType.Percent
        };

    public static System.Windows.Forms.TableLayoutPanelCellPosition ToTableLayoutPanelCellPosition(this AutoLayoutFencedPosition fencedPosition)
        => new System.Windows.Forms.TableLayoutPanelCellPosition(fencedPosition.Column, fencedPosition.Row);

    public static System.Windows.Forms.RowStyle ToTableLayoutPanelRowStyle(this AutoLayoutRowDefinition rowDefinition)
        => new System.Windows.Forms.RowStyle(
            rowDefinition.Height.ToTableLayoutPanelSizeType(),
            rowDefinition.Height.Value is not null
                ? (float)rowDefinition.Height.Value
                : 0);
}
