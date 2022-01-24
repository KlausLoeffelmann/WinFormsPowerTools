using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.D2D;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Direct2D;
using Windows.Win32.Graphics.Direct2D.Common;
using Windows.Win32.Graphics.DirectWrite;
using Windows.Win32.Graphics.Imaging;
using Windows.Win32.System.Com;

using Color = System.Drawing.Color;

namespace System.Windows.Forms.D2D
{
    internal class D2DLayer : IDisposable
    {
        private const int DefaultFontSize = 12;

        private ID2D1HwndRenderTarget? _renderTarget;
        private Control? _window;

        private bool disposedValue;

        public D2DLayer(Control? window)
        {
            _window = window;

            if (window is not null &&
                window is Control control &&
                control.IsHandleCreated)
            {
                FontName = window.Font.Name;
            }

            DirectWriteFactory = CreateDirectWriteFactory();
        }

        public ID2D1RenderTarget RenderTarget => _renderTarget ??= CreateHwndRenderTarget(_window);
        public ID2D1Factory? Direct2DFactory { get; } = CreateFactory();
        public IDWriteFactory? DirectWriteFactory { get; } 
        public Control? Window => _window;
        internal float StrokeSize { get; set; }

        public Color FontColor { get; internal set; } = Color.Black;
        public string? FontName { get; internal set; }
        public float FontSize { get; internal set; } = DefaultFontSize;

        public IDWriteTextFormat CreateTextFormat(
            string fontFamilyname,
            DWRITE_FONT_WEIGHT fontWeight = DWRITE_FONT_WEIGHT.DWRITE_FONT_WEIGHT_NORMAL,
            DWRITE_FONT_STYLE fontStyle = DWRITE_FONT_STYLE.DWRITE_FONT_STYLE_NORMAL,
            DWRITE_FONT_STRETCH fontStretch = DWRITE_FONT_STRETCH.DWRITE_FONT_STRETCH_NORMAL,
            float fontSize = DefaultFontSize,
            string? localName = null)
        {
            DirectWriteFactory.CreateTextFormat(
                fontFamilyname,
                fontCollection: null,
                fontWeight,
                fontStyle,
                fontStretch,
                fontSize,
                localName,
                out var textFormat);

            return textFormat;
        }

        private unsafe IDWriteFactory CreateDirectWriteFactory(DWRITE_FACTORY_TYPE factoryType = DWRITE_FACTORY_TYPE.DWRITE_FACTORY_TYPE_ISOLATED)
        {
            PInvoke.DWriteCreateFactory(factoryType, new Guid(D2DExtensions.IID_IDWriteFactory), out var dWriteFactory);
            var factory = dWriteFactory as IDWriteFactory;

            if (factory is null)
            {
                throw new InvalidOperationException("Couldn't create a IDWriteFactory.");
            }

            return factory;
        }

        private unsafe static ID2D1Factory? CreateFactory(
            D2D1_FACTORY_TYPE factoryType = D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED,
            D2D1_DEBUG_LEVEL debugLevel = D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_NONE)
        {
            D2D1_FACTORY_OPTIONS options = new D2D1_FACTORY_OPTIONS();
            options.debugLevel = debugLevel;

            var result = PInvoke.D2D1CreateFactory(
                D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED,
                typeof(ID2D1Factory).GUID,
                new D2D1_FACTORY_OPTIONS(),
                out var pFactory);

            var factory = Marshal.GetObjectForIUnknown(new IntPtr(pFactory)) as ID2D1Factory;
            return factory;
        }

        private static ID2D1HwndRenderTarget CreateHwndRenderTarget(Control? control)
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            var factory = CreateFactory();
            if (factory is not null)
            {
                var renderTargetProperties = new D2D1_HWND_RENDER_TARGET_PROPERTIES();
                renderTargetProperties.hwnd = new HWND(control.Handle);

                var size = new D2D_SIZE_U();
                size.width = (uint)control.Width;
                size.height = (uint)control.Height;
                renderTargetProperties.pixelSize = size;

                factory.CreateHwndRenderTarget(default, renderTargetProperties, out var dcHwndRenderTarget);

                return dcHwndRenderTarget;
            }
            else
            {
                throw new InvalidOperationException("Couldn't create a D2D render target for the given control.");
            }
        }

        private static void ResizeRenderTarget(ID2D1HwndRenderTarget renderTarget, System.Drawing.Size size)
        {
            D2D_SIZE_U newSize;
            newSize.width = (uint)size.Width;
            newSize.height = (uint)size.Height;
            renderTarget.Resize(newSize);
        }

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

        public void Resize(System.Drawing.Size size)
        {
            if (_renderTarget is null)
            {
                return;
            }

            ResizeRenderTarget(_renderTarget, size);
        }

        unsafe public void Flush()
        {
            RenderTarget.Flush();
        }

