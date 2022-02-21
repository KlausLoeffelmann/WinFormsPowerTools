using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace WinFormsPowerToolsDemo.D2DSamples.RetroVideoController
{
    internal class RetroBitmapFont
    {
        private Image[] _fontImages;

        public RetroBitmapFont(Image[] fontImages)
        {
            _fontImages = fontImages;
        }

        public static RetroBitmapFont? BitmapArrayFromHexFontFile(string hexFontFileName)
        {
            List<byte[]> fontByteMap = new();

            using StreamReader reader = new StreamReader(hexFontFileName);
            while (reader.ReadLine() is { } currentLine)
            {
                var currentLineNumber = Convert.ToInt32(
                    currentLine[0..4], 
                    fromBase: 16);

                byte[] fontBytes = new byte[(currentLine.Length - 6) / 2];

                int count = 0;
                while (count < currentLine.Length)
                {
                    fontBytes[count] = Convert.ToByte(
                        currentLine[(count * 2 + 5)..(count * 2 + 6)],
                        fromBase: 16);
                    count += 2;
                }

                var image = new Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            }

            return null;
            
        }
    }
}
