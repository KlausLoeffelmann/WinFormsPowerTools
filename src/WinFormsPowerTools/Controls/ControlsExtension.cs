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
    public static T? AsyncInvokeEx<T>(this Control control, Func<Task<T>> asyncFunc)
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

        TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

        var asyncTask = Task.Run(async () =>
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

            var asyncResult = control.BeginInvoke(() =>
            {
            });

            return asyncResult;
        });

        var result = asyncTask.GetAwaiter().GetResult();
        control.EndInvoke(result);

        return tcs.Task.Result;
    }

    /// <summary>
    ///  Invokes the specified synchronous function asynchronously on the thread that owns the control's handle.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <param name="syncFunction"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async static Task InvokeSyncAsyncEx(this Control control, Action syncFunction, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource();

        if (!control.IsHandleCreated)
        {
            tcs.TrySetException(new InvalidOperationException("Control handle not created."));

            await tcs.Task;
        }

        // We're already on the UI thread, so we spin up a new task to avoid blocking the UI thread.
        _ = control.BeginInvoke(
            () =>
            {
                try
                {
                    syncFunction();
                    tcs.TrySetResult();
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            });

        await tcs.Task.ConfigureAwait(false);
    }

    /// <summary>
    ///  Invokes the specified synchronous function asynchronously on the thread that owns the control's handle.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="control"></param>
    /// <param name="syncFunction"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async static Task<T> InvokeSyncAsyncEx<T>(this Control control, Func<T> syncFunction, CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<T>();

        if (!control.IsHandleCreated)
        {
            tcs.TrySetException(new InvalidOperationException("Control handle not created."));

            return await tcs.Task;
        }

        // We're already on the UI thread, so we spin up a new task to avoid blocking the UI thread.
        _ = control.BeginInvoke(
            () =>
            {
                try
                {
                    var result = syncFunction();
                    tcs.TrySetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            });

        return await tcs.Task.ConfigureAwait(false);
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
