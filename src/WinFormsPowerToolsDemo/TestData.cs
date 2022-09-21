using System;
using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

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

    [ViewController]
    public partial class OptionFormsController : ObservableObject
    {

        [ViewControllerMapping(displayName: "ID:")] private string _idContact;
        [ViewControllerMapping(displayName: "First name:")] private string _firstName;
        [ViewControllerMapping(displayName: "Last name:")] private string _lastName;
        [ViewControllerMapping(displayName: "DateOfBirth:")] private DateTime? _dateOfBirth;

        public OptionFormsController()
        {
        }

        public string Test { get; set; }

        public void TestMethod()
        {
        }

        static OptionFormsController INotifyPropertyChangedDocumentFactory<OptionFormsController>.CreateDocument()
        {
            throw new NotImplementedException();
        }

    }

    [ViewController]
    public partial class CascadingFormsController : ObservableObject
    {
        [ViewControllerMapping] private OptionFormsController _optionsForms;

        public CascadingFormsController()
        {
            _optionsForms = new OptionFormsController();
            //_optionsForms.FirstName = "John";
        }

        static CascadingFormsController INotifyPropertyChangedDocumentFactory<CascadingFormsController>.CreateDocument()
        {
            throw new NotImplementedException();
        }

    }
}

