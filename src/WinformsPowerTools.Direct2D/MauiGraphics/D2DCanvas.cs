using Microsoft.Maui.Graphics.Text;
using System;
using System.Numerics;
using System.Windows.Forms;

namespace Microsoft.Maui.Graphics.D2D
{
    public partial class D2DCanvas : AbstractCanvas<D2DCanvasState>
    {
        private IWin32Window? _window;

        public D2DCanvas() : base(CreateNewState, CreateStateCopy)
        {
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
            CurrentState.BeginDraw();
        }

        public void EndDraw()
        {
            CurrentState.EndDraw();
        }

        public IWin32Window? Window
        {
            get => _window;
            set
            {
                _window = value;
                ResetState();
            }
        }

        public override Color FillColor { set => throw new NotImplementedException(); }
        public override Color FontColor { set => throw new NotImplementedException(); }
        public override string FontName { set => throw new NotImplementedException(); }
        public override float FontSize { set => throw new NotImplementedException(); }
        public override float Alpha { set => throw new NotImplementedException(); }
        public override bool Antialias { set => throw new NotImplementedException(); }
        public override BlendMode BlendMode { set => throw new NotImplementedException(); }
        public override float MiterLimit { set => throw new NotImplementedException(); }
        public override Color StrokeColor 
        { 
            set
            {
                this.CurrentState.StrokeColor = value;
            }
        }
        public override LineCap StrokeLineCap { set => throw new NotImplementedException(); }
        public override LineJoin StrokeLineJoin { set => throw new NotImplementedException(); }
        protected override float NativeStrokeSize { set => throw new NotImplementedException(); }

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override void FillPath(PathF path, WindingMode windingMode)
        {
            throw new NotImplementedException();
        }

        public override void FillRectangle(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public override void FillRoundedRectangle(float x, float y, float width, float height, float cornerRadius)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override void NativeDrawEllipse(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        protected override void NativeDrawLine(float x1, float y1, float x2, float y2)
        {
            CurrentState.DrawLine(x1, y1, x2, y2);
        }

        protected override void NativeDrawPath(PathF path)
        {
            throw new NotImplementedException();
        }

        protected override void NativeDrawRectangle(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        protected override void NativeDrawRoundedRectangle(float x, float y, float width, float height, float cornerRadius)
        {
            throw new NotImplementedException();
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
    }
}
