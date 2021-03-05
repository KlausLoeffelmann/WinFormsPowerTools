using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    public class Foo
    {
        public Guid IDContact { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NoOfChildren { get; set; }
    }

    //[FormsController(typeof(Foo), nameof(Foo.IDContact))]
    [FormsController]
    public partial class OptionFormsController : FormsControllerBase
    {
        [FormsControllerProperty] private string _firstName;
        [FormsControllerProperty] private string _lastName;

        public OptionFormsController()
        {

        }

        public string Test { get; set; }
    }
}
