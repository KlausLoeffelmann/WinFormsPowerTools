using System.ComponentModel;

namespace WinForms.PowerTools.Components;

/// <summary>
///  Provides theming capabilities to a form.
/// </summary>
[Description("A component to manage theming for WinForms.")]
public partial class ThemingComponent : BindableComponent
{
    private bool _initialized;

    /// <summary>
    ///  Gets or sets the theming mode.
    /// </summary>
    [Category("Appearance")]
    [Description("Sets the theme to DarkMode, LightMode or SystemMode.")]
    [DefaultValue(ThemingMode.LightMode)]
    [Bindable(BindableSupport.Default, BindingDirection.OneWay)]
    public ThemingMode ThemingMode
    {
        get => _themingMode;

        set
        {
            if (_themingMode != value)
            {
                _themingMode = value;
                OnThemingModeChanged();
            }
        }
    }

    /// <summary>
    ///  Raises the ThemingChanged event and applies the theme.
    /// </summary>
    protected virtual void OnThemingModeChanged()
    {
        ThemingModeChanged?.Invoke(this, EventArgs.Empty);
        if (_initialized)
        {
            ApplyTheming();
        }
    }
}
