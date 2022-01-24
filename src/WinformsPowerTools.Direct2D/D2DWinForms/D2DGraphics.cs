using System.Collections.Generic;
using System.Drawing;
using Windows.Win32.Graphics.Direct2D;

namespace System.Windows.Forms.D2D
{
    internal class D2DGraphics : IGraphics, ISupportBeginAndEndDraw
    {
        private Control _control;
        private bool disposedValue;
        private D2DLayer? _d2dLayer;

        private WeakCache<Pen, ID2D1SolidColorBrush> _strokeColorCache;
        private WeakCache<Brush, ID2D1SolidColorBrush> _fillColorCache;
        private const int MaxBrushesCacheSize = 10;

        public D2DGraphics(Control control)
        {
            _control = control;
            _control.HandleCreated += Control_HandleCreated;
            _control.Resize += Control_Resize;
            _control.HandleDestroyed += Control_HandleDestroyed;
            _control.Disposed += Control_Disposed;

            _strokeColorCache = new WeakCache<Pen, ID2D1SolidColorBrush>(MaxBrushesCacheSize);
            _fillColorCache = new WeakCache<Brush, ID2D1SolidColorBrush>(MaxBrushesCacheSize);
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
        {
            _d2dLayer?.EndDraw();
        }

        public Region Clip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RectangleF ClipBounds => throw new NotImplementedException();

        public void Clear(Color color)
        {
            throw new NotImplementedException();
        }

        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            throw new NotImplementedException();
        }

        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void DrawString(string? s, Font font, Brush brush, float x, float y)
        {
            throw new NotImplementedException();
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
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
