using Windows.Win32.Foundation;

namespace WinForms.PowerTools.Win32
{
    public static class PInvokeExtension
    {
        public static COLORREF ToCOLORREF(this System.Drawing.Color color)
        {
            COLORREF colorRef = new((uint)(color.R | color.G << 8 | color.B << 16));
            return colorRef;
        }
    }
}
