using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace WinForms.PowerTools.Controls;

public class TransparentPanel : Control
{
    public TransparentPanel()
    {
        // We don't want the background painted.
        SetStyle(ControlStyles.Opaque, true);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            // The key here is to set the appropriate TransparentWindow style.
            CreateParams cp = base.CreateParams;
            
            cp.Style &= ~((int)WINDOW_STYLE.WS_CLIPCHILDREN | (int)WINDOW_STYLE.WS_CLIPSIBLINGS);

            // We do not want the OverlayControl to participate in Windows hit testing so we set the WS_DISABLED
            // flag. This allows WindowFromPoint to walk past the OverlayControl to underlying child controls. The
            // client InputShield leverages this to provide us the handle to the window that is visually under the
            // mouse pointer.
            //
            // Note that we cannot call WindowFromPoint in the server as it will stop at the InputShield, which is
            // by design. The InputShield is meant to hide the server window from direct Windows messages.
            cp.Style |= (int)WINDOW_STYLE.WS_DISABLED;
            cp.ExStyle |= (int)WINDOW_EX_STYLE.WS_EX_TRANSPARENT;
            return cp;
        }
    }

    protected override void WndProc(ref Message m)
    {
        switch ((uint)m.Msg)
        {
            case PInvoke.WM_PAINT:
                {
                    // We have to do our own painting. Control painting doesn't handle our layering on top
                    // of our transparent background.

                    // Get the update region before we call DefWndProc. After DefWndProc the update region
                    // will have been validated, leaving us with an empty update region (and nothing will
                    // be painted as a result).
                    HRGN hRGN = new();
                    var regionType = PInvoke.GetUpdateRgn((HWND)m.HWnd, hRGN, true);

                    // Call the default window procedure to paint the "background" (the underlying controls).
                    DefWndProc(ref m);
                    WmPaint(ref m, hRGN);
                    break;
                }

            default:
                base.WndProc(ref m);
                break;
        }
    }

    private void WmPaint(ref Message m, HRGN region)
    {
        // Get the actual region from the HRGN object
        // Region updateRegion = Region.FromHrgn(region);

        using Graphics graphics = Graphics.FromHwnd(Handle);

        // define the Region for the Graphics object.
        // graphics.SetClip(updateRegion, CombineMode.Replace);
        OnPaint(new PaintEventArgs(graphics,ClientRectangle));
    }
}
