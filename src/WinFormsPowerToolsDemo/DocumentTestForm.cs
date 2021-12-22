using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Documents;

namespace WinFormsPowerToolsDemo
{
    public partial class DocumentTestForm : Form
    {
        public DocumentTestForm()
        {
            InitializeComponent();
            documentControl1.MainDocument = new Document();
        }

        private void DocumentTestForm_Load(object sender, EventArgs e)
        {
        }

        private void documentControl1_Paint(object sender, PaintEventArgs e)
        {
            for (int x=0; x< 800; x+=10)
            {
                e.Graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(x, 600));
            }
        }
    }
}
