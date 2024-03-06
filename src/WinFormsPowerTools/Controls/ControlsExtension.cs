using System.Runtime.ExceptionServices;

namespace WinForms.PowerTools.Controls;

public static class ControlsExtension
{
    /// <summary>
    /// Executes an asynchronous function in a Task to avoid UI deadlocks, and blocks until the operation completes.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="asyncFunc">The asynchronous function to execute.</param>
    /// <returns>The result of the asynchronous operation.</returns>
    public static T? AsyncInvoke<T>(this Control control, Func<Task<T>> asyncFunc)
    {
        if (!control.IsHandleCreated)
            throw new InvalidOperationException("Control handle not created.");

        var tcs = new TaskCompletionSource<T>();

        var callback = async () =>
        {
            try
            {
                var result = await asyncFunc();
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        };

        // Marshal the asyncFunc execution back to the UI thread
        control.Invoke(async () => await Task.Run(callback));

        T? result = default;

        try
        {
            result = tcs.Task.Result;
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Throw(ex);
        }

        return result;
    }

    /// <summary>
    ///  Executes the specified asynchronous function on the thread that owns the control's handle.
    /// </summary>
    /// <param name="asyncAction">The asynchronous function to execute.</param>
    /// <param name="args">The arguments to pass to the function.</param>
    /// <returns>A task representing the operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the control's handle is not yet created.</exception>
    public static Task<object> InvokeAsync(this Control control, Func<object[], Task<object>> asyncAction, params object[] args)
    {
        var tcs = new TaskCompletionSource<object>();

        if (!control.IsHandleCreated)
        {
            tcs.SetException(new InvalidOperationException("Control handle not created."));
            return tcs.Task;
        }

        control.BeginInvoke(new Action(async () =>
        {
            try
            {
                object result = await asyncAction(args).ConfigureAwait(true);
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }));

        return tcs.Task;
    }

    /// <summary>
    ///  Executes the specified delegate on the thread that owns the control's handle.
    /// </summary>
    /// <param name="method">The delegate to execute.</param>
    /// <param name="args">The arguments to pass to the delegate.</param>
    /// <returns>A task representing the operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the control's handle is not yet created.</exception>
    public static Task<object?> InvokeAsync(this Control control, Delegate method, params object[] args)
    {
        var tcs = new TaskCompletionSource<object?>();

        control.BeginInvoke(new Action(() =>
        {
            try
            {
                object? result = method.DynamicInvoke(args);
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }));

        return tcs.Task;
    }
}
