namespace System.Windows.Forms.TextLayout;

/// <summary>
///  Interface for getting a device context for render purposes and a device-compatible text layout engine.
/// </summary>
public interface IDeviceContextLayoutFactory
{
    /// <summary>
    ///  Gets a device-compatible text layout engine.
    /// </summary>
    ITextLayoutEngine GetDeviceTextLayoutEngine();
    
    /// <summary>
    ///  Gets a device context for render purposes.
    /// </summary>
    IDeviceContext GetDeviceContext();
}
