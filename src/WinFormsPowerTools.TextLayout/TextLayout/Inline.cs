using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Windows.Forms.TextLayout;

/// <summary>
///  Represents a base class for inline elements, providing property change notification infrastructure.
/// </summary>
[DebuggerDisplay("{GetDebugDisplay(),nq}")]
public class Inline : INotifyPropertyChanged
{
    private string? _text;

    /// <summary>
    ///  Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///  Creates a new instance of the <see cref="Inline"/> class.
    /// </summary>
    public Inline()
    {
        Text = ToString();
    }

    /// <summary>
    ///  Creates a new instance of the <see cref="Inline"/> class with the specified text.
    /// </summary>
    /// <param name="text">
    ///  The text for the inline element.
    /// </param>
    protected Inline(string? text)
    {
        Text = text ?? ToString();
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed. The default is null.</param>
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Sets the property to a new value and raises the <see cref="PropertyChanged"/> event if the value has changed.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    /// <param name="storage">A reference to a field to store the property value.</param>
    /// <param name="value">The new value for the property.</param>
    /// <param name="propertyName">The name of the property that changed. The default is null.</param>
    /// <returns>true if the property value was changed; otherwise, false.</returns>
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged(propertyName);

        return true;
    }

    /// <summary>
    ///  Returns a string that represents the current object, by default it's the type name in curly braces.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    /// <remarks>By default it's the type name in curly braces.</remarks>
    /// <example>
    /// <code>
    ///  Inline inline = new Inline();
    ///  string inlineString = inline.ToString(); // "{Inline}"
    /// </code>
    /// </example>
    public override string ToString()
        => Text ?? $"{{{GetType().Name}}}";

    /// <summary>
    ///  Debug-only method that returns a string that represents the current object.
    /// </summary>
    protected virtual string GetDebugDisplayText()
        => $"{ToString()} {{{GetType().Name}}}";

    /// <summary>
    ///  Gets or sets the text content of this inline element.
    /// </summary>
    public virtual string? Text
    {
        get => _text;

        protected set => SetProperty(ref _text, value);
    }
}
