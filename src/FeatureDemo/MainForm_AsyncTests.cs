using FeatureDemo.Controls;
using WinForms.PowerTools.Controls;

namespace FeatureDemo;

public partial class MainForm
{
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

    private void SetDemoInfo(string demoInfo)
    {
        _debugStatusPanel.WriteLine($"[{DateTime.Now:HH:mm:ss.ff}]: Starting {demoInfo}");
    }

    private void ResetDemoInfo() => _lblDemoName.Text = NoDemo;

    internal DebugPanel DebugPanel => null!;

    private class ColorAnimator
    {
        int _duration = 5000; // Duration of the color transition in milliseconds
        int _steps = 100; // Number of steps in the transition

        Color _startColor = Color.Blue;
        Color _endColor = Color.Red;

        int _stepDelay;
        int _i = 0;
        int _direction = 1;

        public ColorAnimator()
        {
            _stepDelay = _duration / _steps;
        }

        public static ColorAnimator Default { get; } = new ColorAnimator();

        public Color Trigger()
        {
            float ratio = (float)_i / _steps;
            int red = (int)(_startColor.R + (_endColor.R - _startColor.R) * ratio);
            int green = (int)(_startColor.G + (_endColor.G - _startColor.G) * ratio);
            int blue = (int)(_startColor.B + (_endColor.B - _startColor.B) * ratio);

            Color currentColor = Color.FromArgb(red, green, blue);
            // Set the background color here
            _i += _direction;

            if (_i == _steps || _i == 0)
            {
                _direction *= -1;
            }

            return currentColor;
        }
    }
}