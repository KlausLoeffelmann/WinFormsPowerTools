namespace WinForms.PowerTools.Components;

/// <summary>
/// Represents a collection of theming colors for application UI.
/// </summary>
public partial class ThemingColors
{
    private static ThemingColors? _darkModeColors;
    private static ThemingColors? _lightModeColors;
    private static ThemingColors? _systemColors;

    /// <summary>
    /// Retrieves the appropriate color theme based on the specified theming mode.
    /// </summary>
    /// <param name="theming">The theming mode to get the colors for.</param>
    /// <returns>A <see cref="ThemingColors"/> instance for the specified theme.</returns>
    public static ThemingColors GetColors(ThemingMode theming)
    {
        return theming switch
        {
            ThemingMode.DarkMode => _darkModeColors ??= GetDefaultDarkModeColors(),
            ThemingMode.LightMode => _lightModeColors ??= GetDefaultLightModeColors(),
            ThemingMode.System => _systemColors ??= GetDefaultSystemColors(),
            _ => throw new ArgumentOutOfRangeException(nameof(theming), theming, null)
        };
    }
    
    /// <summary>
    /// Gets the default light mode theme colors.
    /// </summary>
    public static ThemingColors LightModeTheme { get; } = GetDefaultLightModeColors();

    /// <summary>
    /// Gets the default dark mode theme colors.
    /// </summary>
    public static ThemingColors DarkModeTheme { get; } = GetDefaultDarkModeColors();

    /// <summary>
    /// Gets the default system theme colors.
    /// </summary>
    public static ThemingColors SystemTheme { get; } = GetDefaultSystemColors();

    /// <summary>
    /// Gets or sets the color of the active window border.
    /// </summary>
    public Color ActiveBorder { get; set; }

    /// <summary>
    /// Gets or sets the color of the active window caption (title bar).
    /// </summary>
    public Color ActiveCaption { get; set; }

    /// <summary>
    /// Gets or sets the color of the text in the active window caption (title bar).
    /// </summary>
    public Color ActiveCaptionText { get; set; }

    /// <summary>
    /// Gets or sets the color of the application workspace area.
    /// </summary>
    public Color AppWorkspace { get; set; }

    /// <summary>
    /// Gets or sets the face color of a 3-D element such as a button or scrollbar.
    /// </summary>
    public Color ButtonFace { get; set; }

    /// <summary>
    /// Gets or sets the highlight color of a 3-D element such as a button or scrollbar.
    /// </summary>
    public Color ButtonHighlight { get; set; }

    /// <summary>
    /// Gets or sets the shadow color of a 3-D element such as a button or scrollbar.
    /// </summary>
    public Color ButtonShadow { get; set; }

    /// <summary>
    /// Gets or sets the background color of the control.
    /// </summary>
    public Color Control { get; set; }

    /// <summary>
    /// Gets or sets the dark color of a control.
    /// </summary>
    public Color ControlDark { get; set; }

    /// <summary>
    /// Gets or sets the darkest color of a control.
    /// </summary>
    public Color ControlDarkDark { get; set; }

    /// <summary>
    /// Gets or sets the light color of a control.
    /// </summary>
    public Color ControlLight { get; set; }

    /// <summary>
    /// Gets or sets the lightest color of a control.
    /// </summary>
    public Color ControlLightLight { get; set; }

    /// <summary>
    /// Gets or sets the color of the text on controls.
    /// </summary>
    public Color ControlText { get; set; }

    /// <summary>
    /// Gets or sets the color of the desktop background.
    /// </summary>
    public Color Desktop { get; set; }

    /// <summary>
    /// Gets or sets the color at the beginning of the gradient of an active window's title bar.
    /// </summary>
    public Color GradientActiveCaption { get; set; }

    /// <summary>
    /// Gets or sets the color at the beginning of the gradient of an inactive window's title bar.
    /// </summary>
    public Color GradientInactiveCaption { get; set; }

    /// <summary>
    /// Gets or sets the color of text that is grayed out.
    /// </summary>
    public Color GrayText { get; set; }

    /// <summary>
    /// Gets or sets the color used to highlight menu items when the menu appears as a flat menu.
    /// </summary>
    public Color Highlight { get; set; }

