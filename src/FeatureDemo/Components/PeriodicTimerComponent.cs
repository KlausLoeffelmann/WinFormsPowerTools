using System.ComponentModel;

namespace FeatureDemo.Components;

public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs e);

/// <summary>
///  Represents a component that provides asynchronous periodic timer functionality.
/// </summary>
public class PeriodicTimerComponent : Component
{
    private PeriodicTimer? _timer;
    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    ///  Occurs when the timer engages.
    /// </summary>
    public event AsyncEventHandler<EngageEventArgs>? EngageAsync;

    /// <summary>
    ///  Initializes a new instance of the <see cref="AsyncTimerComponent"/> class.
    /// </summary>
    public PeriodicTimerComponent()
    {
    }

    /// <summary>
    ///  Gets or sets the interval in milliseconds between timer ticks.
    /// </summary>
    [DefaultValue(500)]
    public int IntervalMs { get; set; } = 500;

    [Browsable(false)]
    public bool IsRunning => _timer != null;

    /// <summary>
    ///  Starts the timer asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token to stop the timer.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        if (cancellationToken == default)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = _cancellationTokenSource.Token;
        }

        if (_timer != null)
        {
            _timer.Dispose();
        }

        _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(IntervalMs));

        try
        {
            while (await _timer.WaitForNextTickAsync(cancellationToken))
            {
                if (EngageAsync != null)
                {
                    await EngageAsync(this, new EngageEventArgs(_timer));
                }
            }
        }
        finally
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }

    public void SignalCancellation()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            return;
        }

        throw new InvalidOperationException("No implicit cancellation token source available.");
    }
}
