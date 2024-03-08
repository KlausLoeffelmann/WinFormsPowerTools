using System.Windows.Forms;

namespace WinFormsPowerToolsDemo;

public partial class GridViewTestForm : Form
{
    public GridViewTestForm()
    {
        InitializeComponent();
    }

    private void test1ToolStripMenuItem_Click(object sender, System.EventArgs e)
    {
        _gridView.SuspendLayout();

        for (int i = 0; i < 10; i++)
        {
            _gridView.Add(_gridView.NewItem(20, new(100, 100)));
        }

        _gridView.ResumeLayout();
    }
}
