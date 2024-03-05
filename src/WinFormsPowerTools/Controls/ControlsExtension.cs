using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms.PowerTools.Controls
{
    public static class ControlsExtension
    {
        public static Task InvokeAsync(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                return control.InvokeAsync(action);
            }
            else
            {
                action();
                return Task.CompletedTask;
            }
        }
    }
}
