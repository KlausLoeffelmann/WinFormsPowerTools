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
                    columnDefinitions: new("*:>100", "*:>200"),
                    rowDefinitions: new("*"),
                    padding: new AutoLayoutPadding(10, 10, 10, 10)).
                    AddIdContextCaptionLabel(row: 0, column: 0).AddIdContextLabel(row: 0, column: 1).
                    AddFirstNameCaptionLabel(row: 1, column: 0).AddFirstNameTextEntry(row: 1, column: 1).
                    AddLastNameCaptionLabel(row: 2, column: 0).AddLastNameTextEntry(row: 2, column: 1).
                    AddAddressline1CaptionLabel(row: 3, column: 0).AddAddressline1TextEntry(row: 3, column: 1).
                    AddAddressline2CaptionLabel(row: 4, column: 0).AddAddressline2TextEntry(row: 4, column: 1).
                    AddCityCaptionLabel(row: 5, column: 0).AddCityTextEntry(row: 5, column: 1).
                    AddDateOfBirthCaptionLabel(row: 6, column: 0).AddDateOfBirthTextEntry(row: 6, column: 1);

            return document;
        }
    }
}
