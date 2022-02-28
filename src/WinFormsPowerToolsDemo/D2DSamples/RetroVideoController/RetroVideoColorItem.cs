using System.Drawing;

namespace WinFormsPowerToolsDemo.D2DSamples.RetroVideoController
{
    public struct RetroVideoColorItem
    {
        public RetroVideoColorItem(int index, string name, Color color)
        {
            Index = index;
            Name = name;
            Color = color;
        }

        public int Index { get; }
        public string Name { get; }
        public Color Color { get; }
    }
}
