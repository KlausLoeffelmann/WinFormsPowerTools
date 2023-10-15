using System.Collections.ObjectModel;

namespace System.Windows.Forms.TextLayout;

/// <summary>
///  Represents a collection of <see cref="GlyphRun"/> objects.
/// </summary>
internal class GlyphRuns : ObservableCollection<GlyphRun>
{
    private readonly IDeviceContext _deviceContext;

    /// <summary>
    ///  Initializes a new instance of the <see cref="GlyphRuns"/> class.
    /// </summary>
    /// <param name="deviceContext">The device context.</param>
    /// <exception cref="ArgumentNullException"><paramref name="deviceContext"/> is <see langword="null"/>.</exception>
    public GlyphRuns(IDeviceContext deviceContext) : base()
    {
        _deviceContext = deviceContext;
    }

    /// <inheritdoc/>
    protected override void InsertItem(int index, GlyphRun item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        if (item.DeviceContext != _deviceContext)
            throw new ArgumentException("GlyphRun.DeviceContext must match the GlyphRuns.DeviceContext.", nameof(item));

        base.InsertItem(index, item);
    }
}
