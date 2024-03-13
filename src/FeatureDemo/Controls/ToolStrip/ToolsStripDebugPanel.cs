using System.Windows.Forms.Design;

namespace FeatureDemo.Controls.ToolStrip;

[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
public class ToolsStripDebugPanel : ToolStripControlHost
{
    internal static DebugPanel DebugPanelFactory
        => new()
        {
            MinimumSize = new Size(400, 150),
            Padding = new Padding(10)
        };

    public ToolsStripDebugPanel() : base(DebugPanelFactory)
    {
    }

    private DebugPanel DebugPanel => (DebugPanel)base.Control;

    public void Write(string message) => DebugPanel.Write(message);

    public void WriteLine(string message) => DebugPanel.WriteLine(message);

    protected override Size DefaultSize => new Size(400, 150);
   
}
