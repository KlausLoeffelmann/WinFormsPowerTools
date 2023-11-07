namespace WinForms.PowerTools.Components;

public partial class ThemingColors
{
    /// <summary>
    ///  Provide colors for a dark themed ToolStrip experience.
    /// </summary>
    public class DarkProfessionalColors : ProfessionalColorTable
    {
        private readonly ThemingColors _darkThemeColors = ThemingColors.GetColors(ThemingMode.DarkMode);

        public DarkProfessionalColors(){ }

        public override Color MenuItemPressedGradientBegin => 0xFF606060.ToColor();
        public override Color MenuItemPressedGradientMiddle => 0xFF606060.ToColor();
        public override Color MenuItemPressedGradientEnd => 0xFF606060.ToColor();
        public override Color MenuItemSelected => _darkThemeColors.ControlText;
        public override Color MenuItemSelectedGradientBegin => 0xFF404040.ToColor();
        public override Color MenuItemSelectedGradientEnd => 0xFF404040.ToColor();
        public override Color MenuStripGradientBegin => _darkThemeColors.Control;
        public override Color MenuStripGradientEnd => _darkThemeColors.Control;
        public override Color StatusStripGradientBegin => _darkThemeColors.Control;
        public override Color StatusStripGradientEnd => _darkThemeColors.Control;
        public override Color ToolStripDropDownBackground => _darkThemeColors.Control;
        public override Color ImageMarginGradientBegin => _darkThemeColors.Control;
        public override Color ImageMarginGradientMiddle => _darkThemeColors.Control;
        public override Color ImageMarginGradientEnd => _darkThemeColors.Control;
    }
}
