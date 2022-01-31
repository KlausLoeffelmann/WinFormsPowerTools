using Microsoft.Maui.Graphics.Text;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Windows.Win32;
using Windows.Win32.Graphics.Direct2D;
using Windows.Win32.Graphics.Direct2D.Common;

namespace Microsoft.Maui.Graphics.D2D
{
    public partial class D2DCanvas : AbstractCanvas<D2DCanvasState>
    {
        private Control? _window;
        private Color _strokeColor;
        private Color _fontColor;
        private Color _fillColor;
        private float _strokeSize;

        private ID2D1StrokeStyle? _strokeStyle;
        private ID2D1SolidColorBrush _strokeColorCache;
        private ID2D1SolidColorBrush _fillColorCache;
        private ID2D1SolidColorBrush _fontColorCache;

        public D2DCanvas() : base(CreateNewState, CreateStateCopy)
        {
            this.StrokeColor = Colors.White;
            this.FillColor = Colors.Black;
            _strokeSize = 1;
            _strokeStyle = null;
        }

        private static D2DCanvasState CreateNewState(object context)
        {
            var canvas = (D2DCanvas)context;
            return new D2DCanvasState(canvas.Window);
        }

        private static D2DCanvasState CreateStateCopy(D2DCanvasState prototype)
        {
            return new D2DCanvasState(prototype);
        }

        public void BeginDraw()
        {
            CurrentState.CurrentStateLayer.BeginDraw();
        }

        public void EndDraw()
        {
            CurrentState.CurrentStateLayer.EndDraw();
        }

        public void Clear(System.Drawing.Color color)
        {
            CurrentState.CurrentStateLayer.Clear(color);
        }

        public void Resize(System.Drawing.Size size)
        {
            CurrentState.CurrentStateLayer.Resize(size);
        }

        public Control? Window
        {
            get => _window;
            set
            {
                _window = value;
                ResetState();
            }
        }

        public override Color StrokeColor
        {
            set
            {
                if (!Equals(_strokeColor, value))
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    D2D1_COLOR_F strokeColor;

                    strokeColor.a = value.Alpha;
                    strokeColor.b = value.Blue;
                    strokeColor.g = value.Green;
                    strokeColor.r = value.Red;

                    CurrentState.CurrentStateLayer.RenderTarget.CreateSolidColorBrush(in strokeColor, null, out var strokeColorCache);

                    if (_strokeColorCache is not null)
                    {
                        Marshal.FinalReleaseComObject(_strokeColorCache);
                    }
                    _strokeColorCache = strokeColorCache;
                }
            }
        }

