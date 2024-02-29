using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    public partial class GeneratedUserControl : AutoLayoutUserControl<ContactController>
    {
        public override AutoLayoutDocument<ContactController> GetDocument()
        {
            var document = NewDocument;

            AutoLayoutGrid<ContactController> grid = document.
            SetGrid(
                columnDefinitions: new("*:>100", "*:<200"),
                rowDefinitions: new("*"),
                padding: new AutoLayoutPadding(10, 10, 10, 10)).
                AddIdContextCaptionLabel(row: 0, column: 0).AddIdContextLabel(row: 0, column: 1).
                AddFirstNameCaptionLabel(row: 1, column: 0).AddFirstNameTextEntry(row: 1, column: 1).
                AddLastNameCaptionLabel(row: 2, column: 0).AddLastNameTextEntry(row: 2, column: 1).
                AddCityCaptionLabel(row: 5, column: 0).AddCityTextEntry(row: 5, column: 1).
                AddDateOfBirthCaptionLabel(row: 6, column: 0).AddDateOfBirthTextEntry(row: 6, column: 1).
                AddMiddleNameCaptionLabel(row: 7, column: 0).AddMiddleNameTextEntry(row: 7, column: 1);

            return document;
        }
    }

    public class MainFormMenu : AutoLayoutMenuStrip<ContactController>
    {
        public override AutoLayoutMenu<ContactController> GetMenu()
        {
            var fileMenu = GetNewMenuItem(name: "fileMenu", text: "&File")
                .AddNewDocumentCommandMenuItem()
                .AddMenuItem("openMenuItem", "&Open")
                .AddMenuItem("saveMenuItem", "&Save")
                .AddMenuItem("saveAsMenuItem", "Save &As")
                .AddMenuItem("exitMenuItem", "E&xit");

            var editMenu = GetNewMenuItem(name: "editMenu", text: "&Edit");
            var viewMenu = GetNewMenuItem(name: "viewMenu", text: "&View");

            return GetNewMenu().AddMenuItem(fileMenu).AddMenuItem(editMenu).AddMenuItem(viewMenu);
        }
    }

}
