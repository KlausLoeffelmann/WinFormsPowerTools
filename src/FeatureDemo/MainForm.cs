using FeatureDemo.Components;
using FeatureDemo.Controls;
using WinForms.PowerTools.Controls;

namespace FeatureDemo;

public partial class MainForm : Form
{
    private const string NoDemo = "(No Demo in progress.)";

    public MainForm()
    {
        InitializeComponent();
    }

    private async void Demo1_AsyncEventsDemo_Click(object sender, EventArgs e)
    {
        if (_periodicTimerComponent.IsRunning)
        {
            _periodicTimerComponent.SignalCancellation();
            ResetDemoInfo();
            return;
        }

        SetDemoInfo("Async Periodic Timer.");
        await _periodicTimerComponent.StartAsync();
    }

    private async void Demo2_FlashingTitleDemo_Click(object sender, EventArgs e)
    {
        if (_flashingTitleTimer.IsRunning)
        {
            _flashingTitleTimer.SignalCancellation();
            return;
        }

        await _flashingTitleTimer.StartAsync();
    }

    private async Task FlashingTitleTimer_EngageAsync(object sender, EngageEventArgs e)
    {
        // ******
        // DEMO 2
        // ******

        await this.InvokeSyncAsyncEx(
            () => SetWindowCaptionColor(ColorAnimator.Default.Trigger()),
            CancellationToken.None);
    }

    private async void SpiralAsyncDemo_Click(object sender, EventArgs e)
    {
        // New Async Features:
        // AsyncInvoke;
        // InvokeSyncAsync;
        // InvokeAsync;
        // Async Events.

        AsyncSpiralContainer spiralContainer = null!;

        await this.InvokeSyncAsyncEx(() =>
        {
            spiralContainer = new();

            spiralContainer.Dock = DockStyle.Fill;
            Controls.Add(spiralContainer);
            spiralContainer.BringToFront();
        });

        spiralContainer.Paint += SpiralContainer_DebugInfo;
        spiralContainer.KickOff();

        void SpiralContainer_DebugInfo(object? sender, PaintEventArgs e)
        {
            _debugStatusPanel.WriteLine($"[{DateTime.Now:HH:mm:ss.ff}]: SpiralAsyncDemo_Paint.");
        }
    }

    private async Task _periodicTimerComponent_EngageAsync(object sender, EngageEventArgs e)
    {
        // ******
        // DEMO 1
        // ******

        await InvokeAsync(
            function: () => AnimateTextAsync(
                _clockLabel,
                $"   {DateTime.Now:MM/dd - HH:mm:ss}"),
            cancellationToken: CancellationToken.None);
    }

    /// <summary>
    ///  This is a UI-bound method which 
    ///  --> a) needs to be awaited and 
    ///  --> b) has to be marshalled to the UI thread.
    /// </summary>
    private async Task AnimateTextAsync(ToolStripItem control, string text)
    {
        // Animates the text asynchronously into the control like this:
        // 
        // ******************************
        // text-->
        // ******************************

        text += "          ";
        string tempText = string.Empty;

        for (int i = 0; i < text.Length; i++)
        {
            tempText += text[i];
            control.Text = "*************************\n\r"
                + tempText
                + "\n\r*************************";

            await Task.Delay(50);
        }
    }
}
