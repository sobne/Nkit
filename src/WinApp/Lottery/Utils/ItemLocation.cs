using System.Collections.Generic;
using System.Drawing;

namespace Lottery.Utils
{
    public class ItemLocation
    {
        private int _MaxColumns;
        private int _MaxRows;
        private int _ItemCount;
        private Size _ItemSize;
        private Size _DrawSize;
        private ItemLocation()
        {

        }
        public ItemLocation(int maxColumns, int maxRows, int itemCount, Size itemSize, Size drawSize) : this()
        {
            _MaxColumns = maxColumns;
            _MaxRows = maxRows;
            _ItemCount = itemCount;
            _ItemSize = itemSize;
            _DrawSize = drawSize;
        }
        public List<Point> GetPoints()
        {
            var locations = new List<Point>();

            var pw = _DrawSize.Width;
            var ph = _DrawSize.Height;
            var h = _ItemSize.Height;
            var w = _ItemSize.Width;
            var ic = _MaxColumns;
            var iRemainCount = _ItemCount;
            int disY = (ph - h * _MaxRows) / (_MaxRows + 1);
            int r = 0;
            for (int i = 0; i < _ItemCount; i++)
            {
                int y = disY + r * (disY + h);
                ic = iRemainCount > ic ? ic : iRemainCount;
                int disX = (pw - w * ic) / (ic + 1);
                int x = disX + (i % ic) * (disX + w);

                locations.Add(new Point
                {
                    X = x,
                    Y = y
                });

                if ((i + 1) % _MaxColumns == 0)
                {
                    r++;
                }
            }
            return locations;
        }
    }
}
