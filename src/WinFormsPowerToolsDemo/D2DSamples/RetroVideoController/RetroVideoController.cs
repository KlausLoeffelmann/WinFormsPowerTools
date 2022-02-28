using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms.Direct2D;

namespace WinFormsPowerToolsDemo.D2DSamples.RetroVideoController
{
    public class RetroVideoController : Direct2DPanel
    {
        private MultiMediaTimer _mmTimer;
        private IDirect2DImage[] _rasterLines;

        private int _timerCounter;
        private int _timerReloadValue;

        public RetroVideoController()
        {
            _mmTimer = new(1, 1);
            _mmTimer.Elapsed += _mmTimer_Elapsed;
            _rasterLines = new IDirect2DImage[HorizontalResolution];

            _timerReloadValue = 1000 / RefreshRate;
            _timerCounter = _timerReloadValue;
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            IDirect2DImaging imaging = (IDirect2DImaging)this.GetDirect2Graphics();

            for (int counter = 0; counter < _rasterLines.Length; counter++)
            {
                using Bitmap gdiRasterLine = new Bitmap(
                    width: VerticalResolution,
                    height: 1,
                    PixelFormat.Format32bppArgb);

                _rasterLines[counter] = imaging.FromImage(gdiRasterLine);
            }
        }

        private void _mmTimer_Elapsed(object? sender, System.EventArgs e)
        {
            if (_timerCounter-- > 0)
            {
                return;
            }

            _timerCounter = _timerReloadValue;

            RenderRasterlines();
            // TODO: RaiseEvent and pass rasterline.
        }

        private void RenderRasterlines()
        {
            for (var count=0; count<_rasterLines.Length; count++)
            {
                RenderRasterline(count, _rasterLines[count]);
            }
        }

        protected virtual void RenderRasterline(int rasterLineNo, IDirect2DImage rasterLine)
        {
            var charLineIndex = rasterLineNo % 16;


            var upperFrameArea = (HorizontalResolution - VisibleHorizontalResolution) / 2;
            var visibleArea = upperFrameArea + VisibleHorizontalResolution;
            var leftBoderOffset = (VerticalResolution - VisibleVerticalResolution) / 2;

            if (rasterLineNo < upperFrameArea || rasterLineNo > visibleArea)
            {
                for (int x = 0; x < VerticalResolution; x += 4)
                {
                    rasterLine.BitmapBytes[0 + x * 4] = Palette.FrameColor.Color.R;
                    rasterLine.BitmapBytes[1 + x * 4] = Palette.FrameColor.Color.G;
                    rasterLine.BitmapBytes[2 + x * 4] = Palette.FrameColor.Color.B;
                    rasterLine.BitmapBytes[3 + x * 4] = Palette.FrameColor.Color.A;
                }

                return;
            }

            int textRow = (rasterLineNo - upperFrameArea) / 16;

            // TODO: Prepare left border;
            for (var textColumn = 0; textColumn < TextColumns; textColumn++)
            {
                int videoRamValue = TextVideoRam!.Value.Span[textColumn * textRow];
                var fontImage = BitmapFont!.FontImages[videoRamValue];

                for (var characterBit = 0; characterBit < 8; characterBit++)
                {
                    rasterLine.BitmapBytes[0 + textColumn * 8 + characterBit * 4] = fontImage.BitmapBytes[0 + characterBit * 4 + charLineIndex * 4 * 8];
                    rasterLine.BitmapBytes[1 + textColumn * 8 + characterBit * 4] = fontImage.BitmapBytes[1 + characterBit * 4 + charLineIndex * 4 * 8];
                    rasterLine.BitmapBytes[2 + textColumn * 8 + characterBit * 4] = fontImage.BitmapBytes[2 + characterBit * 4 + charLineIndex * 4 * 8];
                    rasterLine.BitmapBytes[3 + textColumn * 8 + characterBit * 4] = fontImage.BitmapBytes[3 + characterBit * 4 + charLineIndex * 4 * 8];
                }
            }
        }

        protected virtual int RefreshRate { get; } = 50;
        protected virtual int VerticalResolution { get; } = 720;
        protected virtual int HorizontalResolution { get; } = 526;
        protected virtual int VisibleVerticalResolution { get; } = 640;
        protected virtual int VisibleHorizontalResolution { get; } = 400;

        protected virtual int TextColumns { get; } = 80;
        protected virtual int TextRows { get; } = 40;

        public Memory<byte>? TextVideoRam { get; set; }
        public Memory<byte>? TextColorRam { get; set; }

        public RetroBitmapFont? BitmapFont { get; set; }
        public RetroColorPalette Palette { get; } = RetroColorPalette.C64Palette;

        protected virtual void CheckPrerequisites()
        {
            if (BitmapFont is null)
                throw new ArgumentNullException("BitmapFont property is not set!");

            if (TextVideoRam is null)
                throw new ArgumentNullException(
                    nameof(TextVideoRam),
                    "TextVideoRam need to be present in the size of TextColumns and TextRows.");

            if (TextColorRam is null)
                throw new ArgumentNullException(
                    nameof(TextColorRam),
                    "TextColorRam need to be present in the size of TextColumns and TextRows.");

            if (TextVideoRam.Value.Length != TextColumns * TextRows)
                throw new ArgumentException(
                    "TextVideoRam needs to be the size stated by TextColumns and TextRows.");

            if (TextColorRam.Value.Length != TextColumns * TextRows)
                throw new ArgumentException(
                    "TextColorRam needs to be the size stated by TextColumns and TextRows.");
        }

        public void Start()
        {
            CheckPrerequisites();
            _mmTimer.Start();
        }

        public void Stop()
        {
            _mmTimer.Stop();
        }
    }
}
