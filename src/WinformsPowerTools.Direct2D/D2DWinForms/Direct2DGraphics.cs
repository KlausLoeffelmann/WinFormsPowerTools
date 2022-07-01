using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace System.Windows.Forms.Direct2D
{
    internal class Direct2DGraphics 
        : IGraphics, 
          ISupportsBeginAndEndDraw, 
          IDirect2DImaging
    {
        private readonly Control _control;
        private bool disposedValue;

        [AllowNull]
        private Direct2DLayer _d2dLayer;

        private const int  MaxBrushesCacheSize = 10;

        public Direct2DGraphics(Control control)
        {
            _control = control;
            _control.HandleCreated += Control_HandleCreated;
            _control.Resize += Control_Resize;
            _control.HandleDestroyed += Control_HandleDestroyed;
            _control.Disposed += Control_Disposed;

            if (control.IsHandleCreated)
            {
                _d2dLayer = new(_control);
            }
        }

        private void Control_Resize(object? sender, EventArgs e)
        {
            _d2dLayer?.Resize(_control.ClientSize);
        }

        private void Control_HandleCreated(object? sender, EventArgs e)
        {
            if (_d2dLayer is not null)
            {
                _d2dLayer.Dispose();
            }

            _d2dLayer = new(_control);
        }

        private void Control_HandleDestroyed(object? sender, EventArgs e)
        {
            _d2dLayer?.Dispose();
        }

        private void Control_Disposed(object? sender, EventArgs e)
        {
            Dispose();
        }

        public void BeginDraw()
        {
            _d2dLayer?.BeginDraw();
        }

        public void EndDraw() 
            => _d2dLayer?.EndDraw();

        public Region Clip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RectangleF ClipBounds => throw new NotImplementedException();

        public void Clear(Color color) 
            => _d2dLayer!.Clear(color);

        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            var d2dPen = Direct2DPen.FromPen(pen, _d2dLayer.RenderTarget);
            _d2dLayer.DrawEllipse(x, y, width, height, d2dPen.PenBrush, d2dPen.PenSize, d2dPen.PenStyle);
        }

        public void DrawImage(Image image, float x, float y, float width, float height) 
            => _d2dLayer.DrawImage(image, x, y, width, height);

        public IDirect2DImage FromImage(Image image)
            => _d2dLayer.FromImage(image);

        public void DrawImage(IDirect2DImage image, float x, float y, float width, float height)
            => _d2dLayer.DrawImage(image, x, y, width, height);

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            var d2dPen = Direct2DPen.FromPen(pen, _d2dLayer.RenderTarget);
            _d2dLayer.DrawLine(x1, y1, x2, y2, d2dPen.PenBrush, d2dPen.PenSize, d2dPen.PenStyle);
        }

        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            var d2dPen = Direct2DPen.FromPen(pen, _d2dLayer.RenderTarget);
            _d2dLayer.DrawRectangle(x, y, width, height, d2dPen.PenBrush, d2dPen.PenSize, d2dPen.PenStyle);
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            var d2dBrush = Direct2DBrush.FromSolidBrush((SolidBrush)brush, _d2dLayer!.RenderTarget);
            _d2dLayer.FillEllipse(x, y, width, height, d2dBrush.Brush);
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            var d2dBrush = Direct2DBrush.FromSolidBrush((SolidBrush)brush, _d2dLayer!.RenderTarget);
            _d2dLayer.FillRectangle(x, y, width, height, d2dBrush.Brush);
        }

        public void DrawString(string? s, Font font, Brush brush, float x, float y)
        {
            if (brush is SolidBrush solidBrush)
            {
                var d2dFont = Direct2DFont.FromFont(font, _d2dLayer.DirectWriteFactory);
                var d2dBrush = Direct2DBrush.FromSolidBrush(solidBrush, _d2dLayer!.RenderTarget);

                _d2dLayer.DrawText(s, d2dBrush, d2dFont, x, y);
                return;
            }

            throw new ArgumentException($"Only SolidBrush brushes are supported at this time.", nameof(brush));
        }

        public void DrawString(string? s, Font font, Brush brush, float x, float y, StringFormat stringFormat)
        {
            if (brush is SolidBrush solidBrush)
            {
                var d2dFont = Direct2DFont.FromFont(font, _d2dLayer.DirectWriteFactory);
                var d2dBrush = Direct2DBrush.FromSolidBrush(solidBrush, _d2dLayer!.RenderTarget);

                _d2dLayer.DrawText(s, d2dBrush, d2dFont, x, y, stringFormat);
                return;
            }

            throw new ArgumentException($"Only SolidBrush brushes are supported at this time.", nameof(brush));
        }

        public void DrawString(string? s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string? s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat stringFormat)
        {
            throw new NotImplementedException();
        }

        public SizeF MeasureString(string? text, Font font, SizeF layoutArea)
        {
            throw new NotImplementedException();
        }

        public SizeF MeasureString(string? text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _control.HandleCreated -= Control_HandleCreated;
                    _control.HandleDestroyed -= Control_HandleDestroyed;
                    _control.Disposed -= Control_Disposed;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~D2DGraphics()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