    /// <summary>
    /// Gets or sets the color of the text of a selected menu item.
    /// </summary>
    public Color HighlightText { get; set; }

    /// <summary>
    /// Gets or sets the color used to designate a hot-tracked item.
    /// </summary>
    public Color HotTrack { get; set; }

    /// <summary>
    /// Gets or sets the color of an inactive window's border.
    /// </summary>
    public Color InactiveBorder { get; set; }

    /// <summary>
    /// Gets or sets the color of an inactive window's caption (title bar).
    /// </summary>
    public Color InactiveCaption { get; set; }

    /// <summary>
    /// Gets or sets the color of the text in an inactive window's caption (title bar).
    /// </summary>
    public Color InactiveCaptionText { get; set; }

    /// <summary>
    /// Gets or sets the background color for tooltip controls.
    /// </summary>
    public Color Info { get; set; }

    /// <summary>
    /// Gets or sets the text color for tooltip controls.
    /// </summary>
    public Color InfoText { get; set; }

    /// <summary>
    /// Gets or sets the background color of a menu.
    /// </summary>
    public Color Menu { get; set; }

    /// <summary>
    /// Gets or sets the background color of a menu bar.
    /// </summary>
    public Color MenuBar { get; set; }

    /// <summary>
    /// Gets or sets the color used to highlight menu items when the menu appears as a flat menu.
    /// </summary>
    public Color MenuHighlight { get; set; }

    /// <summary>
    /// Gets or sets the color of the text on menus.
    /// </summary>
    public Color MenuText { get; set; }

    /// <summary>
    /// Gets or sets the color of a scrollbar.
    /// </summary>
    public Color ScrollBar { get; set; }

    /// <summary>
    /// Gets or sets the background color of a window.
    /// </summary>
    public Color Window { get; set; }

    /// <summary>
    /// Gets or sets the frame color of a window.
    /// </summary>
    public Color WindowFrame { get; set; }

    /// <summary>
    /// Gets or sets the text color in a window.
    /// </summary>
    public Color WindowText { get; set; }

    private static ThemingColors GetDefaultSystemColors()
    {
        return new ThemingColors
        {
            ActiveBorder = SystemColors.ActiveBorder,
            ActiveCaption = SystemColors.ActiveCaption,
            ActiveCaptionText = SystemColors.ActiveCaptionText,
            AppWorkspace = SystemColors.AppWorkspace,
            ButtonFace = SystemColors.ButtonFace,
            ButtonHighlight = SystemColors.ButtonHighlight,
            ButtonShadow = SystemColors.ButtonShadow,
            Control = SystemColors.Control,
            ControlDark = SystemColors.ControlDark,
            ControlDarkDark = SystemColors.ControlDarkDark,
            ControlLight = SystemColors.ControlLight,
            ControlLightLight = SystemColors.ControlLightLight,
            ControlText = SystemColors.ControlText,
            Desktop = SystemColors.Desktop,
            GradientActiveCaption = SystemColors.GradientActiveCaption,
            GradientInactiveCaption = SystemColors.GradientInactiveCaption,
            GrayText = SystemColors.GrayText,
            Highlight = SystemColors.Highlight,
            HighlightText = SystemColors.HighlightText,
            HotTrack = SystemColors.HotTrack,
            InactiveBorder = SystemColors.InactiveBorder,
            InactiveCaption = SystemColors.InactiveCaption,
            InactiveCaptionText = SystemColors.InactiveCaptionText,
            Info = SystemColors.Info,
            InfoText = SystemColors.InfoText,
            Menu = SystemColors.Menu,
            MenuBar = SystemColors.MenuBar,
            MenuHighlight = SystemColors.MenuHighlight,
            MenuText = SystemColors.MenuText,
            ScrollBar = SystemColors.ScrollBar,
            Window = SystemColors.Window,
            WindowFrame = SystemColors.WindowFrame,
            WindowText = SystemColors.WindowText
        };
    }

