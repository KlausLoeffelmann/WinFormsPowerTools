using System.ComponentModel;
using WinForms.PowerTools.Components;

namespace WinForms.PowerTools.Controls;

public class DarkModeStatusStrip : StatusStrip, ISupportInitialize
{
    public DarkModeStatusStrip() : base() { }

    void ISupportInitialize.BeginInit() { }

    void ISupportInitialize.EndInit()
        => this.ApplyDarkRenderer(ThemingMode.DarkMode);
}
