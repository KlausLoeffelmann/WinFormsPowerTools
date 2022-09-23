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

        public OptionFormsController()
        {
        }

        public string Test { get; set; }

        public void TestMethod()
        {
        }

        public static OptionFormsController CreateDocument()
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

