using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms.Direct2D;

namespace WinFormsPowerToolsDemo.D2DSamples.RetroVideoController
{
    internal class RetroBitmapFont
    {
        private IDirect2DImage[] _fontImages;

        public RetroBitmapFont(IDirect2DImage[] fontImages)
        {
            _fontImages = fontImages;
        }

        public static RetroBitmapFont? BitmapArrayFromHexFontFile(string hexFontFileName, IDirect2DImaging d2dImaging)
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

                var image = new Bitmap(
                    16, 
                    16, 
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                var d2dGlyphImage = d2dImaging.FromImage(image);





            }

            return null;
            
        }
    }
}
