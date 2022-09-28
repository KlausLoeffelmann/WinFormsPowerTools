using System;
using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerToolsDemo
{
    [ViewController]
    public partial class ContactController : ObservableObject
    {
        [PropertyMapping(displayName: "ID:", targetHint: AutoLayoutTarget.Label)]
        private Guid _idContext;
        
        [PropertyMapping(displayName: "First name:")] 
        private string? _firstName;
        
        [PropertyMapping(displayName: "Last name:")] 
        private string? _lastName;

        [PropertyMapping(displayName: "Address line 1:")]
        private string? _addressline1;

        [PropertyMapping(displayName: "Address line 2:")]
        private string? _addressline2;

        [PropertyMapping(displayName: "City:")]
        private string? _city;

        [PropertyMapping(displayName: "Zip:")]
        private string? _zip;

        [PropertyMapping(displayName: "Date of Birth:")] 
        private DateTime? _dateOfBirth;

        [PropertyMapping(displayName: "Is active contact")]
        private bool? _isActiveContact;

        [CommandMapping(displayName: "OK")]
        public void ExecuteDialogCommand(object parameter)
        {
        }
    }
}
