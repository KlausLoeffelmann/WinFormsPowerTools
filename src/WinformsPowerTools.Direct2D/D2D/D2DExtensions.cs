using System;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Graphics.Direct2D;

namespace Microsoft.Maui.Graphics.D2D
{
    internal static class D2DExtensions
    {
        public const string IID_ID2D1Factory = "06152247-6f50-465a-9245-118bfd3b6007";
        public const string IID_ID2D1Resource = "2cd90691-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1Geometry = "2cd906a1-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1StrokeStyle = "2cd9069d-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1SimplifiedGeometrySink = "2cd9069e-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1TessellationSink = "2cd906c1-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1RectangleGeometry = "2cd906a2-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1RoundedRectangleGeometry = "2cd906a3-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1ElipseGeometry = "2cd906a4-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1GeometryGroup = "2cd906a6-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1TransformedGeometry = "2cd906bb-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1PathGeometry = "2cd906a5-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1GeometrySink = "2cd9069f-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1DrawingStateBlock = "28506e39-ebf6-46a1-bb47-fd85565ab957";
        public const string IID_ID2D1RenderTarget = "2cd90694-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1HwndRenderTarget = "2cd90698-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1DCRenderTarget = "1c51bc64-de61-46fd-9899-63a5d8f03950";
        public const string IID_ID2D1BitmapRenderTarget = "2cd90695-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1Image = "65019f75-8da2-497c-b32c-dfa34e48ede6";
        public const string IID_ID2D1Bitmap = "a2296057-ea42-4099-983b-539fb6505426";
        public const string IID_ID2D1Brush = "2cd906a8-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1BitmapBrush = "2cd906aa-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1SolidColorBrush = "2cd906a9-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1LinearGradientBrush = "2cd906ab-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1RadialGradientBrush = "2cd906ac-12e2-11dc-9fed-001143a055f9";
        public const string IID_ID2D1GradientStopCollection = "2cd906a7-12e2-11dc-9fed-001143a055f9";

        public unsafe static ID2D1Factory? CreateFactory(
            D2D1_FACTORY_TYPE factoryType = D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED,
            D2D1_DEBUG_LEVEL debugLevel = D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_NONE)
        {
            D2D1_FACTORY_OPTIONS options=new D2D1_FACTORY_OPTIONS();
            options.debugLevel = debugLevel;

            var result = PInvoke.D2D1CreateFactory(
                D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED, 
                typeof(ID2D1Factory).GUID, 
                new D2D1_FACTORY_OPTIONS(), 
                out var pFactory);

            var factory = Marshal.GetObjectForIUnknown(new IntPtr(pFactory)) as ID2D1Factory;
            return factory;
        }
    }
}
