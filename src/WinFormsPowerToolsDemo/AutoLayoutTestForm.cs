using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;
using WinFormsPowerToolsDemo;

namespace ExtenderPropertiesTest
{
    public partial class AutoLayoutTestForm : Form
    {
        public AutoLayoutTestForm()
        {
            InitializeComponent();
            TestAutoLayout();
        }

        private void TestAutoLayout()
        {
            //var document = new AutoLayoutDocument<OptionFormsController>("Options Dialog");
            //document.Content
        }
    }
}
