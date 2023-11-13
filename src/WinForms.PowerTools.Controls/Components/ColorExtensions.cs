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

    public static void ApplyDarkRenderer(this ToolStrip toolstrip)
    {
        var darkProfessionalColors = new ThemingColors.DarkProfessionalColors();

        toolstrip.BackColor = ThemingColors.DarkModeTheme.Control;
        toolstrip.ForeColor = ThemingColors.DarkModeTheme.ControlText;
        toolstrip.RenderMode= ToolStripRenderMode.Professional;
        var darkRenderer = new ToolStripProfessionalRenderer(darkProfessionalColors);
        toolstrip.Renderer = darkRenderer;
    }
}
