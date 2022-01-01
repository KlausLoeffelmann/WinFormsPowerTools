using SkiaSharp;
using System.Drawing;

namespace WinFormsPowerToolsDemo.SkiaSharpDemo
{
    internal class MovingCircleShape : Shape
    {
        private const float MarginX = 2;
        private const float MarginY = 2;

        private Color _lineColor;
        private Color _fillColor;
        private float _radius;
        private float _xSpeed;
        private float _ySpeed;
        private PointF _currentLocation;
        private float _penWidth;

        private Pen _drawingPen;
        private Brush _fillingBrush;

        private SKPaint _drawingPaint;
        private SKPaint _fillingPaint;
        private SKPath _circlePath;
        private SKPath _circlePathClone;

        public MovingCircleShape(
            Color lineColor, 
            Color fillColor, 
            float penWidth, 
            float radius, 
            float xSpeed, 
            float ySpeed, 
            PointF startLocation)
        {
            _lineColor = lineColor;
            _fillColor = fillColor;
            _radius = radius;
            _xSpeed = xSpeed;
            _ySpeed = ySpeed;
            _currentLocation = startLocation;
            _penWidth = penWidth;
        }

        public override void OnGdiplusRender(Graphics g)
        {
            _drawingPen ??= new Pen(_lineColor, _penWidth);
            _fillingBrush ??= new SolidBrush(_fillColor);
            g.FillEllipse(_fillingBrush, _currentLocation.X - _radius, _currentLocation.Y - _radius, _radius * 2, _radius * 2);
            g.DrawEllipse(_drawingPen, _currentLocation.X - _radius, _currentLocation.Y - _radius, _radius * 2, _radius * 2);
        }

        public override void OnSkiaRender(SKSurface surface)
        {
            if (_fillingPaint is null)
            {
                _fillingPaint = new();
                _drawingPaint = new();

                _fillingPaint.Style = SKPaintStyle.Fill;
                _fillingPaint.Color = new SKColor(_fillColor.R, _fillColor.G, _fillColor.B, _fillColor.A);

                _drawingPaint.Style = SKPaintStyle.Stroke;
                _drawingPaint.Color = new SKColor(_lineColor.R, _lineColor.G, _lineColor.B, _fillColor.A);
                _drawingPaint.StrokeWidth = _penWidth;
                _drawingPaint.FilterQuality = SKFilterQuality.Low;

                _circlePath=new SKPath();
                _circlePathClone = new SKPath();
                _circlePath.AddCircle(0, 0, _radius);
                _circlePath.Simplify();
            }

            surface.Canvas.DrawCircle(_currentLocation.X, _currentLocation.Y, _radius, _fillingPaint);

            //_circlePathClone.Reset();
            _circlePath.Transform(SKMatrix.CreateTranslation(_currentLocation.X, _currentLocation.Y));
            surface.Canvas.DrawPath(_circlePath, _drawingPaint);
            _circlePath.Transform(SKMatrix.CreateTranslation(-_currentLocation.X, -_currentLocation.Y));

            //surface.Canvas.DrawCircle(_currentLocation.X, _currentLocation.Y, _radius, _drawingPaint);

            //surface.Canvas.DrawRect(_currentLocation.X - _radius, _currentLocation.Y - _radius, _radius * 2, _radius * 2, _drawingPaint);
            //for (int x = 0; x < _radius * 2; x += 10)
            //{
            //    surface.Canvas.DrawLine(
            //        _currentLocation.X - _radius,
            //        _currentLocation.Y - _radius,
            //        _currentLocation.X - _radius + x,
            //        _currentLocation.Y + _radius,
            //        _drawingPaint);
            //}
        }

        public override void Trigger()
        {
            _currentLocation.X += _xSpeed;
            _currentLocation.Y += _ySpeed;
            CheckBounderies();
        }

        private void CheckBounderies()
        {
            if ((_xSpeed > 0 && _currentLocation.X + _radius + MarginX > CanvasSize.Width) ||
                (_xSpeed < 0 && _currentLocation.X - _radius- MarginX < 0))
            {
                _xSpeed *= -1;
            }

            if ((_ySpeed > 0 && _currentLocation.Y + _radius + MarginY > CanvasSize.Height) ||
                (_ySpeed < 0 && _currentLocation.Y - _radius - MarginY < 0))
            {
                _ySpeed *= -1;
            }
        }
    }
}
