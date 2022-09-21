using WinFormsPowerTools.AutoLayout;
using WinFormsPowerTools.UnitTests.Support;

namespace WinFormsPowerTools.UnitTests.AutoLayout
{
    internal class AutoLayoutElementTests
    {
        public void SimpleElementTest()
        {
            var element = new AutoLayoutDocument<OptionFormsController>("OptionFormController", "Options");
        }
    }
}
