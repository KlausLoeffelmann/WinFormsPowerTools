using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerTools.UnitTests.Support
{
    [ViewController]
    public partial class ContactController : ObservableObject
    {

        [ViewControllerMapping(displayName:"First name:")] internal string? _firstName;
        [ViewControllerMapping(displayName:"Last name:")] internal string? _lastName;
        [ViewControllerMapping(displayName: "Date of birth:")] internal DateTime? _dateOfBirth;

        public string? Test { get; set; }

        public static ContactController CreateDocument()
        {
            ContactController contact = new();
            return contact;
        }
    }
}
