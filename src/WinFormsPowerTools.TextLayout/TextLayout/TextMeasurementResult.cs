namespace WinFormsPowerTools.TextLayout.TextLayout;

/// <summary>
///  Represents the result of a text measurement operation.
/// </summary>
public struct TextMeasurementResult
{
    /// <summary>
    ///  Initializes a new instance of the <see cref="TextMeasurementResult"/> struct.
    /// </summary>
    /// <param name="actualTextBounds">The actual bounding box encompassing the text.</param>
    /// <param name="charactersFitCount">The count of characters that fit within the specified bounds.</param>
    /// <param name="widthOfFitCharacters">The width of the characters that fit within the specified bounds.</param>
    /// <param name="lastWrapOptionPosition">The position of the last character where a wrap could occur.</param>
    /// <param name="lastWrapOptionWidth">The width of the text up to the last possible wrap position.</param>
    public TextMeasurementResult(
        SizeF actualTextBounds,
        int charactersFitCount,
        float widthOfFitCharacters,
        int lastWrapOptionPosition,
        float lastWrapOptionWidth)
    {
        ActualTextBounds = actualTextBounds;
        CharactersFitCount = charactersFitCount;
        CharactersFitWidth = widthOfFitCharacters;
        LastWrapOptionPosition = lastWrapOptionPosition;
        LastWrapOptionWidth = lastWrapOptionWidth;
    }

    /// <summary>
    ///  Gets the actual bounding box encompassing the text.
    /// </summary>
    public SizeF ActualTextBounds { get; }

    /// <summary>
    ///  Gets the count of characters that fit within the specified bounds.
    /// </summary>
    public int CharactersFitCount { get; }

    /// <summary>
    ///  Gets the width of the characters that fit within the specified bounds.
    /// </summary>
    public float CharactersFitWidth { get; }

    /// <summary>
    ///  Gets the position of the last character where a wrap could occur,
    ///  considering the <see cref="CharactersFitCount"/>.
    /// </summary>
    public int LastWrapOptionPosition { get; }

    /// <summary>
    ///  Gets the width of the text up to the last possible wrap position, 
    ///  considering the specified bounds.
    /// </summary>
    public float LastWrapOptionWidth { get; }

    override public string ToString()
    {
        return $"ActualTextBounds: {ActualTextBounds}, " +
            $"CharactersFitCount: {CharactersFitCount}, " +
            $"CharactersFitWidth: {CharactersFitWidth}, " +
            $"LastWrapOptionPosition: {LastWrapOptionPosition}, " +
            $"LastWrapOptionWidth: {LastWrapOptionWidth}";
    }
}
