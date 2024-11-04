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

        for (int i = 0; i < 100; i++)
        {
            _gridView.Add(_gridView.NewItem(20, new(200, 200)));
        }

        _gridView.ResumeLayout();
    }

    private void GridViewTestForm_Load(object sender, System.EventArgs e)
    {

    }
}
