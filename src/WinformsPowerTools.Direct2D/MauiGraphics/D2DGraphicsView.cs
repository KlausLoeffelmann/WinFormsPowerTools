using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Maui.Graphics.D2D.WinForms
{
    public class D2DGraphicsView : Control
    {
        private IDrawable? _drawable;
        private D2DCanvas _canvas;
        private bool _isDesignModeOrUnknown = true;

        private readonly System.Drawing.Color _defaultBackColor = System.Drawing.Color.Black;

        public D2DGraphicsView()
        {
            ResizeRedraw = true;
            ResetBackColor();

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

        public override System.Drawing.Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                Invalidate();
            }
        }

        public bool ShouldSerializeBackColor()
        {
            return BackColor != _defaultBackColor;
        }

        public override void ResetBackColor()
        {
            BackColor = _defaultBackColor;
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _canvas.Resize(ClientSize);
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
            _canvas.Clear(BackColor);

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
