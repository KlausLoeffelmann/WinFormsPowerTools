﻿namespace WinForms.PowerTools.Components;

public static class ColorExtensions
{
    /// <summary>
    /// Converts a uint representing an ARGB color value to a Color object.
    /// </summary>
    /// <param name="colorValue">The ARGB value as a uint.</param>
    /// <returns>The Color object.</returns>
    public static Color ToColor(this uint colorValue)
    {
        return Color.FromArgb(
            (byte)((colorValue >> 24) & 0xFF), // Alpha
            (byte)((colorValue >> 16) & 0xFF), // Red
            (byte)((colorValue >> 8) & 0xFF),  // Green
            (byte)(colorValue & 0xFF)          // Blue
        );
    }

    public static void ApplyDarkRenderer(this ToolStrip toolstrip, ThemingMode darkThemingMode)
    {
        var darkProfessionalColors = new ThemingColors.DarkProfessionalColors(darkThemingMode);

        toolstrip.BackColor = ThemingColors.DarkModeTheme.MenuBar;
        toolstrip.ForeColor = ThemingColors.DarkModeTheme.ControlText;

        toolstrip.RenderMode = ToolStripRenderMode.Professional;
        var darkRenderer = new ToolStripProfessionalRenderer(darkProfessionalColors);
        
        toolstrip.Renderer = darkRenderer;

        ApplyDarkSystemColors(toolstrip.Items);

        void ApplyDarkSystemColors(ToolStripItemCollection toolStripItems)
        {
            foreach (ToolStripItem item in toolStripItems)
            {
                item.BackColor = ThemingColors.DarkModeTheme.MenuBar;
                item.ForeColor = ThemingColors.DarkModeTheme.ControlText;

                if (item is ToolStripDropDownItem dropDownItem)
                {
                    if (dropDownItem.HasDropDownItems)
                    {
                        ApplyDarkSystemColors(dropDownItem.DropDownItems);
                    }
                }
            }
        }
    }
}