        public override Color FillColor
        {
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

                    CurrentState.CurrentStateLayer.RenderTarget.CreateSolidColorBrush(in fillColor, null, out var fillColorCache);

                    if (_fillColorCache is not null)
                    {
                        Marshal.FinalReleaseComObject(_fillColorCache);
                    }
                    _fillColorCache = fillColorCache;
                }
            }
        }

        public override Color FontColor
        {
            set
            {
                if (!Equals(_fontColor, value))
                {
                    if (value is null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    _fontColor = value;
                    D2D1_COLOR_F fontColor;

                    fontColor.a = value.Alpha;
                    fontColor.b = value.Blue;
                    fontColor.g = value.Green;
                    fontColor.r = value.Red;

                    CurrentState.CurrentStateLayer.RenderTarget.CreateSolidColorBrush(in fontColor, null, out var fontColorCache);

                    if (_fontColorCache is not null)
                    {
                        Marshal.FinalReleaseComObject(_fontColorCache);
                    }
                    _fontColorCache = fontColorCache;
                }
            }
        }

        public override string FontName { set => this.CurrentState.CurrentStateLayer.FontName = value; }
        public override float FontSize { set => this.CurrentState.CurrentStateLayer.FontSize = value; }
        public override float Alpha { set => throw new NotImplementedException(); }
        public override bool Antialias { set => throw new NotImplementedException(); }
        public override BlendMode BlendMode { set => throw new NotImplementedException(); }
        public override float MiterLimit { set => throw new NotImplementedException(); }
        public override LineCap StrokeLineCap { set => throw new NotImplementedException(); }
        public override LineJoin StrokeLineJoin { set => throw new NotImplementedException(); }

        protected override float NativeStrokeSize { set => this.CurrentState.CurrentStateLayer.StrokeSize = value; }

        public override void ClipPath(PathF path, WindingMode windingMode = WindingMode.NonZero)
        {
            throw new NotImplementedException();
        }

        public override void ClipRectangle(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public override void DrawImage(IImage image, float x, float y, float width, float height)
        {
        }

        public override void DrawString(string value, float x, float y, HorizontalAlignment horizontalAlignment)
        {
            throw new NotImplementedException();
        }

        public override void DrawString(string value, float x, float y, float width, float height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment, TextFlow textFlow = TextFlow.ClipBounds, float lineSpacingAdjustment = 0)
        {
            throw new NotImplementedException();
        }

        public override void DrawText(IAttributedText value, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public override void FillArc(float x, float y, float width, float height, float startAngle, float endAngle, bool clockwise)
        {
            throw new NotImplementedException();
        }

        public override void FillEllipse(float x, float y, float width, float height)
        {
            CurrentState.CurrentStateLayer.FillEllipse(x, y, width, height, _fillColorCache);
        }

        public override void FillPath(PathF path, WindingMode windingMode)
        {
            throw new NotImplementedException();
        }

        public override void FillRectangle(float x, float y, float width, float height)
        {
            CurrentState.CurrentStateLayer.FillRectangle(x, y, width, height, _fillColorCache);
        }

        public override void FillRoundedRectangle(float x, float y, float width, float height, float cornerRadius)
        {
            CurrentState.CurrentStateLayer.FillRoundedRectangle(x, y, width, height, cornerRadius, _fillColorCache);
        }

        public override void SetFillPaint(Paint paint, RectangleF rectangle)
        {
            throw new NotImplementedException();
        }

        public override void SetShadow(SizeF offset, float blur, Color color)
        {
            throw new NotImplementedException();
        }

        public override void SetToBoldSystemFont()
        {
            throw new NotImplementedException();
        }

        public override void SetToSystemFont()
        {
            throw new NotImplementedException();
        }

        public override void SubtractFromClip(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        protected override void NativeConcatenateTransform(Matrix3x2 transform)
        {
            throw new NotImplementedException();
        }

        protected override void NativeDrawArc(float x, float y, float width, float height, float startAngle, float endAngle, bool clockwise, bool closed)
        {
            CurrentState.CurrentStateLayer.DrawArc(x, y, x, y, startAngle, endAngle, clockwise, closed, _strokeColorCache, _strokeSize, _strokeStyle);
        }

        protected override void NativeDrawEllipse(float x, float y, float width, float height)
        {
            CurrentState.CurrentStateLayer.DrawEllipse(x, y, width, height, _strokeColorCache, _strokeSize, _strokeStyle);
        }

        protected override void NativeDrawLine(float x1, float y1, float x2, float y2)
        {
            CurrentState.CurrentStateLayer.DrawLine(x1, y1, x2, y2, _strokeColorCache, _strokeSize, _strokeStyle);
        }

        protected override void NativeDrawPath(PathF path)
        {
            throw new NotImplementedException();
        }

        protected override void NativeDrawRectangle(float x, float y, float width, float height)
        {
            CurrentState.CurrentStateLayer.DrawRectangle(x, y, width, height, _strokeColorCache, _strokeSize, _strokeStyle);
        }

        protected override void NativeDrawRoundedRectangle(float x, float y, float width, float height, float cornerRadius)
        {
            CurrentState.CurrentStateLayer.DrawRoundedRectangle(x, y, width, height, cornerRadius, _strokeColorCache, _strokeSize, _strokeStyle);
        }

        protected override void NativeRotate(float degrees, float radians, float x, float y)
        {
            throw new NotImplementedException();
        }

        protected override void NativeRotate(float degrees, float radians)
        {
            throw new NotImplementedException();
        }

        protected override void NativeScale(float fx, float fy)
        {
            throw new NotImplementedException();
        }

        protected override void NativeSetStrokeDashPattern(float[] pattern, float strokeSize)
        {
            throw new NotImplementedException();
        }

        protected override void NativeTranslate(float tx, float ty)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            base.Dispose();
            if (_fillColorCache is not null)
            {
                Marshal.FinalReleaseComObject(_fillColorCache);

            }

            if (_strokeColorCache is not null)
            {
                Marshal.FinalReleaseComObject(_strokeColorCache);
            }
        }
    }
}
