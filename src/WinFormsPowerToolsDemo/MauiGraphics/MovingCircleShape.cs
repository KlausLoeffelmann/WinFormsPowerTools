
using Microsoft.Maui.Graphics;

namespace WinFormsPowerToolsDemo.MauiGraphics
{
    internal class MovingCircleShape : DrawableShape
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

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.FillColor = _fillColor;
            canvas.StrokeColor = _lineColor;
            canvas.StrokeSize = _penWidth;

            canvas.FillEllipse(_currentLocation.X - _radius, _currentLocation.Y - _radius, _radius * 2, _radius * 2);
            canvas.DrawEllipse(_currentLocation.X - _radius, _currentLocation.Y - _radius, _radius * 2, _radius * 2);
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
