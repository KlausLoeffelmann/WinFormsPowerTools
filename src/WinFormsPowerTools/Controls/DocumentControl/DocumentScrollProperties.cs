// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using System.ComponentModel;
using Windows.Win32;
using Windows.Win32.UI.WindowsAndMessaging;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Controls;

namespace System.Windows.Forms.Documents
{
    /// <summary>
    ///  Basic Properties for Scrollbars.
    /// </summary>
    public abstract class DocumentScrollProperties
    {
        internal int _minimum;
        internal int _maximum = 100;
        internal int _smallChange = 1;
        internal int _largeChange = 10;
        internal int _value;
        internal bool _maximumSetExternally;
        internal bool _smallChangeSetExternally;
        internal bool _largeChangeSetExternally;

        private readonly DocumentControl? _parent;

        protected DocumentControl? ParentControl => _parent;

        internal bool _visible;
        private bool _enabled = true;

        protected DocumentScrollProperties(DocumentControl? container)
        {
            _parent = container;
        }

        /// <summary>
        ///  Gets or sets a bool value controlling whether the scrollbar is enabled.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_parent is not null)
                {
                    return;
                }

                if (value != _enabled)
                {
                    _enabled = value;
                    if (_parent is not null)
                    {
                        PInvoke.EnableScrollBar(
                            new HWND(_parent.Handle),
                            (uint) Orientation,
                            value
                                ? ENABLE_SCROLL_BAR_ARROWS.ESB_ENABLE_BOTH
                                : ENABLE_SCROLL_BAR_ARROWS.ESB_DISABLE_BOTH);
                    }
                }
            }
        }

        /// <summary>
        ///  Gets or sets a value to be added or subtracted to the <see cref='LargeChange'/>
        ///  property when the scroll box is moved a large distance.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(10)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int LargeChange
        {
            get
            {
                // We preserve the actual large change value that has been set, but when we come to
                // get the value of this property, make sure it's within the maximum allowable value.
                // This way we ensure that we don't depend on the order of property sets when
                // code is generated at design-time.
                return Math.Min(_largeChange, _maximum - _minimum + 1);
            }
            set
            {
                if (_largeChange != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(value), 
                            string.Format("Value for large change is too small.", 
                            nameof(LargeChange), 
                            value, 
                            0));
                    }

                    _largeChange = value;
                    _largeChangeSetExternally = true;
                    UpdateScrollInfo();
                }
            }
        }

        /// <summary>
        ///  Gets or sets the upper limit of values of the scrollable range.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(100)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (_parent is not null)
                {
                    return;
                }

                if (_maximum != value)
                {
                    if (_minimum > value)
                    {
                        _minimum = value;
                    }

                    if (value < _value)
                    {
                        Value = value;
                    }

                    _maximum = value;
                    _maximumSetExternally = true;
                    UpdateScrollInfo();
                }
            }
        }

        /// <summary>
        ///  Gets or sets the lower limit of values of the scrollable range.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int Minimum
        {
            get => _minimum;
            set
            {
                if (_parent is not null)
                {
                    return;
                }

                if (_minimum != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(value), 
                            string.Format("Value for Minimum is too small.", 
                            nameof(Minimum), 
                            value, 
                            0));
                    }

                    if (_maximum < value)
                    {
                        _maximum = value;
                    }

                    if (value > _value)
                    {
                        _value = value;
                    }

                    _minimum = value;
                    UpdateScrollInfo();
                }
            }
        }

        private protected abstract int GetPageSize(DocumentControl parent);

        private protected abstract SCROLLBAR_CONSTANTS Orientation { get; }

        private protected abstract int GetHorizontalDisplayPosition(DocumentControl parent);

        private protected abstract int GetVerticalDisplayPosition(DocumentControl parent);

        /// <summary>
        ///  Gets or sets the value to be added or subtracted to the <see cref='ScrollBar.Value'/>
        ///  property when the scroll box is moved a small distance.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(1)]
        public int SmallChange
        {
            get
            {
                // We can't have SmallChange > LargeChange, but we shouldn't manipulate
                // the set values for these properties, so we just return the smaller
                // value here.
                return Math.Min(_smallChange, LargeChange);
            }
            set
            {
                if (_smallChange != value)
                {
                    if (value < 0)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(value), 
                            string.Format("Value for SmallChange is too small.", 
                            nameof(SmallChange), 
                            value, 
                            0));
                    }

                    _smallChange = value;
                    _smallChangeSetExternally = true;
                    UpdateScrollInfo();
                }
            }
        }

        /// <summary>
        ///  Gets or sets a numeric value that represents the current position of the scroll box
        ///  on the scroll bar control.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(0)]
        [Bindable(true)]
        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    if (value < _minimum || value > _maximum)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(value),
                            string.Format("Value can't be < Minimum or > Maximum.",
                            nameof(Value),
                            value,
                            $"'{nameof(Minimum)}'",
                            $"'{nameof(Maximum)}'"));
                    }

                    _value = value;
                    UpdateScrollInfo();
                    UpdateDisplayPosition();
                }
            }
        }

        /// <summary>
        ///  Gets or sets a bool value controlling whether the scrollbar is showing.
        /// </summary>
        [Category("Behavior")]
        [DefaultValue(false)]
        public bool Visible
        {
            get => _visible;
            set
            {
                if (_parent is not null)
                {
                    return;
                }

                if (value != _visible)
                {
                    _visible = value;
                    //_parent?.UpdateStylesCore();
                    UpdateScrollInfo();
                    UpdateDisplayPosition();
                }
            }
        }

        internal unsafe void UpdateScrollInfo()
        {
            if (_parent is not null && _parent.IsHandleCreated && _visible)
            {
                SCROLLINFO si = new()
                {
                    cbSize = (uint)sizeof(SCROLLINFO),
                    fMask = SCROLLINFO_MASK.SIF_ALL,
                    nMin = _minimum,
                    nMax = _maximum,
                    nPage = (uint)GetPageSize(_parent),
                    nPos = _value,
                    nTrackPos = 0
                };

                PInvoke.SetScrollInfo(new HWND(_parent.Handle), Orientation, si, new BOOL(true));
            }
        }

        private void UpdateDisplayPosition()
        {
            if (_parent is null)
            {
                return;
            }

            int horizontal = GetHorizontalDisplayPosition(_parent);
            int vertical = GetVerticalDisplayPosition(_parent);
            _parent.SetDisplayFromScrollProps(horizontal, vertical);
        }
    }
}
