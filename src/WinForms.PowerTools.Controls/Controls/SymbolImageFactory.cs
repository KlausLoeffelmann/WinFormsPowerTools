using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing.Text;

namespace WinForms.PowerTools.Controls
{
    /// <summary>
    ///  Represents a factory for creating symbol images using the Segoe MDL2 Assets font.
    /// </summary>public 
    public class SymbolImageFactory
    {
        public enum BaseFontSetting
        {
            SegoeMDL2Assets,
            SegoeFluentIcons
        }

        private const string SegoeMDL2AssetsFont = "Segoe MDL2 Assets";
        private const string SegoeFluentFont = "Segoe Fluent Icons";

        private static readonly ConcurrentDictionary<string, bool> s_fontCache = new();

        /// <summary>
        ///  Initializes a new instance of the <see cref="SymbolImageFactory"/> class.
        /// </summary>
        /// <param name="symbolValue">Unicode value representation of the character of the Font.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="fontBase">The Font to be used for generating the symbols/icons.</param>
        /// <param name="transparentColor">The transparent color for the image.</param>
        /// <param name="leftOffset">The left offset for the symbol.</param>
        /// <param name="topOffset">The top offset for the symbol.</param>
        /// <param name="getImageLazy">Indicates whether to create the image lazily.</param>
        public SymbolImageFactory(
            int symbolValue,
            int width = 32,
            int height = 32,
            int scalePercentage = 100,
            BaseFontSetting baseFont = BaseFontSetting.SegoeFluentIcons,
            Color foreColor = default,
            Color transparentColor = default,
            int leftOffset = 0,
            int topOffset = 0,
            bool getImageLazy = true) : this(
            symbolChar: (char)symbolValue,
            width: width,
            height: height,
            scalePercentage: scalePercentage,
            baseFont: baseFont,
            foreColor: foreColor,
            transparentColor: transparentColor,
            leftOffset: leftOffset,
            topOffset: topOffset,
            getImageLazy: getImageLazy)
        { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="SymbolImageFactory"/> class.
        /// </summary>
        /// <param name="symbol">One of the <see cref="MDL2Assets"/> values.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="fontBase">The Font to be used for generating the symbols/icons.</param>
        /// <param name="transparentColor">The transparent color for the image.</param>
        /// <param name="leftOffset">The left offset for the symbol.</param>
        /// <param name="topOffset">The top offset for the symbol.</param>
        /// <param name="getImageLazy">Indicates whether to create the image lazily.</param>
        public SymbolImageFactory(
            MDL2Assets symbol,
            int width = 32,
            int height = 32,
            int scalePercentage = 100,
            BaseFontSetting baseFont = BaseFontSetting.SegoeFluentIcons,
            Color foreColor = default,
            Color transparentColor = default,
            int leftOffset = 0,
            int topOffset = 0,
        bool getImageLazy = true) : this(
            symbolChar: (char)symbol,
            width: width,
            height: height,
            scalePercentage: scalePercentage,
            baseFont: baseFont,
            foreColor: foreColor,
            transparentColor: transparentColor,
            leftOffset: leftOffset,
            topOffset: topOffset,
            getImageLazy: getImageLazy)
        { }

        /// <summary>
        ///  Initializes a new instance of the <see cref="SymbolImageFactory"/> class.
        /// </summary>
        /// <param name="symbolChar">The character representing the symbol.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="fontBase">The Font to be used for generating the symbols/icons.</param>
        /// <param name="transparentColor">The transparent color for the image.</param>
        /// <param name="leftOffset">The left offset for the symbol.</param>
        /// <param name="topOffset">The top offset for the symbol.</param>
        /// <param name="getImageLazy">Indicates whether to create the image lazily.</param>
        public SymbolImageFactory(
            char symbolChar,
            int width = 32,
            int height = 32,
            int scalePercentage = 100,
            BaseFontSetting baseFont = BaseFontSetting.SegoeFluentIcons,
            Color foreColor = default,
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
            ScalePercentage = scalePercentage;

            if (transparentColor == default)
            {
                transparentColor = Color.Transparent;
            }

            BaseFont = baseFont;

            TransparentColor = transparentColor;
            SymbolFontName = baseFont == BaseFontSetting.SegoeMDL2Assets
                ? SegoeMDL2AssetsFont
                : SegoeFluentFont;

            if (getImageLazy)
            {
                SymbolImage = new Lazy<Bitmap>(GetImage(
                    SymbolChar,
                    Width,
                    Height,
                    SymbolFontName,
                    foreColor,
                    TransparentColor,
                    LeftOffset,
                    TopOffset,
                    ScalePercentage)).Value;
            }
            else
            {
                SymbolImage = GetImage(
                    SymbolChar,
                    Width,
                    Height,
                    SymbolFontName,
                    foreColor,
                    TransparentColor,
                    LeftOffset,
                    TopOffset,
                    ScalePercentage);
            }
        }

        /// <summary>
        ///  Scaling factor in Percent by which the image is scaled up or down.
        /// </summary>
        [DefaultValue(100)]
        public int ScalePercentage { get; }

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

        public BaseFontSetting BaseFont { get; }

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
        public string SymbolFontName { get; }

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
            string fontName,
            Color foreColor = default,
            Color transparentColor = default,
            int leftOffset = 0,
            int topOffset = 0,
            int scalePercentage = 100)
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
                using Font font = new(
                    fontName,
                    (int)(height * 0.50 * (scalePercentage / 100.0f)));

                using StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                // Adjust the point to account for any offsets
                Point point = new(width / 2 + leftOffset, height / 2 + topOffset);

                // Draw the text (symbol) onto the bitmap
                graphics.DrawString(symbolChar.ToString(), font, new SolidBrush(foreColor), point, stringFormat);
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
