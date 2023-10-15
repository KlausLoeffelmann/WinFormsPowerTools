using System.Windows.Forms.TextLayout;

namespace System.Windows.Forms.Markdown;

public partial class MarkDownPanel : Panel
{
    public event EventHandler? ParagraphChanged;

    private Paragraph? _paragraph;
    private GdiPlusFactory? _gdiPlusFactory;

    public Paragraph? Paragraph
    {
        get => _paragraph;

        set
        {
            if (value == _paragraph)
            {
                return;
            }

            _paragraph = value;
            OnParagraphChanged(EventArgs.Empty);
            Invalidate();
        }
    }

    protected virtual void OnParagraphChanged(EventArgs e)
    {
        ParagraphChanged?.Invoke(this, e);
    }

    public override string Text
    {
        get => base.Text;

        set
        {
            if (value == base.Text)
            {
                return;
            }

            base.Text = value;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (Paragraph is null)
        {
            return;
        }

        _gdiPlusFactory ??= new();
        _gdiPlusFactory.Graphics = e.Graphics;

        var layoutComponent = new TextLayoutComponent(_gdiPlusFactory);
        layoutComponent.PerformLayout(Paragraph);
    }

    public static List<Run> TestRunData()
    {
        // Prepare some demo text
        var loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. ";
        var quickBrownFox = "The quick brown fox jumps over the lazy dog. ";

        // Prepare some fonts
        var fonts = new List<Font>
        {
            new("Arial", 10f, FontStyle.Regular),
            new ("Times New Roman", 12f, FontStyle.Italic),
            new ("Courier New", 14f, FontStyle.Bold),
            new ("Verdana", 16f, FontStyle.Regular),
            new ("Georgia", 18f, FontStyle.Bold | FontStyle.Italic)
        };

        // Create a list to hold the Run instances
        var runs = new List<Run>();

        // Create two Run instances for each font: one with lorem ipsum text and one with quick brown fox text
        foreach (var font in fonts)
        {
            runs.Add(new Run(loremIpsum, font));
            runs.Add(new Run(quickBrownFox, font));
        }

        return runs;
    }
}
