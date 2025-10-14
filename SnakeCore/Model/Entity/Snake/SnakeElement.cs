using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Snake
{
    /// <summary>
    /// Represents a single segment of the snake on the game board.
    /// Contains information about the current and previous positions
    /// to facilitate movement and tail-following logic.
    /// </summary>
    public class SnakeElement
    {
        public Position2D CurrentPosition { get; set; }

        public Position2D PreviousPosition { get; set; } = Position2D.Zero;

        public SnakeElement(Position2D currentPosition)
        {
            this.CurrentPosition = currentPosition;
        }
    }
}
