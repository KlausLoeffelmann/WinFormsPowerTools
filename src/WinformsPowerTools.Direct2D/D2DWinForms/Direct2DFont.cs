using System.Drawing;
using Windows.Win32;
using Windows.Win32.Graphics.DirectWrite;

namespace System.Windows.Forms.Direct2D
{
    internal class Direct2DFont
    {
        IDWriteTextFormat _textFormat;

        private Direct2DFont(IDWriteTextFormat textFormat)
        {
            _textFormat = textFormat;
        }

        internal IDWriteTextFormat TextFormat => _textFormat;

        public static Direct2DFont FromFont(Font font, IDWriteFactory writeFactory)
        {
            var textFormat = CreateTextFormat(
                writeFactory,
                font.FontFamily.Name,
                font.Size,
                font.Bold
                    ? DWRITE_FONT_WEIGHT.DWRITE_FONT_WEIGHT_BOLD
                    : DWRITE_FONT_WEIGHT.DWRITE_FONT_WEIGHT_NORMAL,
                font.Italic
                    ? DWRITE_FONT_STYLE.DWRITE_FONT_STYLE_ITALIC
                    : DWRITE_FONT_STYLE.DWRITE_FONT_STYLE_NORMAL);

            return new Direct2DFont(textFormat);
        }

        private static IDWriteTextFormat CreateTextFormat(
            IDWriteFactory writeFactory,
            string fontFamilyname,
            float fontSize,
            DWRITE_FONT_WEIGHT fontWeight = DWRITE_FONT_WEIGHT.DWRITE_FONT_WEIGHT_NORMAL,
            DWRITE_FONT_STYLE fontStyle = DWRITE_FONT_STYLE.DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH fontStretch = DWRITE_FONT_STRETCH.DWRITE_FONT_STRETCH_NORMAL,
            string? localName = null)
        {
            writeFactory.CreateTextFormat(
                fontFamilyname,
                fontCollection: null,
                fontWeight,
                fontStyle,
                fontStretch,
                fontSize,
                localName,
                out var textFormat);

            return textFormat;
        }
    }
}