    private static ThemingColors GetDefaultLightModeColors()
    {
        return new ThemingColors
        {
            ActiveBorder = 0xFFB4B4B4.ToColor(),
            ActiveCaption = 0xFF99B4D1.ToColor(),
            ActiveCaptionText = 0xFF000000.ToColor(),
            AppWorkspace = 0xFFFFFFFF.ToColor(),
            ButtonFace = 0xFFF0F0F0.ToColor(),
            ButtonHighlight = 0xFFFFFFFF.ToColor(),
            ButtonShadow = 0xFFA0A0A0.ToColor(),
            Control = 0xFFF0F0F0.ToColor(),
            ControlDark = 0xFFA0A0A0.ToColor(),
            ControlDarkDark = 0xFF696969.ToColor(),
            ControlLight = 0xFFE3E3E3.ToColor(),
            ControlLightLight = 0xFFFFFFFF.ToColor(),
            ControlText = 0xFF000000.ToColor(),
            Desktop = 0xFF004E98.ToColor(),
            GradientActiveCaption = 0xFFB9D1EA.ToColor(),
            GradientInactiveCaption = 0xFFD7E4F2.ToColor(),
            GrayText = 0xFF6D6D6D.ToColor(),
            Highlight = 0xFF3399FF.ToColor(),
            HighlightText = 0xFFFFFFFF.ToColor(),
            HotTrack = 0xFF0066CC.ToColor(),
            InactiveBorder = 0xFFF4F7FC.ToColor(),
            InactiveCaption = 0xFFBFCDDB.ToColor(),
            InactiveCaptionText = 0xFF000000.ToColor(),
            Info = 0xFFFFFFE1.ToColor(),
            InfoText = 0xFF000000.ToColor(),
            Menu = 0xFFF0F0F0.ToColor(),
            MenuBar = 0xFFF0F0F0.ToColor(),
            MenuHighlight = 0xFF3399FF.ToColor(),
            MenuText = 0xFF000000.ToColor(),
            ScrollBar = 0xFFC8C8C8.ToColor(),
            Window = 0xFFFFFFFF.ToColor(),
            WindowFrame = 0xFF646464.ToColor(),
            WindowText = 0xFF000000.ToColor(),
        };
    }

    // Initialize with custom colors for Dark Mode (Example)
    private static ThemingColors GetDefaultDarkModeColors()
    {
        return new ThemingColors
        {
            ActiveBorder = 0xFF505050.ToColor(),
            ActiveCaption = 0xFF3C3C3C.ToColor(),
            ActiveCaptionText = 0xFFFFFFFF.ToColor(),
            AppWorkspace = 0xFF1E1E1E.ToColor(),
            ButtonFace = 0xFF303030.ToColor(),
            ButtonHighlight = 0xFF404040.ToColor(),
            ButtonShadow = 0xFF404040.ToColor(),
            Control = 0xFF202020.ToColor(),
            ControlDark = 0xFF151515.ToColor(),
            ControlDarkDark = 0xFF101010.ToColor(),
            ControlLight = 0xFF404040.ToColor(),
            ControlLightLight = 0xFF606060.ToColor(),
            ControlText = 0xFFF0F0F0.ToColor(),
            Desktop = 0xFF141414.ToColor(),
            GradientActiveCaption = 0xFF373737.ToColor(),
            GradientInactiveCaption = 0xFF2D2D2D.ToColor(),
            GrayText = 0xFF808080.ToColor(),
            Highlight = 0xFF404040.ToColor(),
            HighlightText = 0xFFFFFFFF.ToColor(),
            HotTrack = 0xFF404040.ToColor(),
            InactiveBorder = 0xFF464646.ToColor(),
            InactiveCaption = 0xFF282828.ToColor(),
            InactiveCaptionText = 0xFF808080.ToColor(),
            Info = 0xFF404040.ToColor(),
            InfoText = 0xFFFFFFFF.ToColor(),
            Menu = 0xFF303030.ToColor(),
            MenuBar = 0xFF202020.ToColor(),
            MenuHighlight = 0xFF404040.ToColor(),
            MenuText = 0xFFF0F0F0.ToColor(),
            ScrollBar = 0xFF404040.ToColor(),
            Window = 0xFF202020.ToColor(),
            WindowFrame = 0xFF808080.ToColor(),
            WindowText = 0xFFF0F0F0.ToColor(),
        };
    }
}
