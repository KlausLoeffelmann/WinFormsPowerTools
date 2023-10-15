using System.Collections.ObjectModel;

namespace System.Windows.Forms.TextLayout;

/// <summary>
/// Represents a collection of inlines.
/// </summary>
public class Inlines : ObservableCollection<Inline>
{
    public Inlines() : base()
    {
    }

    public Inlines(IEnumerable<Inline> inlines) : base(inlines)
    {
    }
}
