#nullable enable

public enum ScrollDragTrackingMode
{
    /// <summary>
    ///  Updates-while-tracking setting is taken from the system settings.
    /// </summary>
    SystemDragSetting,

    /// <summary>
    ///  We force updating while tracking.
    /// </summary>
    ForceNoDragTracking,

    /// <summary>
    ///  We force not updating while tracking. Updates are done only on track release.
    /// </summary>
    ForceDragTracking,
}
