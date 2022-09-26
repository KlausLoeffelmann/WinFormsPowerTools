using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    public partial class GeneratedUserControl : AutoLayoutUserControl<ContactController>
    {
        public GeneratedUserControl()
        {
            InitializeComponent();
        }

        public override AutoLayoutDocument<ContactController> GetDocument()
        {
            var document = NewDocument;

            AutoLayoutGrid<ContactController> grid = document.
                SetGrid(
                    columnDefinitions: new("*", "*:<200"),
                    rowDefinitions: new("*"),
                    padding: new AutoLayoutPadding(10, 10, 10, 10)).
                    AddFirstNameCaptionLabel(row: 0, column: 0).AddFirstNameTextEntry(row: 0, column: 1).
                    AddLastNameCaptionLabel(row: 1, column: 0).AddLastNameTextEntry(row: 1, column: 1).
                    AddAddressline1CaptionLabel(row: 2, column: 0).AddAddressline1TextEntry(row: 2, column: 1).
                    AddAddressline2CaptionLabel(row: 3, column: 0).AddAddressline2TextEntry(row: 3, column: 1).
                    AddCityCaptionLabel(row: 4, column: 0).AddCityTextEntry(row: 4, column: 1).
                    AddDateOfBirthCaptionLabel(row: 5, column: 0).AddDateOfBirthTextEntry(row: 5, column: 1);

            return document;
        }
    }
}