        internal void DrawLine(float x1, float y1, float x2, float y2, 
            ID2D1SolidColorBrush strokeColorBrush, float strokeSize, ID2D1StrokeStyle? strokeStyle)
        {
            D2D_POINT_2F startPoint = new() { x = x1, y = x1 };
            D2D_POINT_2F endPoint = new() { x = x2, y = y2 };

            RenderTarget!.DrawLine(startPoint, endPoint, strokeColorBrush, strokeSize, strokeStyle);
        }

        internal unsafe void DrawImage(IImage image, float x, float y, float width, float height)
        {
            if (image is D2DGdiImage d2dImage)
            {
                var hBitMapPtr = d2dImage.NativeImage.GetHbitmap();

                //global::Windows.Win32.Foundation.HRESULT.

                var hres=PInvoke.CoCreateInstance<IWICImagingFactory>(
                    typeof(IWICImagingFactory).GUID, 
                    pUnkOuter: null,
                    CLSCTX.CLSCTX_INPROC_SERVER,
                    out var imageFactory);

                D2D_SIZE_U size = new() { width = (uint)width, height = (uint)height };
                var bytes = image.AsBytes();
                var paint = image.AsPaint();


                fixed (byte* pBytes = &bytes[0])
                {
                    //ID2D1Bitmap bitmap = RenderTarget.CreateBitmapFromWicBitmap()
                    //    size,
                    //    (void*)pBytes, image.AsPaint.
    
                    //    );

                    //RenderTarget!.DrawBitmap();
                }
            }
        }

        internal void DrawRectangle(float x, float y, float width, float height,
            ID2D1SolidColorBrush strokeColorBrush, float strokeSize, ID2D1StrokeStyle? strokeStyle)
        {
            D2D_RECT_F rrectangle;
            rrectangle = new() { left = x, top = y, right = x + width, bottom = y + height };
            RenderTarget!.DrawRectangle(rrectangle, strokeColorBrush, strokeSize, strokeStyle);
        }

        internal void FillRectangle(float x, float y, float width, float height, ID2D1SolidColorBrush fillColorBrush)
        {
            D2D_RECT_F rrectangle;
            rrectangle = new() { left = x, top = y, right = x + width, bottom = y + height };
            RenderTarget!.FillRectangle(rrectangle, fillColorBrush);
        }

        internal void DrawRoundedRectangle(float x, float y, float width, float height, float cornerRadius,
            ID2D1SolidColorBrush strokeColorBrush, float strokeSize, ID2D1StrokeStyle? strokeStyle)
        {
            D2D1_ROUNDED_RECT rrectangle;
            rrectangle.rect = new() { left = x, top = y, right = x + width, bottom = y + height };
            rrectangle.radiusX = cornerRadius;
            rrectangle.radiusY = cornerRadius;

            RenderTarget!.DrawRoundedRectangle(rrectangle, strokeColorBrush, strokeSize, strokeStyle);
        }

        internal void FillRoundedRectangle(float x, float y, float width, float height, 
            float cornerRadius, ID2D1SolidColorBrush fillColorBrush)
        {
            D2D1_ROUNDED_RECT rrectangle;
            rrectangle.rect = new() { left = x, top = y, right = x + width, bottom = y + height };
            rrectangle.radiusX = cornerRadius;
            rrectangle.radiusY = cornerRadius;

            RenderTarget!.FillRoundedRectangle(rrectangle, fillColorBrush);
        }

        internal void DrawArc(float x1, float y1, float x2, float y2, float startAngle, float endAngle, bool clockwise, bool closed,
                        ID2D1SolidColorBrush strokeColorBrush, float strokeSize, ID2D1StrokeStyle? strokeStyle)
        {
            // We need to create a Path with a D2D1_ARC_SEGMENT, and then draw that path.
        }

        internal void DrawEllipse(float x, float y, float width, float height,
            ID2D1SolidColorBrush strokeColorBrush, float strokeSize, ID2D1StrokeStyle? strokeStyle)

        {
            D2D1_ELLIPSE ellipse;
            ellipse.point = new() { x = x, y = y };
            ellipse.radiusX = width;
            ellipse.radiusY = height;

            RenderTarget!.DrawEllipse(ellipse, strokeColorBrush, strokeSize, strokeStyle);
        }

        internal void FillEllipse(float x, float y, float width, float height, ID2D1SolidColorBrush fillColorBrush)
        {
            D2D1_ELLIPSE ellipse;
            ellipse.point = new() { x = x, y = y };
            ellipse.radiusX = width;
            ellipse.radiusY = height;

            RenderTarget!.FillEllipse(ellipse, fillColorBrush);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                if (_renderTarget is not null)
                {
                    Marshal.FinalReleaseComObject(RenderTarget);
                    _renderTarget = null;
                }

                disposedValue = true;
            }
        }

        ~D2DLayer()
        {
            // do not change this code. put cleanup code in 'dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
