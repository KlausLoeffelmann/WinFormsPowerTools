using System;
using System.Windows.Forms;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct2D;
using Windows.Win32.Graphics.Direct2D.Common;

namespace Microsoft.Maui.Graphics.D2D
{
    internal class D2DLayer
    {
        private ID2D1RenderTarget? _renderTarget;
        private IWin32Window? _window;

        private Color _strokeColor = Colors.Black;
        private ID2D1SolidColorBrush? _strokeColorCash;

        public D2DLayer(IWin32Window? window)
        {
            _window = window;
        }

        public ID2D1RenderTarget RenderTarget => _renderTarget ??= CreateRenderTarget(_window);
        public ID2D1Factory? Direct2DFactory { get; } = D2DExtensions.CreateFactory();
        public IWin32Window? Window => _window;


        public void BeginDraw()
        {
            RenderTarget.BeginDraw();
        }

        unsafe public void EndDraw()
        {
            RenderTarget.EndDraw();
        }

        unsafe public void Flush()
        {
            RenderTarget.Flush();
        }

        internal void DrawLine(float x1, float y1, float x2, float y2)
        {
            D2D_POINT_2F startPoint = new() { x = x1, y = x1 };
            D2D_POINT_2F endPoint = new() { x = x2, y = y2 };

            _renderTarget!.DrawLine(startPoint, endPoint, _strokeColorCash, 3, null);
        }

        private static ID2D1RenderTarget CreateRenderTarget(IWin32Window? window)
        {
            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            var control = Control.FromHandle(window.Handle);

            if (control is null)
            {
                throw new InvalidOperationException("No control with the given windowsHandler found.");
            }

            var factory = D2DExtensions.CreateFactory();
            if (factory is not null)
            {
                var renderTargetProperties = new D2D1_HWND_RENDER_TARGET_PROPERTIES();
                renderTargetProperties.hwnd = new HWND(window.Handle);

                var size = new D2D_SIZE_U();
                size.width = (uint)control.Width;
                size.height = (uint)control.Height;
                renderTargetProperties.pixelSize = size;

                factory.CreateHwndRenderTarget(default, renderTargetProperties, out var dcRenderTarget);
                return dcRenderTarget;
            }
            else
            {
                throw new InvalidOperationException("Couldn't create a D2D render target for the given control.");
            }
        }

        internal Color StrokeColor
        {
            get => _strokeColor;

            set
            {
                if (!Equals(_strokeColor, value))
                {
                    D2D1_COLOR_F strokeColor;

                    strokeColor.a = value.Alpha;
                    strokeColor.b = value.Blue;
                    strokeColor.g = value.Green;
                    strokeColor.r = value.Red;

                    RenderTarget.CreateSolidColorBrush(in strokeColor, null, out var strokeColorCache);
                    _strokeColorCash = strokeColorCache;
                }
            }
        }
    }
}
