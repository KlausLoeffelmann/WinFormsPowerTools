namespace System.Windows.Forms.TextLayout;

/// <summary>
/// Represents a line spacing.
/// </summary>
public struct LineSpacing
{
    private Distance? _distance;

    /// <summary>
    /// Initializes a new instance of the <see cref="LineSpacing"/> struct.
    /// </summary>
    /// <param name="type">The type of line spacing.</param>
    /// <param name="distance">The distance of the line spacing.</param>
    public LineSpacing(LineSpacingType type = LineSpacingType.Single, Distance? distance = default)
    {
        Type = type;

        if (type == LineSpacingType.Single || type == LineSpacingType.OneAndHalf || type == LineSpacingType.Double)
        {
            if (!distance.HasValue)
            {
                throw new ArgumentException($"Distance must be default for {nameof(LineSpacingType.Single)}, " +
                    $"{nameof(LineSpacingType.OneAndHalf)}, and {nameof(LineSpacingType.Double)} line spacing types.");
            }
        }

        _distance = distance;
    }

    /// <summary>
    /// Represents the type of line spacing.
    /// </summary>
    public enum LineSpacingType
    {
        Single,
        OneAndHalf,
        Double,
        AtLeast,
        Exactly,
        Multiple
    }

    /// <summary>
    /// Gets or sets the type of line spacing.
    /// </summary>
    public LineSpacingType Type { get; set; }

    /// <summary>
    /// Gets the distance of the line spacing.
    /// </summary>
    public Distance ToDistance(Font font)
    {
        return Type switch
        {
            LineSpacingType.Single => new Distance(font.Height),
            LineSpacingType.OneAndHalf => new Distance(font.Height * 1.5f),
            LineSpacingType.Double => new Distance(font.Height * 2f),
            LineSpacingType.AtLeast => _distance?.Value ?? 0,
            LineSpacingType.Exactly => _distance?.Value ?? 0,
            LineSpacingType.Multiple => _distance?.Value ?? 0,
            _ => throw new NotImplementedException(),
        };
    }

    override public string ToString()
    {
        return Type switch
        {
            LineSpacingType.Single => "Single",
            LineSpacingType.OneAndHalf => "OneAndHalf",
            LineSpacingType.Double => "Double",
            LineSpacingType.AtLeast => $"AtLeast({_distance})",
            LineSpacingType.Exactly => $"Exactly({_distance})",
            LineSpacingType.Multiple => $"Multiple({_distance})",
            _ => throw new NotImplementedException(),
        };
    }
}
