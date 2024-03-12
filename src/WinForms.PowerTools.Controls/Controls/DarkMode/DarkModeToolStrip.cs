using System.ComponentModel;
using WinForms.PowerTools.Components;

namespace WinForms.PowerTools.Controls;

internal class DarkModeToolStrip : ToolStrip, ISupportInitialize
{
    public DarkModeToolStrip() : base() { }

    void ISupportInitialize.BeginInit() { }

    void ISupportInitialize.EndInit()
        => this.ApplyDarkRenderer(ThemingMode.DarkMode);
}
