using System;
using System.Windows.Forms;

namespace Microsoft.Maui.Graphics.D2D.WinForms
{
    public class D2DGraphicsView : Control
    {
        private IDrawable? _drawable;
        private D2DCanvas _canvas;
        private bool _isDesignModeOrUnknown = true;

        public D2DGraphicsView()
        {
            ResizeRedraw = true;

            _canvas = new D2DCanvas() { Window = this };
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _canvas.Window = this;

            if (!IsAncestorSiteInDesignMode)
            {
                _isDesignModeOrUnknown = false;
                if (Drawable is not null)
                {
                    Invalidate();
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            _isDesignModeOrUnknown = true;
        }

        public ICanvas Canvas => _canvas;

        public IDrawable? Drawable
        {
            get => _drawable;
            set
            {
                if (!Equals(_drawable,value))
                {
                    _drawable = value;
                    //OnDrawableChanged();
                }

                if (_drawable is not null && IsHandleCreated)
                {
                    Invalidate();
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // We're blocking background-painting by not
            // calling the base method, if we're not in Design Mode.
            if (_isDesignModeOrUnknown)
            {
                base.OnPaintBackground(pevent);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_isDesignModeOrUnknown)
            {
                return;
            }

            _canvas.BeginDraw();

            if (Drawable is not null)
            {
                Drawable.Draw(Canvas, new RectangleF(
                    ClientRectangle.X,
                    ClientRectangle.Y,
                    ClientRectangle.Width,
                    ClientRectangle.Height));
            }

            _canvas.EndDraw();
        }
    }
}
