using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.StandardLib.ViewControllerBaseClasses;

namespace WinFormsPowerTools.UnitTests.Support
{
    [ViewController]
    public partial class OptionFormsController : ObservableObject
    {

        [ViewControllerMapping()] internal string? _firstName;
        [ViewControllerMapping()] internal string? _lastName;
        [ViewControllerMapping()] internal string? __anotherName;

        public OptionFormsController()
        {
        }

        public string? Test { get; set; }

        static OptionFormsController INotifyPropertyChangedDocumentFactory<OptionFormsController>.CreateDocument()
        {
            throw new NotImplementedException();
        }

        public void TestMethod()
        {
        }
    }
}
