using System.Drawing.Drawing2D;
using WinForms.PowerTools.Controls;

namespace FeatureDemo.Controls;

public class AsyncSpiralContainer : Panel
{
    private bool _started = false;
    private CancellationTokenSource? _cancellationTokenSource;
    private readonly object _syncObject = new object();

    public void KickOff()
    {
        _started = true;
        Invalidate();
    }

    protected override async void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        CancellationToken cancellationToken = CancellationToken.None;

        lock (_syncObject)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
            }
            else
            {
                _cancellationTokenSource = new CancellationTokenSource();
            }

            cancellationToken = _cancellationTokenSource.Token;
        }

        await Task.Delay(100);

        var random = new Random();

        if (IsAncestorSiteInDesignMode || !_started)
        {
            return;
        }

        var clientSize = ClientSize;

        // Outer render task.
        var outerRenderTask = Task.Run(async () =>
        {
            // TODO: Add overload for non cancellable InvokeSyncAsync!
            nint handle = await this.InvokeSyncAsyncEx(
                () => Handle, CancellationToken.None).ConfigureAwait(false);

            List<Rectangle> areas = DivideScreen(clientSize, 64);

            List<Task> tasks = [];

            try
            {
                // Loop through the areas and create a task for each area.
                foreach (var areaItem in areas)
                {
                    // Each inner render task will render a spiral in a
                    // specific area using its dedicated graphics object.
                    var innerRenderTask = Task.Run(async () =>
                    {
                        // Planned API for .NET 9:
                        Graphics taskGraphics = await Graphics.FromHwndAsync(
                            handle: handle,
                            threadConfiningBounds: areaItem).ConfigureAwait(false);

                        var backgroundColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                        var foregroundColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                        // Draws our spiral.
                        await DrawSpiralAsync(
                            taskGraphics,
                            backgroundColor,
                            foregroundColor,
                            20,
                            cancellationToken).ConfigureAwait(false);


                    }, cancellationToken);

                    if (!innerRenderTask.IsCanceled)
                    {
                        tasks.Add(innerRenderTask);
                    }
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
            }
        }, cancellationToken);

        try 
        {
            await outerRenderTask.ConfigureAwait(false);
        } 
        catch 
        { 
        }

        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
    }

    public async Task DrawSpiralAsync(
        Graphics graphics,
        Color backgroundColor,
        Color foregroundColor,
        int delayMilliseconds,
        CancellationToken cancellationToken)
    {
        if (graphics == null)
            throw new ArgumentNullException(nameof(graphics));

        if (delayMilliseconds < 20)
            throw new ArgumentOutOfRangeException(nameof(delayMilliseconds), "Delay must be >=20");


        // Set the background
        graphics.Clear(backgroundColor);

        // Set up the pen with the foreground color
        using Pen pen = new Pen(foregroundColor, 2);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Define the spiral parameters
        float centerX = graphics.VisibleClipBounds.Width / 2;
        float centerY = graphics.VisibleClipBounds.Height / 2;
        float radius = Math.Min(centerX, centerY) * 0.9f; // 90% of half the smallest dimension
        int revolutions = 5;
        int pointsPerRevolution = 100;

        // Calculate the spiral points
        PointF[] points = new PointF[revolutions * pointsPerRevolution];
        for (int i = 0; i < points.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            double angle = Math.PI * 2 * i / pointsPerRevolution;
            float curRadius = radius * i / points.Length;
            points[i] = new PointF(
                centerX + curRadius * (float)Math.Cos(angle),
                centerY + curRadius * (float)Math.Sin(angle)
            );
        }

        // Draw the spiral with throttling
        for (int i = 1; i < points.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            graphics.DrawLine(pen, points[i - 1], points[i]);
            if (delayMilliseconds > 0)
            {
                await Task.Delay(delayMilliseconds).ConfigureAwait(false);
            }
        }
    }

    public static List<Rectangle> DivideScreen(Size clientSize, int areasCount)
    {
        if (areasCount <= 0)
        {
            throw new ArgumentException("Areas count must be positive.", nameof(areasCount));
        }

        var clientWidth = clientSize.Width;
        var clientHeight = clientSize.Height;
        var rectangles = new List<Rectangle>();

        if (areasCount == 1)
        {
            rectangles.Add(new Rectangle(0, 0, clientWidth, clientHeight));
            return rectangles;
        }

        int rows = (int)Math.Sqrt(areasCount);
        int cols = (int)Math.Ceiling((double)areasCount / rows);

        int width = clientWidth / cols;
        int height = clientHeight / rows;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (rectangles.Count < areasCount)
                {
                    int x = col * width;
                    int y = row * height;
                    rectangles.Add(new Rectangle(x, y, width, height));
                }
            }
        }

        return rectangles;
    }
}
