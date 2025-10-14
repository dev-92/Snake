namespace SnakeCore.Model.ValueObject
{
    /// <summary>
    /// Represents a two-dimensional position or vector with X and Y coordinates.
    /// Supports basic arithmetic operations for position calculations.
    /// </summary>
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

        public Position2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Adds two <see cref="Position2D"/> instances and returns the resulting position.
        /// </summary>
        public static Position2D operator +(Position2D firstPos, Position2D secPos)
        {
            return new Position2D(firstPos.X + secPos.X, firstPos.Y + secPos.Y);
        }

        /// <summary>
        /// Subtracts one <see cref="Position2D"/> instance from another and returns the resulting position.
        /// </summary>
        public static Position2D operator -(Position2D firstPos, Position2D secPos)
        {
            return new Position2D(firstPos.X - secPos.X, firstPos.Y - secPos.Y);
        }

        /// <summary>
        /// Determines whether two <see cref="Position2D"/> instances are equal.
        /// </summary>
        public static bool operator ==(Position2D firstPos, Position2D secPos)
        {
            return firstPos.X == secPos.X && firstPos.Y == secPos.Y;
        }

        /// <summary>
        /// Determines whether two <see cref="Position2D"/> instances are not equal.
        /// </summary>
        public static bool operator !=(Position2D firstPos, Position2D secPos)
        {
            return !(firstPos == secPos);
        }

        /// <summary>
        /// Returns a string representation of the position in the format "[X:Y]".
        /// </summary>
        public override string ToString()
        {
            return $"[{this.X}:{this.Y}]";
        }
    }
}
