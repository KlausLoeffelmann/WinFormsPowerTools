using System;
using System.Windows.Forms;
using WinForms.PowerToolsDemo;
using WinFormsPowerTools.AutoLayout;

namespace WinFormsPowerToolsDemo
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#pragma warning disable WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            Application.SetColorMode(SystemColorMode.System);
#pragma warning restore WFO5001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            Application.Run(new GridViewTestForm());
        }

        private static void GenerateForm()
        {
            var autoForm = new Form();
            var view = new OptionFormsController();
            var document = view.GetDocument("optionsFormsView", "Forms Title");
            document.SetGridContent("grid1");
        }
    }
}
