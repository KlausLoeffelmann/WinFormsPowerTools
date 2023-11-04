namespace WinForms.PowerTools.Controls
{
    /// <summary>
    ///  Represents a menu item with a symbol.
    /// </summary>
    public class ToolStripSymbolMenuItem : ToolStripMenuItem
    {
        /// <summary>
        ///  Occurs when the <see cref="Symbol"/> property value changes.
        /// </summary>
        public event EventHandler? SymbolChanged;

        /// <summary>
        ///  Occurs when the <see cref="SymbolColor"/> property value changes.
        /// </summary>
        public event EventHandler? SymbolColorChanged;

        /// <summary>
        ///  Occurs when the <see cref="SymbolSize"/> property value changes.
        /// </summary>
        public event EventHandler? SymbolSizeChanged;

        private SymbolImageFactory? _symbolImageFactory;

        /// <summary>
        ///  Gets or sets the symbol character.
        /// </summary>
        public char? Symbol
        {
            get => _symbolImageFactory?.SymbolChar;
            set
            {
                if (_symbolImageFactory?.SymbolChar == value)
                {
                    return;
                }

                if (!value.HasValue)
                {
                    _symbolImageFactory = null;
                    OnSymbolChanged(EventArgs.Empty);
                    return;
                }
            }
        }

        /// <summary>
        ///  Gets or sets the color of the symbol.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///  Thrown when <see cref="Symbol"/> is <see langword="null"/>.
        /// </exception>
        public Color SymbolColor
        {
            get => _symbolImageFactory?.SymbolColor ?? ForeColor;

            set
            {
                if (_symbolImageFactory is null && value == ForeColor)
                {
                    return;
                }

                if (_symbolImageFactory is null)
                {
                    throw new InvalidOperationException("SymbolColor cannot be set when Symbol is not set.");
                }

                _symbolImageFactory = new SymbolImageFactory(
                    _symbolImageFactory.SymbolChar,
                    _symbolImageFactory.Width,
                    _symbolImageFactory.Height,
                    _symbolImageFactory.TransparentColor,
                    value,
                    _symbolImageFactory.LeftOffset,
                    _symbolImageFactory.TopOffset);

                OnSymbolColorChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        ///  Raises the <see cref="SymbolColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        protected virtual void OnSymbolColorChanged(EventArgs e)
            => SymbolColorChanged?.Invoke(this, e);

        /// <summary>
        ///  Gets or sets the size of the symbol.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///  Thrown when <see cref="Symbol"/> is <see langword="null"/>.
        /// </exception>
        public Size? SymbolSize
        {
            get
            {
                if (_symbolImageFactory is null)
                {
                    return null;
                }

                return new Size(_symbolImageFactory.Width, _symbolImageFactory.Height);
            }

            set
            {
                if (_symbolImageFactory is null && value is null
                    || _symbolImageFactory?.Width == value?.Width
                    && _symbolImageFactory?.Height == value?.Height)
                {
                    return;
                }

                if (_symbolImageFactory is null)
                {
                    throw new InvalidOperationException("SymbolSize cannot be set when Symbol is not set.");
                }

                if (value is null)
                {
                    _symbolImageFactory = null;
                    OnSymbolChanged(EventArgs.Empty);
                    OnSymbolSizeChanged(EventArgs.Empty);
                    return;
                }

                _symbolImageFactory = new SymbolImageFactory(
                    _symbolImageFactory.SymbolChar,
                    value.Value.Width,
                    value.Value.Height,
                    _symbolImageFactory.TransparentColor,
                    _symbolImageFactory.SymbolColor,
                    _symbolImageFactory.LeftOffset,
                    _symbolImageFactory.TopOffset);

                OnSymbolSizeChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        ///  Raises the <see cref="SymbolSizeChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        protected virtual void OnSymbolSizeChanged(EventArgs e)
            => SymbolSizeChanged?.Invoke(this, e);

        /// <summary>
        ///  Gets or sets the image displayed by the item.
        /// </summary>
        /// <returns>
        ///  The <see cref="Image"/> displayed by the item.
        /// </returns>
        public override Image Image
        {
            get
            {
                if (_symbolImageFactory is null)
                {
                    return base.Image;
                }

                return _symbolImageFactory.SymbolImage;
            }

            set => base.Image = value;
        }

        /// <summary>
        ///  Raises the <see cref="SymbolChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> containing the event data.</param>
        protected virtual void OnSymbolChanged(EventArgs e)
            => SymbolChanged?.Invoke(this, e);
    }
}
