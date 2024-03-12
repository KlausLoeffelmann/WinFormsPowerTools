using System.Drawing.Drawing2D;

namespace FeatureDemo.Controls;

public class AsyncSpiralContainer : Panel
{
    private bool _started = false;

    public void Start()
    {
        _started = true;
        Invalidate();
    }

    protected override async void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var random = new Random();

        if (IsAncestorSiteInDesignMode || !_started)
        {
            return;
        }

        var clientSize = ClientSize;

        var outerRenderTask = Task.Run(async () =>
        {
            // TODO: Add overload for non cancellable InvokeSyncAsync!
            nint handle = await this.InvokeSyncAsync(() => Handle, CancellationToken.None).ConfigureAwait(false);
            List<Rectangle> areas = DivideScreen(clientSize, 64);

            List<Task> tasks = [];

            foreach (var areaItem in areas)
            {
                var innerRenderTask = Task.Run(async () =>
                {
                    Graphics taskGraphics = await Graphics.FromHwndAsync(
                        handle: handle,
                        threadConfiningBounds: areaItem).ConfigureAwait(false);

                    var backgroundColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    var foregroundColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

                    await DrawSpiralAsync(taskGraphics, backgroundColor, foregroundColor, 0).ConfigureAwait(false);
                });

                tasks.Add(innerRenderTask);
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        });

        await outerRenderTask.ConfigureAwait(false);
    }

    public async Task DrawSpiralAsync(Graphics graphics, Color backgroundColor, Color foregroundColor, int delayMilliseconds)
    {
        if (graphics == null) throw new ArgumentNullException(nameof(graphics));
        if (delayMilliseconds < 0) throw new ArgumentOutOfRangeException(nameof(delayMilliseconds), "Delay must be non-negative");

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
