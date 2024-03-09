using System.Runtime.CompilerServices;
using Windows.Win32;

namespace System.Windows.Forms.DataEntryForms
{
    public static class Extensions
    {
        public static void Write(this TextBox textBox, string text)
        {
            textBox.AppendText(text);
            textBox.SelectionStart = textBox.Text.Length;
        }

        public static void WriteLine(this TextBox textBox, string text)
        {
            textBox.Write($"{text}\r\n");
        }

        public static void WriteLine(this TextBox textBox)
        {
            textBox.Write($"\r\n");
        }
    }
}
