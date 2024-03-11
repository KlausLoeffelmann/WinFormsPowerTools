using System.Runtime.ExceptionServices;

namespace WinForms.PowerTools.Controls;

public static class ControlsExtension
{
    /// <summary>
    ///  Executes an asynchronous function in a Task to avoid UI deadlocks, and blocks until the operation completes.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="control">The control on which to invoke the function.</param>
    /// <param name="asyncFunc">The asynchronous function to execute.</param>
    /// <returns>The result of the asynchronous operation.</returns>
    public static T? AsyncInvoke<T>(this Control control, Func<Task<T>> asyncFunc)
    {
        if (control is null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        if (asyncFunc is null)
        {
            throw new ArgumentNullException(nameof(asyncFunc));
        }

        if (!control.IsHandleCreated)
        {
            throw new InvalidOperationException("Control handle not created.");
        }

        // We need this to capture the result of the asynchronous operation.
        // We don't run the passed task directly, because then we couldn't return the result synchronously.
        // Instead, we run the task in a separate method and capture the result in a TaskCompletionSource.
        // Then we can block synchronously on the TaskCompletionSource's Task to get the result.
        var tcs = new TaskCompletionSource<T>();

        if (!control.InvokeRequired)
        {
            // We're already on the UI thread, so we spin up a new task to avoid blocking the UI thread.
            _ = control.Invoke(async () => await Task.Run(Callback).ConfigureAwait(false));
        }
        else
        {
            // We're already on a different thread, so we can just invoke the callback directly.
            _ = control.Invoke(async () => await Callback().ConfigureAwait(false));
        }

        T? result = default;

        try
        {
            result = tcs.Task.Result;
        }
        catch (Exception ex)
        {
            // Should the task-wrapper throw, we want to preserve the original exception.
            ExceptionDispatchInfo.Throw(ex);
        }

        return result;

        async Task Callback()
        {
            try
            {
                var result = await asyncFunc().ConfigureAwait(false);
                tcs.TrySetResult(result);
            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }
        }
    }

    /// <summary>
    ///  Invokes the specified synchronous function asynchronously on the thread that owns the control's handle.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <param name="function"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async static Task<T> InvokeSyncAsync<T>(this Control control, Func<T> function, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<T>();

        if (!control.IsHandleCreated)
        {
            tcs.TrySetException(new InvalidOperationException("Control handle not created."));

            return await tcs.Task;
        }

        var result = await Task.Run(
            () => control.Invoke(() => function()),
            cancellationToken);

        tcs.TrySetResult(result);

        return await tcs.Task;
    }

    /// <summary>
    ///  Invokes the specified asynchronous function on the thread that owns the control's handle.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <param name="function"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async static Task InvokeAsync<T>(this Control control, Func<Task> function, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource();

        if (!control.IsHandleCreated)
        {
            tcs.TrySetException(new InvalidOperationException("Control handle not created."));

            await tcs.Task;
        }

        await Task.Run(
            () => control.Invoke(async () => await function()),
            cancellationToken);

        tcs.TrySetResult();

        await tcs.Task;
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
    public static async Task<T> InvokeAsync<T>(this Control control, Func<Task<T>> asyncFunc, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<T>();

        if (!control.IsHandleCreated)
        {
            tcs.TrySetException(new InvalidOperationException("Control handle not created."));
            return await tcs.Task;
        }

        var result = await Task.Run(
            () => control.Invoke(async () => await asyncFunc()),
            cancellationToken);

        tcs.TrySetResult(result);

        return await tcs.Task;
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
    public static async Task<U> InvokeAsync<T, U>(this Control control, Func<T, Task<U>> asyncFunc, T arg, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<U>();

        if (!control.IsHandleCreated)
        {
            tcs.TrySetException(new InvalidOperationException("Control handle not created."));
            return await tcs.Task;
        }

        await Task.Run(
            () => control.Invoke(async () => await Callback()),
            cancellationToken);

        return await tcs.Task;

        async Task Callback()
        {
            try
            {
                var result = await asyncFunc(arg).ConfigureAwait(false);
                tcs.TrySetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }
    }
}
