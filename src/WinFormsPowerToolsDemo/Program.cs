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

            Application.SetDefaultDarkMode(DarkMode.Enabled);

            Application.Run(new EmptyForm());
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
