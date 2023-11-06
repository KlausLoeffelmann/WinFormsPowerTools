namespace WinForms.PowerTools.Components;

public class ThemingColors
{
    private static ThemingColors? _darkModeColors;
    private static ThemingColors? _lightModeColors;

    public static ThemingColors GetColors(ThemingMode theming)
    {
        return theming == ThemingMode.DarkMode
            ? _darkModeColors ??= GetDefaultDarkModeColors()
            : _lightModeColors ??= GetDefaultLightModeColors();
    }

    public static ThemingColors LightModeTheme { get; } = GetDefaultLightModeColors();

    public Color ActiveBorder { get; set; }
    public Color ActiveCaption { get; set; }
    public Color ActiveCaptionText { get; set; }
    public Color AppWorkspace { get; set; }
    public Color ButtonFace { get; set; }
    public Color ButtonHighlight { get; set; }
    public Color ButtonShadow { get; set; }
    public Color Control { get; set; }
    public Color ControlDark { get; set; }
    public Color ControlDarkDark { get; set; }
    public Color ControlLight { get; set; }
    public Color ControlLightLight { get; set; }
    public Color ControlText { get; set; }
    public Color Desktop { get; set; }
    public Color GradientActiveCaption { get; set; }
    public Color GradientInactiveCaption { get; set; }
    public Color GrayText { get; set; }
    public Color Highlight { get; set; }
    public Color HighlightText { get; set; }
    public Color HotTrack { get; set; }
    public Color InactiveBorder { get; set; }
    public Color InactiveCaption { get; set; }
    public Color InactiveCaptionText { get; set; }
    public Color Info { get; set; }
    public Color InfoText { get; set; }
    public Color Menu { get; set; }
    public Color MenuBar { get; set; }
    public Color MenuHighlight { get; set; }
    public Color MenuText { get; set; }
    public Color ScrollBar { get; set; }
    public Color Window { get; set; }
    public Color WindowFrame { get; set; }
    public Color WindowText { get; set; }

    // Initialize with SystemColors for Light Mode
    private static ThemingColors GetDefaultLightModeColors()
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

    // Initialize with custom colors for Dark Mode (Example)
    private static ThemingColors GetDefaultDarkModeColors()
    {
        return new ThemingColors
        {
            ActiveBorder = ColorTranslator.FromHtml("#505050"),
            ActiveCaption = ColorTranslator.FromHtml("#3C3C3C"),
            ActiveCaptionText = ColorTranslator.FromHtml("#FFFFFF"),
            AppWorkspace = ColorTranslator.FromHtml("#1E1E1E"),
            ButtonFace = ColorTranslator.FromHtml("#323232"),
            ButtonHighlight = ColorTranslator.FromHtml("#464646"),
            ButtonShadow = ColorTranslator.FromHtml("#282828"),
            Control = ColorTranslator.FromHtml("#202020"),
            ControlDark = ColorTranslator.FromHtml("#2D2D2D"),
            ControlDarkDark = ColorTranslator.FromHtml("#191919"),
            ControlLight = ColorTranslator.FromHtml("#3C3C3C"),
            ControlLightLight = ColorTranslator.FromHtml("#464646"),
            ControlText = ColorTranslator.FromHtml("#F0F0F0"),
            Desktop = ColorTranslator.FromHtml("#141414"),
            GradientActiveCaption = ColorTranslator.FromHtml("#373737"),
            GradientInactiveCaption = ColorTranslator.FromHtml("#2D2D2D"),
            GrayText = ColorTranslator.FromHtml("#808080"),
            Highlight = ColorTranslator.FromHtml("#3399FF"),
            HighlightText = ColorTranslator.FromHtml("#FFFFFF"),
            HotTrack = ColorTranslator.FromHtml("#0066CC"),
            InactiveBorder = ColorTranslator.FromHtml("#464646"),
            InactiveCaption = ColorTranslator.FromHtml("#282828"),
            InactiveCaptionText = ColorTranslator.FromHtml("#808080"),
            Info = ColorTranslator.FromHtml("#404040"),
            InfoText = ColorTranslator.FromHtml("#FFFFFF"),
            Menu = ColorTranslator.FromHtml("#191919"),
            MenuBar = ColorTranslator.FromHtml("#1E1E1E"),
            MenuHighlight = ColorTranslator.FromHtml("#3399FF"),
            MenuText = ColorTranslator.FromHtml("#F0F0F0"),
            ScrollBar = ColorTranslator.FromHtml("#323232"),
            Window = ColorTranslator.FromHtml("#141414"),
            WindowFrame = ColorTranslator.FromHtml("#282828"),
            WindowText = ColorTranslator.FromHtml("#F0F0F0")
        };
    }

    /// <summary>
    ///  Provide colors for a dark themed ToolStrip experience.
    /// </summary>
    public class DarkProfessionalColors : ProfessionalColorTable
    {
        private ThemingColors _darkThemeColors = ThemingColors.GetColors(ThemingMode.DarkMode);

        public DarkProfessionalColors(){ }

        public override Color MenuItemPressedGradientBegin
            => Color.FromArgb(0xFF, 0x60, 0x60, 0x60);

        public override Color MenuItemPressedGradientMiddle
            => Color.FromArgb(0xFF, 0x60, 0x60, 0x60);

        public override Color MenuItemPressedGradientEnd
            => Color.FromArgb(0xFF, 0x60, 0x60, 0x60);

        public override Color MenuItemSelected
            => _darkThemeColors.ControlText;

        public override Color MenuItemSelectedGradientBegin
            => Color.FromArgb(0xFF, 0x40, 0x40, 0x40);

        public override Color MenuItemSelectedGradientEnd
            => Color.FromArgb(0xFF, 0x40, 0x40, 0x40);

        public override Color MenuStripGradientBegin
            => _darkThemeColors.Control;

        public override Color MenuStripGradientEnd
            => _darkThemeColors.Control;

        public override Color StatusStripGradientBegin
            => _darkThemeColors.Control;

        public override Color StatusStripGradientEnd
            => _darkThemeColors.Control;

        public override Color ToolStripDropDownBackground
            => _darkThemeColors.Control;

        public override Color ImageMarginGradientBegin
            => _darkThemeColors.Control;

        public override Color ImageMarginGradientMiddle
            => _darkThemeColors.Control;

        public override Color ImageMarginGradientEnd
            => _darkThemeColors.Control;
    }
}
