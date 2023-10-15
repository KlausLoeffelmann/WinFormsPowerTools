using WinFormsPowerTools.TextLayout.TextLayout;

namespace System.Windows.Forms.TextLayout;

/// <summary>
/// Defines methods for text layout and measurement.
/// </summary>
public interface ITextLayoutEngine
{
    /// <summary>
    /// Draws the specified text at the specified location using the specified color.
    /// </summary>
    /// <param name="text">The text to draw.</param>
    /// <param name="location">The location at which to draw the text.</param>
    /// <param name="color">The color to use for drawing the text.</param>
    void DrawString(string text, Font font, PointF location, Color color);

    /// <summary>
    /// Measures the dimensions of the text within a specified maximum width.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <param name="maxWidth">The maximum width within which to fit the text.</param>
    /// <returns>
    /// A <see cref="TextMeasurementResult"/> containing information about the measured text.
    /// </returns>
    TextMeasurementResult MeasureString(string text, Font font, float maxWidth);
}
