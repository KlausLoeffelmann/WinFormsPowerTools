using System.Drawing;
using System.Globalization;
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

        public static Direct2DFont FromFontAndStringFormat(
            Font font,
            StringFormat stringFormat,
            IDWriteFactory writeFactory)
        {
            var d2dFont = FromFont(font, writeFactory);

            // TODO: We need to have a cache for that, which should go into the Font-object.
            var textFormatAlignment = stringFormat.Alignment switch
            {
                StringAlignment.Near => DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_LEADING,
                StringAlignment.Far => DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_TRAILING,
                StringAlignment.Center => DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_CENTER,

                _ => throw new NotImplementedException($"Text alignment '{stringFormat.Alignment}' is not supported.")
            };

            var lineFormatAlignment = stringFormat.LineAlignment switch
            {
                StringAlignment.Near => DWRITE_PARAGRAPH_ALIGNMENT.DWRITE_PARAGRAPH_ALIGNMENT_NEAR,
                StringAlignment.Far => DWRITE_PARAGRAPH_ALIGNMENT.DWRITE_PARAGRAPH_ALIGNMENT_FAR,
                StringAlignment.Center => DWRITE_PARAGRAPH_ALIGNMENT.DWRITE_PARAGRAPH_ALIGNMENT_CENTER,

                _ => throw new NotImplementedException($"Text alignment '{stringFormat.LineAlignment}' is not supported.")
            };

            var trimmingGranularity = stringFormat.Trimming switch
            {
                StringTrimming.None => DWRITE_TRIMMING_GRANULARITY.DWRITE_TRIMMING_GRANULARITY_NONE,
                StringTrimming.EllipsisCharacter => DWRITE_TRIMMING_GRANULARITY.DWRITE_TRIMMING_GRANULARITY_CHARACTER,
                StringTrimming.EllipsisWord => DWRITE_TRIMMING_GRANULARITY.DWRITE_TRIMMING_GRANULARITY_WORD,

                _ => throw new NotImplementedException($"Text alignment '{stringFormat.Trimming}' is not supported.")
            };

            DWRITE_TRIMMING trimming = new();
            trimming.granularity = trimmingGranularity;

            d2dFont.TextFormat.SetTextAlignment(textFormatAlignment);
            d2dFont.TextFormat.SetParagraphAlignment(lineFormatAlignment);
            d2dFont.TextFormat.SetTrimming(trimming, null);

            return d2dFont;
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
            if (string.IsNullOrEmpty(localName))
            {
                localName = CultureInfo.CurrentCulture.Name;
            }

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
