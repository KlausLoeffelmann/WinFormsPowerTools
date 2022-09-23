using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    public partial class AutoLayoutTestUserControl : AutoLayoutUserControl<OptionFormsController>
    {
        protected override AutoLayoutDocument<OptionFormsController> CreateDocument(out string name, out string text)
        {
            name = "";
            text = "Options";
            var document = new AutoLayoutDocument<OptionFormsController>(name, text);
            //document.SetGridContent("mainGrid").AddChild()
            return document;
        }
    }
}
