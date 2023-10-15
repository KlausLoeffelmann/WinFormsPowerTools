using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.TextLayout;

namespace WinFormsPowerTools.TextLayout.TextLayout;

/// <summary>
/// Implements the <see cref="ITextLayout"/> interface using GDI+.
/// </summary>
public class GdiPlusTextLayoutEngine : ITextLayoutEngine
{
    private IDeviceContext? _deviceContext;
    private Graphics? _graphics;
    private readonly StringFormat _format;

    /// <summary>
    /// Initializes a new instance of the <see cref="GdiPlusTextLayoutEngine"/> class.
    /// </summary>
    /// <param name="deviceContext">The device context to use for text layout and measurement.</param>
    /// <param name="font">The font to use for text layout and measurement.</param>
    public GdiPlusTextLayoutEngine()
    {
        _format = StringFormat.GenericTypographic;
        _format.FormatFlags &= ~StringFormatFlags.NoWrap;
    }

    public void SetDeviceContext(IDeviceContext deviceContext)
    {
        _deviceContext = deviceContext;
        _graphics = _deviceContext.As<Graphics>();

        _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        _graphics.SmoothingMode = SmoothingMode.AntiAlias;
    }

    /// <summary>
    /// Draws the specified text at the specified location using the specified color.
    /// </summary>
    /// <param name="text">The text to draw.</param>
    /// <param name="location">The location at which to draw the text.</param>
    /// <param name="color">The color to use for drawing the text.</param>
    public void DrawString(string text, Font font, PointF location, Color color)
    {
        using Brush brush = new SolidBrush(color);
        if (_graphics is null)
        {
            throw new InvalidOperationException("Device context not set!");
        }

        _graphics.DrawString(text, font, brush, location, _format);
    }

    /// <summary>
    /// Measures the dimensions of the text within a specified maximum width.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <param name="maxWidth">The maximum width within which to fit the text.</param>
    /// <returns>
    /// A <see cref="TextMeasurementResult"/> containing information about the measured text.
    /// </returns>
    public TextMeasurementResult MeasureString(string text, Font font, float maxWidth)
    {
        if (_graphics is null)
        {
            throw new InvalidOperationException("Device context not set!");
        }

        SizeF textSize = _graphics.MeasureString(
            text: text,
            font: font,
            int.MaxValue,
            _format);

        return textSize.Width <= maxWidth
            ? new TextMeasurementResult(textSize, text.Length, textSize.Width, text.Length, textSize.Width)
            : HandleTextExceedingMaxWidth(text, font, maxWidth, textSize);
    }

    /// <summary>
    /// Handles the scenario where the text width exceeds the maximum width.
    /// </summary>
    /// <param name="graphics">The graphics object to use.</param>
    /// <param name="font">The font to use.</param>
    /// <param name="text">The text to measure.</param>
    /// <param name="maxWidth">The maximum width within which to fit the text.</param>
    /// <param name="textSize">The full size of the text.</param>
    /// <param name="format">The string format to use.</param>
    /// <returns>
    /// A <see cref="TextMeasurementResult"/> containing information about the measured text.
    /// </returns>
    private TextMeasurementResult HandleTextExceedingMaxWidth(
        string text, Font font, float maxWidth, SizeF textSize)
    {
        if (_graphics is null)
        {
            throw new InvalidOperationException("Device context not set!");
        }

        // Text exceeds maxWidth, truncate and render with ellipsis.
        _graphics.MeasureString(
            text: text,
            font: font,
            layoutArea: new SizeF(maxWidth, textSize.Height),
            stringFormat: StringFormat.GenericDefault,
            charactersFitted: out int charactersFitted,
            linesFilled: out _);

        text = text.Substring(0, charactersFitted);
        var charactersFitSize = _graphics.MeasureString(text, font, int.MaxValue, _format);

        // Let's find the last space or hyphen in the text _before_ the truncation point:
        int lastSpaceOrHyphenIndex = text
            .LastIndexOfAny(new char[] { ' ', '-' }, charactersFitted - 1);

        if (lastSpaceOrHyphenIndex == -1)
        {
            lastSpaceOrHyphenIndex = charactersFitted - 1;
        }

        string truncatedText = text.Substring(0, lastSpaceOrHyphenIndex + 1);
        var truncatedTextSize = _graphics.MeasureString(truncatedText, font, new SizeF(10, 10), _format);

        return new TextMeasurementResult(
            textSize,
            charactersFitted,
            charactersFitSize.Width,
            lastSpaceOrHyphenIndex + 1,
            truncatedTextSize.Width);
    }
}
