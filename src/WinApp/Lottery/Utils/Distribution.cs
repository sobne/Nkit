using System;
using System.Collections.Generic;
using System.Drawing;

namespace LotteryChooser.Utils
{
    public class MarginSize
    {
        public MarginSize()
        {
        }
        public MarginSize(int top,int right,int bottom,int left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
    }
    public class Distribution
    {
        public List<Point> Points { get; private set; }
        public Size ItemSize { get; set; }
        public int ColSpace { get; set; }
        public int RowSpace { get; set; }
        private int ColsCount;
        private int RowsCount;
        private int ItemCount;
        private Size ScaleSize;
        private Size DrawSize;
        private MarginSize MarginSize;//画布间距
        private Distribution()
        {
            ColSpace = 10;
            RowSpace = 10;
        }
        public Distribution(Size drawSize,Size scaleSize, MarginSize marginSize) :this()
        {
            drawSize.Width = drawSize.Width - (marginSize.Left + marginSize.Right);
            drawSize.Height = drawSize.Height - (marginSize.Top + marginSize.Bottom);
            DrawSize = drawSize;
            ScaleSize = scaleSize;
            MarginSize = marginSize;
        }
        public void Distribute(int itemCount)
        {
            ItemCount = itemCount;

            CalcColRow();

            var SrcSize = CalcSrcSize();
            ItemSize = CalcItemSize(ScaleSize, SrcSize);

            Points = GetPoints();
        }

        private void CalcColRow()
        {
            ColsCount = 8;
            RowsCount = 2;

            if (ItemCount <= 6)
            {
                ColsCount = ItemCount;
                RowsCount = 1;
            }
            else if (ItemCount <= 20)
            {
                ColsCount = (ItemCount + RowsCount - 1) / 2;
                RowsCount = 2;
            }
            else
            {
                RowsCount = (ItemCount + ColsCount - 1) / ColsCount; ;
            }

            ColSpace = ColsCount >= 10 ? 5 : ColSpace;
        }
        private Size CalcSrcSize()
        {
            var w = DrawSize.Width / ColsCount - ColSpace * ColsCount;
            var h = DrawSize.Height / RowsCount - RowSpace * RowsCount;

            return new Size(w, h);
        }
        private Size CalcItemSize(Size scaleSize,Size srcSize)
        {
            var fh = Math.Round((float)srcSize.Height / scaleSize.Height, 2);
            var fw = Math.Round((float)srcSize.Width / scaleSize.Width, 2);

            var w = srcSize.Width;
            var h = srcSize.Height;

            if (fh <= fw)
            {
                w = (int)(fh * scaleSize.Width);
            }
            else
            {
                h = (int)(fw * scaleSize.Height);
            }
            return new Size(w, h);
        }
        private List<Point> GetPoints()
        {
            var locations = new List<Point>();
            var ic = ColsCount;
            var iRemainCount = ItemCount;
            int disY = (DrawSize.Height - ItemSize.Height * RowsCount) / (RowsCount + 1);
            int r = 0;
            for (int i = 0; i < ItemCount; i++)
            {
                int y = disY + r * (disY + ItemSize.Height);
                ic = iRemainCount > ic ? ic : iRemainCount;
                int disX = (DrawSize.Width - ItemSize.Width * ic) / (ic + 1);
                int x = disX + (i % ic) * (disX + ItemSize.Width);

                locations.Add(new Point
                {
                    X = x + MarginSize.Left,
                    Y = y + MarginSize.Top
                });

                if ((i + 1) % ColsCount == 0)
                {
                    r++;
                }
            }
            return locations;
        }
    }
}
