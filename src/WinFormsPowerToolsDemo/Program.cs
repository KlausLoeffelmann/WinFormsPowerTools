using System;
using System.Windows.Forms;
using WinFormsPowerTools.AutoLayout;
using WinFormsPowerToolsDemo.D2DSamples;

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
            Application.Run(new DirectWriteTestForm());
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
