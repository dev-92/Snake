using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.Model
{
    public class Position2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static Position2D Zero
        {
            get
            {
                return new Position2D(0, 0);
            }
        }

        public Position2D()
        {

        }

        public Position2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Position2D operator +(Position2D firstPos, Position2D secPos)
        {
            return new Position2D(firstPos.X + secPos.X, firstPos.Y + secPos.Y);
        }

    }
}
