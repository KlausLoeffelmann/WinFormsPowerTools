using System.Collections.ObjectModel;
using System.Drawing;

namespace WinFormsPowerToolsDemo.D2DSamples.RetroVideoController
{
    public class RetroColorPalette : KeyedCollection<int, RetroVideoColorItem>
    {
        private static RetroColorPalette? _c64Palette;
        private static RetroVideoColorItem Black = new RetroVideoColorItem(0, "black", Color.Black);
        private static RetroVideoColorItem White = new RetroVideoColorItem(0, "black", Color.White);

        protected override int GetKeyForItem(RetroVideoColorItem item) => item.Index;

        public RetroVideoColorItem FrameColor { get; set; } = Black;
        public RetroVideoColorItem BackColor { get; set; } = Black;
        public RetroVideoColorItem ForeColor { get; set; } = White;

        public static RetroColorPalette C64Palette
        {
            get
            {
                if (_c64Palette is null)
                {
                    _c64Palette = new RetroColorPalette
                    {
                        new RetroVideoColorItem(0, "black", Color.Black),
                        new RetroVideoColorItem(1, "white", Color.White),
                        new RetroVideoColorItem(2, "Red", Color.FromArgb(136, 0, 0)),
                        new RetroVideoColorItem(3, "cyan", Color.FromArgb(170, 255, 238)),
                        new RetroVideoColorItem(4, "purple", Color.FromArgb(204, 68, 204)),
                        new RetroVideoColorItem(5, "green", Color.FromArgb(0, 204, 85)),
                        new RetroVideoColorItem(6, "blue", Color.FromArgb(0, 0, 170)),
                        new RetroVideoColorItem(7, "yellow", Color.FromArgb(238, 238, 119)),
                        new RetroVideoColorItem(8, "orange", Color.FromArgb(221, 136, 85)),
                        new RetroVideoColorItem(9, "brown", Color.FromArgb(102, 68, 0)),
                        new RetroVideoColorItem(10, "lightred", Color.FromArgb(255, 119, 119)),
                        new RetroVideoColorItem(11, "darkgrey", Color.FromArgb(51, 51, 51)),
                        new RetroVideoColorItem(12, "grey", Color.FromArgb(119, 119, 119)),
                        new RetroVideoColorItem(13, "lightgreen", Color.FromArgb(170, 255, 102)),
                        new RetroVideoColorItem(14, "lightblue", Color.FromArgb(0, 136, 255)),
                        new RetroVideoColorItem(15, "lightgrey", Color.FromArgb(187, 187, 187))
                    };

                    _c64Palette.BackColor = _c64Palette[14];
                    _c64Palette.FrameColor = _c64Palette[6];
                    _c64Palette.ForeColor = _c64Palette[3];
                }

                return _c64Palette;
            }
        }
    }
}
