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

        private Color? _strokeColor;
        private Color? _fillColor;
        private ID2D1SolidColorBrush? _strokeColorCash;
        private ID2D1SolidColorBrush? _fillColorCash;
        private float _strokeWidth = 1;
        private ID2D1StrokeStyle? _strokeStyle = null;

        public D2DLayer(IWin32Window? window)
        {
            _window = window;

            if (window is not null)
            {
                StrokeColor = Colors.White;
                FillColor = Colors.Black;
            }
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

        public void Clear(System.Drawing.Color color)
        {
            D2D1_COLOR_F d2dcolor;
            d2dcolor.r = color.R;
            d2dcolor.g = color.G;
            d2dcolor.b = color.B;
            d2dcolor.a = color.A;

            RenderTarget!.Clear(d2dcolor);
        }

        unsafe public void Flush()
        {
            RenderTarget.Flush();
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

        internal Color? StrokeColor
        {
            get => _strokeColor;

            set
            {
                if (!Equals(_strokeColor, value))
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }
                    
                    _strokeColor = value;
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

        internal Color? FillColor
        {
            get => _fillColor;

            set
            {
                if (!Equals(_fillColor, value))
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _fillColor = value;
                    D2D1_COLOR_F fillColor;

                    fillColor.a = value.Alpha;
                    fillColor.b = value.Blue;
                    fillColor.g = value.Green;
                    fillColor.r = value.Red;

                    RenderTarget.CreateSolidColorBrush(in fillColor, null, out var fillColorCache);
                    _fillColorCash = fillColorCache;
                }
            }
        }

        internal void DrawLine(float x1, float y1, float x2, float y2)
        {
            D2D_POINT_2F startPoint = new() { x = x1, y = x1 };
            D2D_POINT_2F endPoint = new() { x = x2, y = y2 };

            RenderTarget!.DrawLine(startPoint, endPoint, _strokeColorCash, _strokeWidth, _strokeStyle);
        }

        internal void DrawRectangle(float x, float y, float width, float height)
        {
            D2D_RECT_F rrectangle;
            rrectangle = new() { left = x, top = y, right = x + width, bottom = y + height };
            RenderTarget!.DrawRectangle(rrectangle, _strokeColorCash, _strokeWidth, _strokeStyle);
        }

        internal void FillRectangle(float x, float y, float width, float height)
        {
            D2D_RECT_F rrectangle;
            rrectangle = new() { left = x, top = y, right = x + width, bottom = y + height };
            RenderTarget!.FillRectangle(rrectangle, _fillColorCash);
        }

        internal void DrawRoundedRectangle(float x, float y, float width, float height, float cornerRadius)
        {
            D2D1_ROUNDED_RECT rrectangle;
            rrectangle.rect = new() { left = x, top = y, right = x + width, bottom = y + height };
            rrectangle.radiusX = cornerRadius;
            rrectangle.radiusY = cornerRadius;

            RenderTarget!.DrawRoundedRectangle(rrectangle, _strokeColorCash, _strokeWidth, _strokeStyle);
        }

        internal void FillRoundedRectangle(float x, float y, float width, float height, float cornerRadius)
        {
            D2D1_ROUNDED_RECT rrectangle;
            rrectangle.rect = new() { left = x, top = y, right = x + width, bottom = y + height };
            rrectangle.radiusX = cornerRadius;
            rrectangle.radiusY = cornerRadius;

            RenderTarget!.FillRoundedRectangle(rrectangle, _fillColorCash);
        }

        internal void DrawArc(float x1, float y1, float x2, float y2, float startAngle, float endAngle, bool clockwise, bool closed)
        {
        }

        internal void DrawEllipse(float x, float y, float width, float height)
        {
            D2D1_ELLIPSE ellipse;
            ellipse.point = new() { x = x, y = y };
            ellipse.radiusX = width;
            ellipse.radiusY = height;

            RenderTarget!.DrawEllipse(ellipse, _strokeColorCash, _strokeWidth, _strokeStyle);
        }

        internal void FillEllipse(float x, float y, float width, float height)
        {
            D2D1_ELLIPSE ellipse;
            ellipse.point = new() { x = x, y = y };
            ellipse.radiusX = width;
            ellipse.radiusY = height;

            RenderTarget!.FillEllipse(ellipse, _fillColorCash);
        }
    }
}
