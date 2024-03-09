using System.Runtime.ExceptionServices;

namespace WinForms.PowerTools.Controls;

public static class ControlsExtension
{
    /// <summary>
    /// Executes an asynchronous function in a Task to avoid UI deadlocks, and blocks until the operation completes.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="control">The control on which to invoke the function.</param>
    /// <param name="asyncFunc">The asynchronous function to execute.</param>
    /// <returns>The result of the asynchronous operation.</returns>
    public static T? AsyncInvoke<T>(this Control control, Func<Task<T>> asyncFunc)
    {
        if (!control.IsHandleCreated)
            throw new InvalidOperationException("Control handle not created.");

        var tcs = new TaskCompletionSource<T>();

        if (!control.InvokeRequired)
        {
            // We're already on the UI thread, so we spin up a new task to avoid blocking the UI thread.
            control.Invoke(async () => await Task.Run(Callback).ConfigureAwait(false));
        }
        else
        {
            // We're already on a different thread, so we can just invoke the callback directly.
            control.Invoke(async () => await Callback().ConfigureAwait(false));
        }

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

        async Task Callback()
        {
            try
            {
                var result = await asyncFunc().ConfigureAwait(false);
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }
    }

    /// <summary>
    /// Invokes the specified asynchronous function on the thread that owns the control's handle.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="control">The control on which to invoke the function.</param>
    /// <param name="syncFunc">The synchronous function to execute asynchronously.</param>
    /// <returns>A task representing the operation and containing the result of the asynchronous operation.</returns>
    public static Task InvokeAsync(this Control control, Action syncFunc)
    {
        var tcs = new TaskCompletionSource();

        if (!control.IsHandleCreated)
        {
            tcs.SetException(new InvalidOperationException("Control handle not created."));
            return tcs.Task;
        }

        var task = Task.Run(() => control.BeginInvoke(() => CallBack()));
        return tcs.Task;

        void CallBack()
        {
            try
            {
                syncFunc();
                tcs.SetResult();
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }
    }

    /// <summary>
    /// Executes the specified asynchronous function on the thread that owns the control's handle.
    /// </summary>
    /// <typeparam name="T">The type of the input argument to be converted into the args array.</typeparam>
    /// <typeparam name="U">The return type of the asynchronous function.</typeparam>
    /// <param name="control">The control on which to invoke the function.</param>
    /// <param name="asyncFunc">The asynchronous function to execute, which takes an input of type T and returns a <see cref="Task{U}"/>.</param>
    /// <param name="input">The input of type T to be used by the asynchronous function.</param>
    /// <returns>A task representing the operation and containing the function's result of type U.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the control's handle is not yet created.</exception>
    public static Task<T> InvokeAsync<T>(this Control control, Func<T> asyncFunc)
    {
        var tcs = new TaskCompletionSource<T>();

        if (!control.IsHandleCreated)
        {
            tcs.SetException(new InvalidOperationException("Control handle not created."));
            return tcs.Task;
        }

        if (control.InvokeRequired)
        {
            control.BeginInvoke(() =>
            {
                try
                {
                    T result = asyncFunc();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return tcs.Task;
        }
        else
        {
            control.BeginInvoke(
                async () => await Task.Run(Callback).ConfigureAwait(false));

            return tcs.Task;

            Task Callback()
            {
                try
                {
                    T result = asyncFunc();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }

                return tcs.Task;
            }
        }
    }

    /// <summary>
    /// Executes the specified delegate on the thread that owns the control's handle.
    /// </summary>
    /// <param name="control">The control on which to invoke the delegate.</param>
    /// <param name="method">The delegate to execute.</param>
    /// <param name="args">The arguments to pass to the delegate.</param>
    /// <returns>A task representing the operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the control's handle is not yet created.</exception>
    //public static Task<object?> InvokeAsync(this Control control, Delegate method, params object[] args)
    //{
    //    var tcs = new TaskCompletionSource<object?>();

    //    control.BeginInvoke(new Action(() =>
    //    {
    //        try
    //        {
    //            object? result = method.DynamicInvoke(args);
    //            tcs.SetResult(result);
    //        }
    //        catch (Exception ex)
    //        {
    //            tcs.SetException(ex);
    //        }
    //    }));

    //    return tcs.Task;
    //}
}
