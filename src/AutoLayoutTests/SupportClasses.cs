using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerTools.UnitTests.Support
{
    [ViewController]
    public partial class ContactController : ObservableObject
    {
        [PropertyMapping(displayName:"First name:")] internal string? _firstName;
        [PropertyMapping(displayName:"Last name:")] internal string? _lastName;
        [PropertyMapping(displayName: "Date of birth:")] internal DateTime? _dateOfBirth;

        public string? Test { get; set; }

        public static void CreateDocuments()
        {
            
        }
    }

    public static class ContactView
    {
        
    }
}
