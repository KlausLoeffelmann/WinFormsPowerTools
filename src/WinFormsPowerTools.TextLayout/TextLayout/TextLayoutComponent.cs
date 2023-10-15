using System.Collections.ObjectModel;
using System.ComponentModel;
using WinFormsPowerTools.TextLayout.TextLayout;

namespace System.Windows.Forms.TextLayout;

public class TextLayoutComponent : Component
{
    private readonly ObservableCollection<Paragraph> _paragraphs = new ObservableCollection<Paragraph>();
    private IDeviceContext _deviceContext;
    private ITextLayoutEngine _textLayout;

    public TextLayoutComponent(IDeviceContextLayoutFactory layoutFactory)
    {
        if (layoutFactory is null)
            throw new ArgumentNullException(nameof(layoutFactory));

        _deviceContext = layoutFactory.GetDeviceContext() ?? throw new NullReferenceException("DeviceContext must not be null in this context.");
        _textLayout = layoutFactory.GetDeviceTextLayoutEngine() ?? throw new NullReferenceException("TextLayoutEngine must not be null in this context.");
    }

    internal GlyphRuns PerformLayout(Paragraph paragraph)
    {
        Distance remainingWidth = paragraph.Width;
        float currentY = 0;
        float lineHeight = 0;
        List<Run> lineTextRuns = new();
        GlyphRuns glyphRuns = new(_deviceContext);

        foreach (var inline in paragraph.Inlines)
        {
            if (inline is not Run run || run.Text is null) continue;

            TextMeasurementResult result = _textLayout.MeasureString(run.Text, run.Font, remainingWidth);

            if (result.CharactersFitCount < run.Text.Length)
            {
                var fittedText = run.Text.Substring(0, result.CharactersFitCount);
                lineTextRuns.Add(new Run(fittedText, run.Font));
                ProcessLine();
                run = run.WithText(run.Text.Substring(result.CharactersFitCount));
            }
            else
            {
                lineTextRuns.Add(run);
            }

            if (paragraph.Inlines[paragraph.Inlines.Count - 1] == inline && inline is Run lastRun &&
                !string.IsNullOrEmpty(lastRun.Text) && !char.IsWhiteSpace(lastRun.Text![lastRun.Text.Length - 1]))
            {
                lineTextRuns.Add(new Run(" ", run.Font));
            }

            lineHeight = Math.Max(lineHeight, result.ActualTextBounds.Height);
            remainingWidth -= result.CharactersFitWidth;
        }

        ProcessLine();

        return glyphRuns;

        void ProcessLine()
        {
            float currentX = 0;
            foreach (var lineTextRun in lineTextRuns)
            {
                if (lineTextRun is not Run lineRun || lineRun.Text is null) continue;

                var lineRunMeasureResult = _textLayout.MeasureString(
                    lineRun.Text, lineRun.Font, paragraph.Width);

                var glyphRun = new GlyphRun(
                    _deviceContext, lineRun, new PointF(currentX, currentY),
                    lineRunMeasureResult.ActualTextBounds);

                glyphRuns.Add(glyphRun);
                currentX += lineRunMeasureResult.CharactersFitWidth;
            }

            currentY += lineHeight;
            remainingWidth = paragraph.Width;
            lineHeight = 0;
            lineTextRuns.Clear();
        }
    }
}
