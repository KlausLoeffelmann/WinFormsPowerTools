using System.ComponentModel;
using WinForms.PowerTools.Components;

namespace WinForms.PowerTools.Controls;

public class DarkModeMenuStrip : MenuStrip, ISupportInitialize
{
    public DarkModeMenuStrip() : base() { }

    void ISupportInitialize.BeginInit() { }

    void ISupportInitialize.EndInit()
        => this.ApplyDarkRenderer(ThemingMode.DarkMode);
}
