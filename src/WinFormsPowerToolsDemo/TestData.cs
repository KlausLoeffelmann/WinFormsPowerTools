using System;
using System.ComponentModel;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    public class Foo
    {
        public Guid IDContact { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NoOfChildren { get; set; }
    }

    //[FormsController(typeof(Foo), nameof(Foo.IDContact))]
    [ViewController]
    public partial class OptionFormsController : ViewControllerBase
    {
        [ViewControllerProperty] private readonly string? _firstName;
        [ViewControllerProperty] private readonly string? _lastName;

        public OptionFormsController()
        {
        }

        public string? Test { get; set; }
    }
}
