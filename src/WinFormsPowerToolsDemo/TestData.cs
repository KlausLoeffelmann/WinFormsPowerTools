using System;
using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerToolsDemo
{
    [ViewController]
    public partial class OptionFormsController : ObservableObject
    {
        [ViewControllerMapping(displayName: "Integer value:")] private int? _integerValue;
        [ViewControllerMapping(displayName: "String value:")] private string? _stringValue;
        [ViewControllerMapping(displayName: "Date value:")] private DateTime _dateValue;
        [ViewControllerMapping(displayName: "boolValue:")] private bool? _boolValue;

        public string? Test { get; set; }

        public void TestMethod()
        {
        }

        public static OptionFormsController CreateDocument()
        {
            throw new NotImplementedException();
        }
    }

    [ViewController]
    public partial class ContactController : ObservableObject
    {
        [ViewControllerMapping(displayName: "First name:", targetHint: AutoLayoutTarget.Label)]
        private Guid _idContext;
        
        [ViewControllerMapping(displayName: "First name:")] 
        private string? _firstName;
        
        [ViewControllerMapping(displayName: "First name:")] 
        private string? _lastName;

        [ViewControllerMapping(displayName: "Address line 1:")]
        private string? _addressline1;

        [ViewControllerMapping(displayName: "Address line 2:")]
        private string? _addressline2;

        [ViewControllerMapping(displayName: "City:")]
        private string? _city;

        [ViewControllerMapping(displayName: "Zip:")]
        private string? _zip;

        [ViewControllerMapping(displayName: "Date of Birth:")] 
        private DateTime? _dateOfBirth;

        [ViewControllerMapping()]
        private bool? _isActiveContact;
    }
}
