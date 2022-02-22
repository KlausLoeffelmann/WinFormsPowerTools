using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms.Direct2D;

namespace WinFormsPowerToolsDemo.D2DSamples.RetroVideoController
{
    internal class RetroBitmapFont
    {
        private IDirect2DImage[] _fontImages;
        private const int FontNumberCharOffset = 6;

        public RetroBitmapFont(IDirect2DImage[] fontImages)
        {
            _fontImages = fontImages;
        }

        public unsafe static RetroBitmapFont BitmapArrayFromHexFontFile(string hexFontFileName, IDirect2DImaging d2dImaging)
        {
            List<IDirect2DImage> fontMap = new();

            using StreamReader reader = new StreamReader(hexFontFileName);
            while (reader.ReadLine() is { } currentLine)
            {
                var currentLineNumber = Convert.ToInt32(
                    currentLine[0..4],
                    fromBase: 16);

                byte[] fontBytes = new byte[(currentLine.Length - FontNumberCharOffset) / 2];

                int count = 0;
                int upperBound = (currentLine.Length - FontNumberCharOffset) / 2;
                while (count < upperBound)
                {
                    var hexByte = currentLine.Substring(count * 2 + FontNumberCharOffset, 2);
                    fontBytes[count] = Convert.ToByte(
                        hexByte,
                        fromBase: 16);

                    Debug.Print($"{Convert.ToString(fontBytes[count], 2).PadLeft(8, '0')}");

                    count++;
                }

                Debug.Print("");

                var image = new Bitmap(
                    fontBytes.Length / 2,
                    16,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                var d2dGlyphImage = d2dImaging.FromImage(image);

                for (int byteCount = 0; byteCount < fontBytes.Length; byteCount++)
                {
                    byte bitValue = 128;

                    for (int bitCount = 0; bitCount < 8; bitCount++)
                    {
                        fixed (byte* pixelBytes = d2dGlyphImage.BitmapBytes)
                        {
                            uint* pixel = (uint*)pixelBytes;
                            var pixelValue = (fontBytes[byteCount] & bitValue) == bitValue
                                ? uint.MaxValue
                                : 0;

                            *pixel++ = pixelValue;
                            bitValue >>= 1;
                        }
                    }
                }

                fontMap.Add(d2dGlyphImage);
            }

            return new RetroBitmapFont(fontMap.ToArray());
        }
    }
}
