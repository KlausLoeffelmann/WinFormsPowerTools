using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsPowerTools.AutoLayout
{
    public struct AutoLayoutPadding
    {
        public AutoLayoutPadding(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public float Top { get; }
        public float Bottom { get; }
        public float Left { get; }
        public float Right { get; }
    }
}
