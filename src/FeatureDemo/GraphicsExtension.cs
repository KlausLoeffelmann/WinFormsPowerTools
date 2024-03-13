namespace System.Drawing;

public static class GraphicsExtension
{
    public static GraphicsCapture Capture(this Graphics graphics)
    {
        var clipBounds = graphics.VisibleClipBounds;
        return new GraphicsCapture(graphics.GetHdc(), clipBounds);
    }
        

    /// <summary>
    ///  Gets a <see cref="Graphics"/> object from an existing <see cref="Graphics"/>. 
    ///  Warning: Make sure that original graphics object isn't disposed by the time you calling this method, since 
    ///  and async method might be running when the EventHandler calling parent did already dispose the original graphics 
    ///  object. Rather capture the graphic's Hdc on the UI-Thread, and use <see cref="FromHdcAsync(IntPtr, RectangleF)"/>
    ///  inside of the async handled method./>
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="threadConfiningBounds"></param>
    /// <returns></returns>
    public static Task<Graphics> NewFromExistingAsync(this Graphics graphics, RectangleF threadConfiningBounds)
    {
        TaskCompletionSource<Graphics> tcs = new();

        if (graphics == null)
        {
            tcs.TrySetException(new ArgumentNullException(nameof(graphics)));
            return tcs.Task;
        }

        IntPtr hdc = graphics.GetHdc();
        if (hdc == IntPtr.Zero)
        {
            tcs.TrySetException(new InvalidOperationException("Graphics object does not have a device context."));
            return tcs.Task;
        }

        try
        {
            var graphicsNew = Graphics.FromHdc(hdc);
            graphicsNew.ResetTransform();
            graphicsNew.SetClip(threadConfiningBounds);
            tcs.TrySetResult(graphicsNew);
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }

        return tcs.Task;
    }


    public static Task<Graphics> NewGraphicsAsync(this GraphicsCapture capture)
        => NewGraphicsAsync(capture, capture.VisualClipBoards);
    
    public static Task<Graphics> NewGraphicsAsync(this GraphicsCapture capture, RectangleF newClipBounds)
    {
        TaskCompletionSource<Graphics> tcs = new();

        try
        {
            var graphicsNew = Graphics.FromHdc(capture.Hdc);
            capture.HdcReleased = true;

            graphicsNew.ResetTransform();
            graphicsNew.SetClip(newClipBounds);
            tcs.TrySetResult(graphicsNew);
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }

        return tcs.Task;
    }

    public static Task<Graphics> FromHwndAsync(this IntPtr hwnd, RectangleF threadConfiningBounds)
    {
        TaskCompletionSource<Graphics> tcs = new();

        if (hwnd == IntPtr.Zero)
        {
            tcs.TrySetException(new InvalidOperationException("Could not retrieve window handle."));
            return tcs.Task;
        }

        try
        {
            var graphicsNew = Graphics.FromHwnd(hwnd);
            graphicsNew.ResetTransform();
            graphicsNew.SetClip(threadConfiningBounds);
            tcs.TrySetResult(graphicsNew);
        }
        catch (Exception ex)
        {
            tcs.TrySetException(ex);
        }

        return tcs.Task;
    }
}

public struct GraphicsCapture
{
    private readonly IntPtr _hdc;

    public GraphicsCapture(IntPtr hdc, RectangleF visualClipBoards)
    {
        _hdc = hdc;
        VisualClipBoards = visualClipBoards;
    }

    public IntPtr Hdc
    {
        get
        {
            if (HdcReleased)
                throw new ObjectDisposedException(nameof(Hdc));

            try
            {
                return _hdc;
            }
            finally
            {
            }
        }
    }

    public RectangleF VisualClipBoards { get; }
    internal bool HdcReleased { get; set; }
    internal IntPtr UnreleasedHdc => _hdc;
}
