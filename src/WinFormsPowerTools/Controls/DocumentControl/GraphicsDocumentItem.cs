using System.Drawing;

#nullable enable

namespace System.Windows.Forms.Documents
{
    public abstract class GraphicsDocumentItem : DocumentItem
    {
        protected internal override void OnRender(PointF ScrollOffset, object DeviceContext)
            => OnRender(
                ScrollOffset,
                (Graphics)(DeviceContext
                            ?? throw new NullReferenceException(nameof(DeviceContext))));

        internal protected abstract void OnRender(PointF scrollOffset, Graphics DeviceContext);
    }
}
