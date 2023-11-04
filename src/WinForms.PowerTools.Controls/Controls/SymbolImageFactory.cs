using System.Collections.Concurrent;
using System.Drawing.Text;

namespace WinForms.PowerTools.Controls
{
    /// <summary>
    ///  Represents a factory for creating symbol images using the Segoe MDL2 Assets font.
    /// </summary>public 
    public class SymbolImageFactory
    {
        private const string SegoeMDL2AssetsFont = "Segoe MDL2 Assets";
        private static readonly ConcurrentDictionary<string, bool> s_fontCache = new();

        /// <summary>
        ///  Initializes a new instance of the <see cref="SymbolImageFactory"/> class.
        /// </summary>
        /// <param name="symbolValue">Unicode value representation of the character of the Font.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="transparentColor">The transparent color for the image.</param>
        /// <param name="leftOffset">The left offset for the symbol.</param>
        /// <param name="topOffset">The top offset for the symbol.</param>
        /// <param name="getImageLazy">Indicates whether to create the image lazily.</param>
        public SymbolImageFactory(
            int symbolValue,
            int width = 32,
            int height = 32,
            Color color = default,
            Color transparentColor = default,
            int leftOffset = 0,
            int topOffset = 0,
        bool getImageLazy = true) : this(
            (char)symbolValue,
            width,
            height,
            color,
            transparentColor,
            leftOffset,
            topOffset,
            getImageLazy)
        { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="SymbolImageFactory"/> class.
        /// </summary>
        /// <param name="symbol">One of the <see cref="MDL2Assets"/> values.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="transparentColor">The transparent color for the image.</param>
        /// <param name="leftOffset">The left offset for the symbol.</param>
        /// <param name="topOffset">The top offset for the symbol.</param>
        /// <param name="getImageLazy">Indicates whether to create the image lazily.</param>
        public SymbolImageFactory(
            MDL2Assets symbol,
            int width = 32,
            int height = 32,
            Color color = default,
            Color transparentColor = default,
            int leftOffset = 0,
            int topOffset = 0,
        bool getImageLazy = true) : this(
            (char)symbol,
            width,
            height,
            color,
            transparentColor,
            leftOffset,
            topOffset,
            getImageLazy) { }

            /// <summary>
            ///  Initializes a new instance of the <see cref="SymbolImageFactory"/> class.
            /// </summary>
            /// <param name="symbolChar">The character representing the symbol.</param>
            /// <param name="width">The width of the image.</param>
            /// <param name="height">The height of the image.</param>
            /// <param name="transparentColor">The transparent color for the image.</param>
            /// <param name="leftOffset">The left offset for the symbol.</param>
            /// <param name="topOffset">The top offset for the symbol.</param>
            /// <param name="getImageLazy">Indicates whether to create the image lazily.</param>
            public SymbolImageFactory(
            char symbolChar,
            int width = 32,
            int height = 32,
            Color color = default,
            Color transparentColor = default,
            int leftOffset = 0,
            int topOffset = 0,
            bool getImageLazy = true)
        {
            SymbolChar = symbolChar;
            Width = width;
            Height = height;
            LeftOffset = leftOffset;
            TopOffset = topOffset;

            if (transparentColor == default)
            {
                transparentColor = Color.Transparent;
            }

            if (color == default)
            {
                color = Color.Black;
            }

            TransparentColor = transparentColor;
            SymbolFont = new Font(SegoeMDL2AssetsFont, height, FontStyle.Regular, GraphicsUnit.Pixel);

            if (getImageLazy)
            {
                SymbolImage = new Lazy<Bitmap>(GetImage(
                    SymbolChar, 
                    Width, 
                    Height, 
                    TransparentColor, 
                    LeftOffset, 
                    TopOffset)).Value;
            }
            else
            {
                SymbolImage = GetImage(
                    SymbolChar,
                    Width,
                    Height,
                    TransparentColor,
                    LeftOffset,
                    TopOffset);
            }
        }

        /// <summary>
        ///  Gets the character representing the symbol.
        /// </summary>
        public char SymbolChar { get; }

        /// <summary>
        ///  Gets the width of the image.
        /// </summary>
        public int Width { get; }

        /// <summary>
        ///  Gets the height of the image.
        /// </summary>
        public int Height { get; }

        /// <summary>
        ///  Gets the left offset for the symbol.
        /// </summary>
        public int LeftOffset { get; }

        /// <summary>
        ///  Gets the top offset for the symbol.
        /// </summary>
        public int TopOffset { get; }

        /// <summary>
        ///  Gets the transparent color for the image.
        /// </summary>
        public Color TransparentColor { get; }

        /// <summary>
        ///  Gets the font used for rendering the symbol.
        /// </summary>
        public Font SymbolFont { get; }

        /// <summary>
        ///  The image for the symbol.
        /// </summary>
        public Bitmap SymbolImage { get; }

        /// <summary>
        ///  The color for the symbol.
        /// </summary>
        public Color SymbolColor { get; }

        private static Bitmap GetImage(
            char symbolChar,
            int width,
            int height,
            Color transparentColor = default,
            int leftOffset = 0,
            int rightOffset = 0)
        {
            // If a transparent color isn't specified, use the default Transparent color
            if (transparentColor == default)
            {
                transparentColor = Color.Transparent;
            }

            Bitmap bitmap = new(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Set the background color to transparent if needed
                if (transparentColor != Color.Empty)
                {
                    graphics.Clear(transparentColor);
                }

                // Set up the text rendering hint to ensure the icon is drawn smoothly
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                // Create the font with the Segoe MDL2 Assets font
                using Font font = new("Segoe MDL2 Assets", height);
                using StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                // Adjust the point to account for any offsets
                Point point = new(width / 2 + leftOffset - rightOffset, height / 2);

                // Draw the text (symbol) onto the bitmap
                graphics.DrawString(symbolChar.ToString(), font, Brushes.Black, point, stringFormat);
            }

            // Return the bitmap with the drawn symbol
            return bitmap;
        }

        /// <summary>
        /// Determines whether a font with the specified name is installed on the system.
        /// </summary>
        /// <param name="fontName">The name of the font to check.</param>
        /// <returns><c>true</c> if the font is installed; otherwise, <c>false</c>.</returns>
        public static bool IsFontInstalled(string fontName)
        {
            return s_fontCache.GetOrAdd(fontName, CheckFontInstalled);
        }

        private static bool CheckFontInstalled(string fontName)
        {
            using var fontsCollection = new InstalledFontCollection();
            return fontsCollection.Families.Any(fontFamily =>
                string.Equals(fontFamily.Name, fontName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
