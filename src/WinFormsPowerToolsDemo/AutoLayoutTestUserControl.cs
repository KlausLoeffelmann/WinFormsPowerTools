using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    public partial class AutoLayoutTestUserControl : AutoLayoutUserControl<OptionFormsController>
    {
        public override AutoLayoutDocument<OptionFormsController> GetDocument()
        {
            var document = new AutoLayoutDocument<OptionFormsController>(
                nameof(AutoLayoutTestUserControl),
                "Options");
            
            //document.SetGridContent("mainGrid").AddChild()
            return document;
        }
    }
}
