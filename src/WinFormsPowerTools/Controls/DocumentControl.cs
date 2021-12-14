#nullable disable

using System.ComponentModel;
using System.Drawing;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace System.Windows.Forms.Documents
{
    public partial class DocumentControl : Control
    {
        private int _scrollState;
        private Document _document;

        private VDocumentScrollProperties _verticalScroll;
        private HDocumentScrollProperties _horizontalScroll;

        /// <summary>
        ///  Current size of the displayRect.
        /// </summary>
        private Rectangle _displayRect = Rectangle.Empty;

        /// <summary>
        ///  Used to figure out what the horizontal scroll value should be set to when the horizontal
        ///  scrollbar is first shown.
        /// </summary>
        private bool resetRTLHScrollValue;

        protected const int ScrollStateAutoScrolling = 0x0001;      // We no longer use this. What was AutoScroll is default here.
        protected const int ScrollStateHScrollVisible = 0x0002;
        protected const int ScrollStateVScrollVisible = 0x0004;
        protected const int ScrollStateUserHasScrolled = 0x0008;
        protected const int ScrollStateFullDrag = 0x0010;

        public event ScrollEventHandler ScrollEvent;

        /// <summary>
        ///  Initializes a new instance of the <see cref='ScrollableControl'/> class.
        /// </summary>
        public DocumentControl() : base()
        {
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, false);
            Document = new Document();
        }

        public Document Document
        {
            get => _document;
            set
            {
                _document = value;
                _displayRect = new Rectangle(0, 0, (int)_document.Width, (int)_document.Height);
                PerformLayout();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            SetDisplayRectLocation(0, 0);
            ApplyScrollbarChanges(_displayRect);
        }

        /// <summary>
        ///  Gets the Vertical Scroll bar for this ScrollableControl.
        /// </summary>
        [Category("Layout")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public VDocumentScrollProperties VerticalScroll
            => _verticalScroll ??= new VDocumentScrollProperties(this);

        /// <summary>
        /// Gets the Horizontal Scroll bar for this ScrollableControl.
        /// </summary>
        [Category("Layout")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public HDocumentScrollProperties HorizontalScroll
            => _horizontalScroll ??= new HDocumentScrollProperties(this);

        /// <summary>
        ///  Gets or sets a value indicating whether the horizontal scroll bar is visible.
        /// </summary>
        protected bool HScroll
        {
            get => GetScrollState(ScrollStateHScrollVisible);
            set => SetScrollState(ScrollStateHScrollVisible, value);
        }

        /// <summary>
        ///  Gets or sets a value indicating whether the vertical scroll bar is visible.
        /// </summary>
        protected bool VScroll
        {
            get => GetScrollState(ScrollStateVScrollVisible);
            set => SetScrollState(ScrollStateVScrollVisible, value);
        }

        /// <summary>
        ///  Tests a given scroll state bit to determine if it is set.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected bool GetScrollState(int bit) => (bit & _scrollState) == bit;

        /// <summary>
        ///  Actually displays or hides the horiz and vert autoscrollbars. This will
        ///  also adjust the values of formState to reflect the new state
        /// </summary>
        private bool SetVisibleScrollbars(bool horiz, bool vert)
        {
            bool needLayout = false;

            if ((!horiz && HScroll)
                || (horiz && !HScroll)
                || (!vert && VScroll)
                || (vert && !VScroll))
            {
                needLayout = true;
            }

            // If we are about to show the horizontal scrollbar, then
            // set this flag, so that we can set the right initial value
            // based on whether we are right to left.
            if (horiz && !HScroll && (RightToLeft == RightToLeft.Yes))
            {
                resetRTLHScrollValue = true;
            }

            if (needLayout)
            {
                int x = _displayRect.X;
                int y = _displayRect.Y;
                if (!horiz)
                {
                    x = 0;
                }

                if (!vert)
                {
                    y = 0;
                }

                SetDisplayRectLocation(x, y);
                SetScrollState(ScrollStateUserHasScrolled, false);
                HScroll = horiz;
                VScroll = vert;

                // Update the visible member of ScrollBars.
                if (horiz)
                {
                    HorizontalScroll._visible = true;
                }
                else
                {
                    ResetScrollProperties(HorizontalScroll);
                }

                if (vert)
                {
                    VerticalScroll._visible = true;
                }
                else
                {
                    ResetScrollProperties(VerticalScroll);
                }

                UpdateStyles();
            }

            return needLayout;
        }

        /// <summary>
        ///  Sets the width and height of the virtual client area used in autoscrolling.
        ///  This will also adjust the x and y location of the virtual client area if the
        ///  new size forces it.
        /// </summary>
        private bool SetDisplayRectangleSize(int width, int height)
        {
            bool needLayout = false;
            if (_displayRect.Width != width || _displayRect.Height != height)
            {
                _displayRect.Width = width;
                _displayRect.Height = height;
                needLayout = true;
            }

            int minX = ClientRectangle.Width - width;
            int minY = ClientRectangle.Height - height;
            if (minX > 0)
            {
                minX = 0;
            }

            if (minY > 0)
            {
                minY = 0;
            }

            int x = _displayRect.X;
            int y = _displayRect.Y;

            if (!HScroll)
            {
                x = 0;
            }

            if (!VScroll)
            {
                y = 0;
            }

            if (x < minX)
            {
                x = minX;
            }

            if (y < minY)
            {
                y = minY;
            }

            SetDisplayRectLocation(x, y);
            return needLayout;
        }

        /// <summary>
        ///  Sets a given scroll state bit.
        /// </summary>
        protected void SetScrollState(int bit, bool value)
        {
            if (value)
            {
                _scrollState |= bit;
            }
            else
            {
                _scrollState &= (~bit);
            }
        }

        /// <summary>
        ///  Retrieves the CreateParams used to create the window.
        ///  If a subclass overrides this function, it must call the base implementation.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                if (HScroll)
                {
                    cp.Style |= (int)WINDOW_STYLE.WS_HSCROLL;
                }
                else
                {
                    cp.Style &= ~(int)WINDOW_STYLE.WS_HSCROLL;
                }

                if (VScroll)
                {
                    cp.Style |= (int)WINDOW_STYLE.WS_VSCROLL;
                }
                else
                {
                    cp.Style &= ~(int)WINDOW_STYLE.WS_VSCROLL;
                }

                return cp;
            }
        }

        private unsafe int ScrollThumbPosition(SCROLLBAR_CONSTANTS fnBar)
        {
            SCROLLINFO si = new()
            {
                cbSize = (uint)sizeof(SCROLLINFO),
                fMask = SCROLLINFO_MASK.SIF_TRACKPOS
            };

            HWND handle = new(this.Handle);
            PInvoke.GetScrollInfo(handle, fnBar, ref si);
            return si.nTrackPos;
        }

        /// <summary>
        ///  Updates the value of the autoscroll scrollbars based on the current form
        ///  state. This is a one-way sync, updating the scrollbars only.
        /// </summary>
        private void SyncScrollbars()
        {
            Rectangle displayRect = this._displayRect;

            if (!IsHandleCreated)
            {
                return;
            }

            if (HScroll)
            {
                if (!HorizontalScroll._maximumSetExternally)
                {
                    HorizontalScroll._maximum = displayRect.Width - 1;
                }

                if (!HorizontalScroll._largeChangeSetExternally)
                {
                    HorizontalScroll._largeChange = ClientRectangle.Width;
                }

                if (!HorizontalScroll._smallChangeSetExternally)
                {
                    HorizontalScroll._smallChange = 5;
                }

                if (resetRTLHScrollValue && !IsMirrored)
                {
                    resetRTLHScrollValue = false;
                    //BeginInvoke(new EventHandler(OnSetScrollPosition));
                }
                else if (-displayRect.X >= HorizontalScroll._minimum && -displayRect.X < HorizontalScroll._maximum)
                {
                    HorizontalScroll._value = -displayRect.X;
                }

                HorizontalScroll.UpdateScrollInfo();
            }

            if (VScroll)
            {
                if (!VerticalScroll._maximumSetExternally)
                {
                    VerticalScroll._maximum = displayRect.Height - 1;
                }

                if (!VerticalScroll._largeChangeSetExternally)
                {
                    VerticalScroll._largeChange = ClientRectangle.Height;
                }

                if (!VerticalScroll._smallChangeSetExternally)
                {
                    VerticalScroll._smallChange = 5;
                }

                if (-displayRect.Y >= VerticalScroll._minimum && -displayRect.Y < VerticalScroll._maximum)
                {
                    VerticalScroll._value = -displayRect.Y;
                }

                VerticalScroll.UpdateScrollInfo();
            }
        }

        private void ResetScrollProperties(DocumentScrollProperties scrollProperties)
        {
            // Set only these two values as when the ScrollBars are not visible ...
            // there is no meaning of the "value" property.
            scrollProperties._visible = false;
            scrollProperties._value = 0;
        }

        /// <summary>
        ///  WM_VSCROLL handler
        /// </summary>
        private void WmVScroll(ref Message m)
        {
            // The lparam is handle of the sending scrollbar, or NULL when
            // the scrollbar sending the message is the "form" scrollbar.
            if ((nint)m.LParam != 0)
            {
                base.WndProc(ref m);
                return;
            }

            Rectangle client = ClientRectangle;
            var loWord = Interop.PARAM.LOWORD(m.WParam);
            bool thumbTrack = loWord != PInvoke.SB_THUMBTRACK;

            int pos = 0-_displayRect.Y;
            int oldValue = pos;
            int maxPos = -(client.Height - _displayRect.Height);

            switch ((uint)loWord)
            {
                case PInvoke.SB_THUMBPOSITION:
                case PInvoke.SB_THUMBTRACK:
                    pos = ScrollThumbPosition(SCROLLBAR_CONSTANTS.SB_VERT);
                    break;

                case PInvoke.SB_LINEUP:
                    if (pos > 0)
                    {
                        pos -= VerticalScroll.SmallChange;
                    }
                    else
                    {
                        pos = 0;
                    }

                    break;

                case PInvoke.SB_LINEDOWN:
                    if (pos < maxPos - VerticalScroll.SmallChange)
                    {
                        pos += VerticalScroll.SmallChange;
                    }
                    else
                    {
                        pos = maxPos;
                    }

                    break;

                case PInvoke.SB_PAGEUP:
                    if (pos > VerticalScroll.LargeChange)
                    {
                        pos -= VerticalScroll.LargeChange;
                    }
                    else
                    {
                        pos = 0;
                    }

                    break;

                case PInvoke.SB_PAGEDOWN:
                    if (pos < maxPos - VerticalScroll.LargeChange)
                    {
                        pos += VerticalScroll.LargeChange;
                    }
                    else
                    {
                        pos = maxPos;
                    }

                    break;

                case PInvoke.SB_TOP:
                    pos = 0;
                    break;

                case PInvoke.SB_BOTTOM:
                    pos = maxPos;
                    break;
            }

            // If SystemInformation.DragFullWindows set is to false the usage should be
            // identical to WnHScroll which follows.
            if (GetScrollState(ScrollStateFullDrag) || thumbTrack)
            {
                SetScrollState(ScrollStateUserHasScrolled, true);
                SetDisplayRectLocation(_displayRect.X, -pos);
                SyncScrollbars();
            }

            WmOnScroll(ref m, oldValue, pos, ScrollOrientation.VerticalScroll);
        }

        /// <summary>
        ///  This function gets called which populates the eventArgs and fires the OnScroll( ) event passing
        ///  the appropriate scroll event and scroll bar.
        /// </summary>
        private void WmOnScroll(ref Message m, int oldValue, int value, ScrollOrientation scrollOrientation)
        {
            ScrollEventType type = (ScrollEventType)Interop.PARAM.LOWORD(m.WParam);

            if (type != ScrollEventType.EndScroll)
            {
                ScrollEventArgs se = new ScrollEventArgs(type, oldValue, value, scrollOrientation);
                OnScroll(se);
            }
        }

        /// <summary>
        ///  Raises the <see cref='System.Windows.Forms.ScrollBar.OnScroll'/> event.
        /// </summary>
        protected virtual void OnScroll(ScrollEventArgs scrollEventArgs) 
            => ScrollEvent?.Invoke(this, scrollEventArgs);

        /// <summary>
        ///  Allows to set the <see cref="DisplayRectangle" /> to enable the visual scroll effect.
        /// </summary>
        internal void SetDisplayFromScrollProps(int x, int y)
        {
            Rectangle display = GetDisplayRectInternal();
            ApplyScrollbarChanges(display);
            SetDisplayRectLocation(x, y);
        }

        private Rectangle GetDisplayRectInternal()
        {
            if (_displayRect.IsEmpty)
            {
                _displayRect = ClientRectangle;
            }

            return _displayRect;
        }

        /// <summary>
        ///  Adjusts the displayRect to be at the offset x, y. The contents of the
        ///  Form is scrolled using Windows.ScrollWindowEx.
        /// </summary>
        protected unsafe void SetDisplayRectLocation(int x, int y)
        {
            int xDelta = 0;
            int yDelta = 0;

            Rectangle client = ClientRectangle;
            // The DisplayRect property modifies
            // the returned rect to include padding. We don't want to
            // include this padding in our adjustment of the DisplayRect
            // because it interferes with the scrolling.
            Rectangle displayRectangle = _displayRect;
            int minX = Math.Min(client.Width - displayRectangle.Width, 0);
            int minY = Math.Min(client.Height - displayRectangle.Height, 0);

            if (x > 0)
            {
                x = 0;
            }

            if (y > 0)
            {
                y = 0;
            }

            if (x < minX)
            {
                x = minX;
            }

            if (y < minY)
            {
                y = minY;
            }

            if (displayRectangle.X != x)
            {
                xDelta = x - displayRectangle.X;
            }

            if (displayRectangle.Y != y)
            {
                yDelta = y - displayRectangle.Y;
            }

            _displayRect.X = x;
            _displayRect.Y = y;

            if (IsHandleCreated && (xDelta != 0 || yDelta != 0))
            {
                RECT rcClip = new RECT()
                {
                    left = ClientRectangle.X,
                    top = ClientRectangle.Y,
                    right = ClientRectangle.Width + ClientRectangle.X,
                    bottom = ClientRectangle.Height + ClientRectangle.Y
                };

                RECT rcUpdate = rcClip;

                HRGN zeroHRGN = new HRGN(IntPtr.Zero);

                PInvoke.ScrollWindowEx(
                    new HWND(Handle),
                    xDelta,
                    yDelta,
                    prcScroll: null,
                    prcClip: &rcClip,
                    zeroHRGN,
                    prcUpdate: &rcUpdate,
                    SHOW_WINDOW_CMD.SW_INVALIDATE |
                        SHOW_WINDOW_CMD.SW_ERASE |
                        SHOW_WINDOW_CMD.SW_SCROLLCHILDREN);
            }
        }

        /// <summary>
        ///  WM_HSCROLL handler
        /// </summary>
        private void WmHScroll(ref Message m)
        {
            // The lparam is handle of the sending scrollbar, or NULL when
            // the scrollbar sending the message is the "form" scrollbar.
            if ((nint) m.LParam != 0)
            {
                base.WndProc(ref m);
                return;
            }

            Rectangle client = ClientRectangle;

            int pos = -_displayRect.X;
            int oldValue = pos;
            int maxPos = -(client.Width - _displayRect.Width);

            var loWord = Interop.PARAM.LOWORD(m.WParam);
            switch ((uint)loWord)
            {
                case PInvoke.SB_THUMBPOSITION:
                case PInvoke.SB_THUMBTRACK:
                    pos = ScrollThumbPosition(SCROLLBAR_CONSTANTS.SB_HORZ);

                    break;
                case PInvoke.SB_LINELEFT:
                    if (pos > HorizontalScroll.SmallChange)
                    {
                        pos -= HorizontalScroll.SmallChange;
                    }
                    else
                    {
                        pos = 0;
                    }

                    break;
                case PInvoke.SB_LINERIGHT:
                    if (pos < maxPos - HorizontalScroll.SmallChange)
                    {
                        pos += HorizontalScroll.SmallChange;
                    }
                    else
                    {
                        pos = maxPos;
                    }

                    break;
                case PInvoke.SB_PAGELEFT:
                    if (pos > HorizontalScroll.LargeChange)
                    {
                        pos -= HorizontalScroll.LargeChange;
                    }
                    else
                    {
                        pos = 0;
                    }

                    break;
                case PInvoke.SB_PAGERIGHT:
                    if (pos < maxPos - HorizontalScroll.LargeChange)
                    {
                        pos += HorizontalScroll.LargeChange;
                    }
                    else
                    {
                        pos = maxPos;
                    }

                    break;
                case PInvoke.SB_LEFT:
                    pos = 0;
                    break;
                case PInvoke.SB_RIGHT:
                    pos = maxPos;
                    break;
            }

            if (GetScrollState(ScrollStateFullDrag) || loWord != PInvoke.SB_THUMBTRACK)
            {
                SetScrollState(ScrollStateUserHasScrolled, true);
                SetDisplayRectLocation(-pos, _displayRect.Y);
                SyncScrollbars();
            }

            WmOnScroll(ref m, oldValue, pos, ScrollOrientation.HorizontalScroll);
        }

        /// <summary>
        ///  Queries the system to determine the users preference for full drag
        ///  of windows.
        /// </summary>
        private void UpdateFullDrag()
        {
            SetScrollState(ScrollStateFullDrag, SystemInformation.DragFullWindows);
        }

        private void WmSettingChange(ref Message m)
        {
            base.WndProc(ref m);
            UpdateFullDrag();
        }

        /// <summary>
        ///  The button's window procedure. Inheriting classes can override this
        ///  to add extra functionality, but should not forget to call
        ///  base.wndProc(m); to ensure the button continues to function properly.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override void WndProc(ref Message m)
        {
            switch ((uint) m.Msg)
            {
                case PInvoke.WM_VSCROLL:
                    WmVScroll(ref m);
                    break;
                case PInvoke.WM_HSCROLL:
                    WmHScroll(ref m);
                    break;
                
                // Same as WM_SETTINGCHANGE
                case PInvoke.WM_WININICHANGE:
                    WmSettingChange(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private bool ApplyScrollbarChanges(Rectangle display)
        {
            bool needLayout = false;
            bool needHscroll = false;
            bool needVscroll = false;

            Rectangle currentClient = ClientRectangle;
            Rectangle fullClient = currentClient;
            Rectangle minClient = fullClient;

            if (HScroll)
            {
                fullClient.Height += SystemInformation.HorizontalScrollBarHeight;
            }
            else
            {
                minClient.Height -= SystemInformation.HorizontalScrollBarHeight;
            }

            if (VScroll)
            {
                fullClient.Width += SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                minClient.Width -= SystemInformation.VerticalScrollBarWidth;
            }

            int maxX = minClient.Width;
            int maxY = minClient.Height;

            if (Document is not null)
            {
                maxX = (int) Document.Width;
                maxY = (int) Document.Height;
                needHscroll = true;
                needVscroll = true;
            }

            // Check maxX/maxY against the clientRect, we must compare it to the
            // clientRect without any scrollbars, and then we can check it against
            // the clientRect with the "new" scrollbars. This will make the
            // scrollbars show and hide themselves correctly at the boundaries.
            //
            if (maxX <= fullClient.Width)
            {
                needHscroll = false;
            }
            if (maxY <= fullClient.Height)
            {
                needVscroll = false;
            }
            Rectangle clientToBe = fullClient;

            if (needHscroll)
            {
                clientToBe.Height -= SystemInformation.HorizontalScrollBarHeight;
            }

            if (needVscroll)
            {
                clientToBe.Width -= SystemInformation.VerticalScrollBarWidth;
            }

            if (needHscroll && maxY > clientToBe.Height)
            {
                needVscroll = true;
            }

            if (needVscroll && maxX > clientToBe.Width)
            {
                needHscroll = true;
            }

            if (!needHscroll)
            {
                maxX = clientToBe.Width;
            }

            if (!needVscroll)
            {
                maxY = clientToBe.Height;
            }

            // Show the needed scrollbars
            needLayout = (SetVisibleScrollbars(needHscroll, needVscroll) || needLayout);

            // If needed, adjust the size...
            if (HScroll || VScroll)
            {
                needLayout = (SetDisplayRectangleSize(maxX, maxY) || needLayout);
            }

            // Else just update the display rect size... this keeps it as big as the client
            // area in a resize scenario
            //
            else
            {
                SetDisplayRectangleSize(maxX, maxY);
            }

            // Sync up the scrollbars
            //
            SyncScrollbars();

            return needLayout;
        }
    }
}
