using System.ComponentModel;

namespace WinForms.PowerTools.Components;

/// <summary>
/// Provides data for the ApplyTheming event.
/// </summary>
public class ThemingEventArgs : HandledEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ThemingEventArgs"/> class.
    /// </summary>
    /// <param name="control">The parent control or object.</param>
    /// <param name="themingMode">The mode for theming.</param>
    /// <param name="colorContainer">The container for colors.</param>
    public ThemingEventArgs(Control control, ThemingMode themingMode, ThemingColors colorContainer)
    {
        Control = control;
        ThemingMode = themingMode;
        ColorContainer = colorContainer;
    }

    /// <summary>
    /// Gets the parent control or object.
    /// </summary>
    public Control Control { get; }

    /// <summary>
    /// Gets the mode for theming.
    /// </summary>
    public ThemingMode ThemingMode { get; }

    /// <summary>
    /// Gets the container for colors.
    /// </summary>
    public ThemingColors ColorContainer { get; }
}
