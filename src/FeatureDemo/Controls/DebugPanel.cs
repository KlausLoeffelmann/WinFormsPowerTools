using System.Windows.Forms.Design;

namespace FeatureDemo.Controls;

internal class DebugPanel : Panel
{
    private readonly TextBox _outputPane = new()
    {
        Multiline = true,
        Dock = DockStyle.Fill,
        ScrollBars = ScrollBars.Vertical,
        Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
        BorderStyle = BorderStyle.FixedSingle,
    };

    public DebugPanel()
    {
        Controls.Add(_outputPane);
    }

    public void Write(string message)
    {
        _outputPane.AppendText(message);
        _outputPane.SelectionStart = _outputPane.Text.Length;
        _outputPane.ScrollToCaret();
    }

    public void WriteLine(string message)
    {
        Write(message + Environment.NewLine);
    }
}
