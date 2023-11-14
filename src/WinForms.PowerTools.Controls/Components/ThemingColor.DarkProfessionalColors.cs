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

        public override Color MenuItemPressedGradientBegin => _darkThemeColors.ControlLightLight;
        public override Color MenuItemPressedGradientMiddle => _darkThemeColors.ControlLightLight;
        public override Color MenuItemPressedGradientEnd => _darkThemeColors.ControlLightLight;
        public override Color MenuItemSelected => _darkThemeColors.ControlText;
        public override Color MenuItemSelectedGradientBegin => _darkThemeColors.ControlLight;
        public override Color MenuItemSelectedGradientEnd => _darkThemeColors.ControlLight;
        public override Color MenuStripGradientBegin => _darkThemeColors.Control;
        public override Color MenuStripGradientEnd => _darkThemeColors.Control;
        public override Color StatusStripGradientBegin => _darkThemeColors.Control;
        public override Color StatusStripGradientEnd => _darkThemeColors.Control;
        public override Color ToolStripDropDownBackground => _darkThemeColors.Menu;
        public override Color ImageMarginGradientBegin => _darkThemeColors.Menu;
        public override Color ImageMarginGradientMiddle => _darkThemeColors.Menu;
        public override Color ImageMarginGradientEnd => _darkThemeColors.Menu;
        public override Color ButtonSelectedGradientBegin => _darkThemeColors.ControlLight;
        public override Color ButtonSelectedGradientMiddle => _darkThemeColors.ControlLight;
        public override Color ButtonSelectedGradientEnd => _darkThemeColors.ControlLight;
        public override Color ButtonPressedGradientBegin => _darkThemeColors.ControlLightLight;
        public override Color ButtonPressedGradientMiddle => _darkThemeColors.ControlLightLight;
        public override Color ButtonPressedGradientEnd => _darkThemeColors.ControlLightLight;
        public override Color ButtonSelectedHighlight => _darkThemeColors.Highlight;
        public override Color ToolStripPanelGradientBegin => base.ToolStripPanelGradientBegin;
        public override Color ToolStripPanelGradientEnd => base.ToolStripPanelGradientEnd;
        public override Color ToolStripContentPanelGradientBegin => base.ToolStripContentPanelGradientBegin;
        public override Color ToolStripContentPanelGradientEnd => base.ToolStripContentPanelGradientEnd;
    }
}
