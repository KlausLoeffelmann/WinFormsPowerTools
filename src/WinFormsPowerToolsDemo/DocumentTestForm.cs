using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPowerToolsDemo
{
    public partial class DocumentTestForm : Form
    {
        public DocumentTestForm()
        {
            InitializeComponent();
        }

        private void DocumentTestForm_Load(object sender, EventArgs e)
        {
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(new Button() { Text = "Test", Width = 100, Height = 25 });
            panel1.AutoScrollMinSize = new Size(1000, 1000);
            panel1.AutoScrollMargin = new Size(50, 50);
        }
    }
}
