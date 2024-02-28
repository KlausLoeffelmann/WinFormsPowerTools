using System.ComponentModel;
using WinForms.PowerTools.Components;

namespace WinForms.PowerTools.Controls;

public class DarkModeToolStrip : ToolStrip, ISupportInitialize
{
    public DarkModeToolStrip() : base() { }

    void ISupportInitialize.BeginInit() { }

    void ISupportInitialize.EndInit()
        => this.ApplyDarkRenderer(ThemingMode.DarkMode);
}
