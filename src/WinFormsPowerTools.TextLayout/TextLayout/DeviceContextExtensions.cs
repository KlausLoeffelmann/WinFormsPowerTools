namespace WinFormsPowerTools.TextLayout.TextLayout;

/// <summary>
///  Provides extension methods for the IDeviceContext interface.
/// </summary>
public static class DeviceContextExtensions
{
    /// <summary>
    ///  Casts an IDeviceContext object to a specific type, if possible.
    /// </summary>
    /// <typeparam name="T">The target type to which the IDeviceContext should be cast.</typeparam>
    /// <param name="deviceContext">The IDeviceContext object to cast.</param>
    /// <returns>An object of type T if the casting is successful; otherwise, null.</returns>
    /// <exception cref="InvalidCastException">Thrown when casting is not successful and the object is not of type T.</exception>
    public static T As<T>(this IDeviceContext deviceContext) where T : class
    {
        if (deviceContext is T result)
        {
            return result;
        }

        throw new InvalidCastException($"Cannot cast {nameof(deviceContext)} to {typeof(T).Name}.");
    }
}
