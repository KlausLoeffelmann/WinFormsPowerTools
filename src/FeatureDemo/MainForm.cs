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

    private void SetDemoInfo(string demoInfo) => _lblDemoName.Text = demoInfo;
    private void ResetDemoInfo() => _lblDemoName.Text = NoDemo;

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
        int duration = 5000; // Duration of the color transition in milliseconds
        int steps = 100; // Number of steps in the transition

        Color startColor = Color.Blue;
        Color endColor = Color.Red;

        int stepDelay = duration / steps;
        int i = 0;
        int direction = 1;

        while (await e.Timer.WaitForNextTickAsync())
        {
            float ratio = (float)i / steps;
            int red = (int)(startColor.R + (endColor.R - startColor.R) * ratio);
            int green = (int)(startColor.G + (endColor.G - startColor.G) * ratio);
            int blue = (int)(startColor.B + (endColor.B - startColor.B) * ratio);

            Color currentColor = Color.FromArgb(red, green, blue);
            SetWindowCaptionColor(currentColor);
            // Set the background color here
            i += direction;

            if (i == steps || i == 0)
            {
                direction *= -1;
            }
        }
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
        });

        spiralContainer.KickOff();
    }

    internal DebugPanel DebugPanel => null!;

    private static class AsyncDemosAndTests
    {
        public async static Task AwaitingOfAsynchronousUiTasksAsync(DebugPanel debugPanel)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var result = await debugPanel.InvokeSyncAsyncEx(() =>
                    {
                        debugPanel.Write($"Start writing i:{i} ...");
                        Thread.Sleep(200);
                        debugPanel.WriteLine($"End writing i:{i}.");

                        if (i == 7)
                            throw new Exception("Test Exception");

                        return true;
                    });
                }
            }
            catch (Exception ex)
            {
                debugPanel.WriteLine($"\n\r{ex.Message}\n\r\n\r{ex.StackTrace}\n\r");
            }
        }

        public async static Task AwaitingOfTaskRunningSyncMethodAsync(DebugPanel debugPanel)
        {
            try
            {
                await Task.Run(async () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var result = await debugPanel.InvokeSyncAsyncEx(() =>
                        {
                            // Sync method - no awaits here.
                            debugPanel.Write($"Start writing i:{i} ...");
                            Thread.Sleep(200);
                            debugPanel.WriteLine($"End writing i:{i}.");

                            if (i == 7)
                                throw new Exception("Test Exception");

                            return true;
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                debugPanel.WriteLine($"\n\r{ex.Message}\n\r\n\r{ex.StackTrace}\n\r");
            }
        }

        public static void RunningOfSyncLoopRunningTasks(DebugPanel debugPanel)
        {
            for (int i = 0; i < 10; i++)
            {
                _ = debugPanel.AsyncInvokeEx(async () =>
                {
                    await WriteCharacterWiseAsync($"Start writing i:{i} ...");
                    await Task.Delay(100);
                    await WriteCharacterWiseAsync($"End writing i:{i}.");

                    // Local async method:
                    async Task WriteCharacterWiseAsync(string text)
                    {
                        for (int j = 0; j < text.Length; j++)
                        {
                            await debugPanel.InvokeSyncAsyncEx(
                                () => debugPanel.Write($"{text[j]}"));
                            await Task.Delay(50);
                        }
                    }

                    return true;
                });
            }
        }
    }

    private async Task _periodicTimerComponent_EngageAsync(object sender, EngageEventArgs e)
    {
        // ******
        // DEMO 1
        // ******
        while (await e.Timer.WaitForNextTickAsync())
        {
            this.Invoke(() => _clockLabel.Text = $"{DateTime.Now:HH:mm:ss.fff}");
        }
    }
}
