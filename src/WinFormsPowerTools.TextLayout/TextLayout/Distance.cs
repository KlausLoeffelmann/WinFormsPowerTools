using System.Runtime.CompilerServices;

namespace System.Windows.Forms.TextLayout;

/// <summary>
/// Represents a distance.
/// </summary>
public struct Distance
{
    private const float MillimetersPerInch = 25.4f;

    private float _value;
    private Dimension _dimension;
    private float _dpi;
    
    public const float DefaultDpi = 96f;

    public Distance()
    {
        _dimension = new Dimension();
        _dpi = DefaultDpi;
        _value = 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Distance"/> struct.
    /// </summary>
    /// <param name="value">The value of the distance.</param>
    /// <param name="dimension">The dimension of the distance.</param>
    /// <param name="dpi">The DPI of the distance.</param>
    public Distance(float value, Dimension dimension = Dimension.Pixel, float dpi = 96) 
    {
        _value = value;
        _dimension = dimension;
        _dpi = dpi;
    }

    private float CalculateUnderLayingValue()
    {
        // Depending on the dimension, convert the value to pixels.
        return Dimension switch
        {
            Dimension.Millimeter => MillimetersToPixels(Value),
            Dimension.Point => PointsToPixels(Value),
            Dimension.Inch => InchesToPixels(Value),
            _ => Value,
        };
    }

    /// <summary>
    /// Gets or sets the value of the distance.
    /// </summary>
    public float Value 
    { 
        get => _value;
        private set => _value = value;
    }

    /// <summary>
    /// Gets or sets the DPI of the distance.
    /// </summary>
    public float DPI
    {
        get => _dpi;
        private set => _dpi = value;
    }

    /// <summary>
    /// Gets the dimension of the distance.
    /// </summary>
    public Dimension Dimension
    {
        get => _dimension;
        private set => _dimension = value;
    }

    /// <summary>
    /// Gets the pixel value of the distance.
    /// </summary>
    public float Pixel
        => CalculateUnderLayingValue();

    /// <summary>
    /// Gets the millimeter value of the distance.
    /// </summary>
    public float Millimeters => PixelsToMillimeters(Pixel);

    /// <summary>
    /// Gets the point value of the distance.
    /// </summary>
    public float Points => PixelsToPoints(Pixel);

    /// <summary>
    /// Gets the inch value of the distance.
    /// </summary>
    public float Inches => PixelsToInches(Pixel);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float MillimetersToPixels(float millimeters)
        => millimeters * DPI / MillimetersPerInch;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float PixelsToMillimeters(float pixels)
        => pixels * MillimetersPerInch / DPI;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float PointsToPixels(float points)
        => points * DPI / 72f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float PixelsToPoints(float pixels)
        => pixels * 72f / DPI;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float InchesToPixels(float inches)
        => inches * DPI;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float PixelsToInches(float pixels)
        => pixels / DPI;

    /// <summary>
    /// Adds two distances together.
    /// </summary>
    public static Distance operator +(Distance left, Distance right)
        => new Distance(left.Pixel + right.Pixel);

    /// <summary>
    /// Subtracts one distance from another.
    /// </summary>
    public static Distance operator -(Distance left, Distance right)
        => new Distance(left.Pixel - right.Pixel);

    /// <summary>
    /// Multiplies a distance by a scalar value.
    /// </summary>
    public static Distance operator *(Distance left, float right)
        => new Distance(left.Pixel * right);

    /// <summary>
    /// Divides a distance by a scalar value.
    /// </summary>
    public static Distance operator /(Distance left, float right)
        => new Distance(left.Pixel / right);

    /// <summary>
    /// Determines if two distances are equal.
    /// </summary>
    public static bool operator ==(Distance left, Distance right)
        => left.Pixel == right.Pixel;

    /// <summary>
    /// Determines if two distances are not equal.
    /// </summary>
    public static bool operator !=(Distance left, Distance right)
        => left.Pixel != right.Pixel;

    /// <summary>
    /// Implicitly converts a distance to a float.
    /// </summary>
    public static implicit operator float(Distance distance)
        => distance.Pixel;

    /// <summary>
    /// Implicitly converts a float to a distance.
    /// </summary>
    public static implicit operator Distance(float value)
        => new Distance(value);

    /// <summary>
    /// Determines if two distances are equal.
    /// </summary>
    public override bool Equals(object? obj)
        => obj is Distance distance && distance.Pixel == Pixel;

    /// <summary>
    /// Gets the hash code of the distance.
    /// </summary>
    public override int GetHashCode()
        => Pixel.GetHashCode();

    /// <summary>
    /// Creates a new distance with the specified DPI.
    /// </summary>
    public Distance WithDpi(float dpi)
        => new Distance(Value, Dimension, dpi);

    public override string ToString()
        => $"{Value} {Dimension}";
}
