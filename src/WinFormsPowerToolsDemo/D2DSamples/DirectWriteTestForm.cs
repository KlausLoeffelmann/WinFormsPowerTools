using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Direct2D;

namespace WinFormsPowerToolsDemo.D2DSamples
{
    public partial class DirectWriteTestForm : Form
    {
        public DirectWriteTestForm()
        {
            InitializeComponent();
        }

        private void direct2dPanel1_PaintIGraphics(object sender, GraphicsPaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.DrawString("Teststring", Font, Brushes.Black, 50, 50);

        }
    }
}
