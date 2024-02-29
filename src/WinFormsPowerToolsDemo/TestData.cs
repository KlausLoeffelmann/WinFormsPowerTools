using System;
using System.Collections.ObjectModel;
using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerToolsDemo
{
    [ViewController]
    public partial class PurchasesController : ObservableObject
    {
        [PropertyMapping(displayName: "ID", targetHint: AutoLayoutTarget.Label)]
        private Guid _idPurchase;

        [PropertyMapping(displayName: "Article no")]
        private string? _articleNo;

        [PropertyMapping(displayName: "Description")]
        private string? _articleDescription;

        [PropertyMapping(displayName: "Unit Price")]
        private decimal? _unitPrice;

        [PropertyMapping(displayName: "Count")]
        private decimal? _count;

        [PropertyMapping(displayName: "Total Price")]
        private decimal? _totalPrice;
    }

    [ViewController]
    public partial class ContactController : ObservableObject
    {
        [PropertyMapping(displayName: "ID:", targetHint: AutoLayoutTarget.Label)]
        private Guid _idContext;

        [PropertyMapping(displayName: "Middle name:")]
        private string? _middleName;

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

        [PropertyMapping(displayName: "Purchases:", targetHint: AutoLayoutTarget.DetailsList)]
        private ObservableCollection<PurchasesController>? _purchases;

        [CommandMapping(displayName: "New document", TargetHint = AutoLayoutTarget.MenuItem)]
        public void ExecuteNewDocumentCommand(object? parameter)
        {
        }

        [CommandMapping(displayName: "OK")]
        public void ExecuteOKCommand(object? parameter)
        {
        }
    }

    [ViewController]
    public partial class MainController : ObservableObject
    {
        [PropertyMapping(displayName: "Contacts:", targetHint: AutoLayoutTarget.DetailsList)]
        private ObservableCollection<ContactController>? _contacts;
    }
}
