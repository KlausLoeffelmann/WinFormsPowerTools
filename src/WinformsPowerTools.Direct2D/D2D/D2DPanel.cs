using System;
using System.Windows.Forms;
using Windows.Win32;
using Windows.Win32.Graphics.Direct2D;
using Windows.Win32.Graphics.Direct2D.Common;

namespace Microsoft.Maui.Graphics.D2D.WinForms
{
    public partial class D2DPanel : Control
    {
        private ID2D1RenderTarget? _renderTarget;
        private bool _baseResourcesValid;

        public D2DPanel()
        {
            ResizeRedraw = true;
        }

        private ID2D1RenderTarget? RenderTarget => _renderTarget;
        private ID2D1Factory? Direct2DFactory { get; } = D2DExtensions.CreateFactory();

        private void CreateResourcesInternal(IWin32Window window)
        {
            var factory = D2DExtensions.CreateFactory();
            if (factory is not null)
            {
                var renderTargetProperties = new D2D1_HWND_RENDER_TARGET_PROPERTIES();
                renderTargetProperties.hwnd = new Windows.Win32.Foundation.HWND(this.Handle);

                var size = new Windows.Win32.Graphics.Direct2D.Common.D2D_SIZE_U();
                size.width = (uint)this.Width;
                size.height = (uint)this.Height;
                renderTargetProperties.pixelSize = size;

                factory.CreateHwndRenderTarget(default, renderTargetProperties, out var dcRenderTarget);
                _renderTarget = dcRenderTarget;
                _baseResourcesValid = true;
            }
        }

        unsafe protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.IsAncestorSiteInDesignMode)
            {
                return;
            }

            if (!_baseResourcesValid)
            {
                CreateResourcesInternal(this);
            }

            if (_baseResourcesValid)
            {
                _renderTarget!.BeginDraw();
                OnD2DPaint();
                _renderTarget!.EndDraw();
            }
        }

        protected virtual void OnD2DPaint()
        {
            D2D_POINT_2F startPoint = new() { x = 1, y = 1 };
            D2D_POINT_2F endPoint = new() { x = ClientRectangle.Right - 1, y = ClientRectangle.Bottom - 1 };
            D2D1_COLOR_F brushColor;
            brushColor.a = 200;
            brushColor.b = 200;
            brushColor.g = 0;
            brushColor.r = 0;

            _renderTarget.CreateSolidColorBrush(in brushColor, null, out var solidColorBrush);

            _renderTarget!.DrawLine(startPoint, endPoint, solidColorBrush, 3, null);
        }
    }
}
