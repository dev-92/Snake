using SnakeCore.Services.UpdateService;
using SnakeCore.Enums;
using SnakeCore.Config;
using SnakeCore.Extensions;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Snake
{
    /// <summary>
    /// Represents the snake in the game, including its head, tail, and movement logic.
    /// Implements <see cref="IUpdateable"/> to be updated each game tick.
    /// Uses the singleton pattern to ensure only one snake instance exists.
    /// </summary>
    public class SnakeModel : IUpdateable
    {
        public Direction CurrentDirection { get; private set; } = Direction.Up;
        public SnakeElement Head { get; private set; }
        public List<SnakeElement> Tail { get; private set; }
        public SnakeModel()
        {
            this.Head = new SnakeElement(this.GetRandomStartPosition());
            this.Tail = this.InitSnakeTail();

            this.SetRandomStartDirection();
        }

        private List<SnakeElement> InitSnakeTail()
        {
            List<SnakeElement> snakeElements = new List<SnakeElement>();

            for(int i = 0; i < CoreConstants.INITIAL_SNAKE_LENGTH; i++)
            {
                snakeElements.Add(new SnakeElement(Position2D.Zero));
            }

            return snakeElements;
        }

        /// <summary>
        /// Generates a random starting position for the snake on the game board.
        /// </summary>
        /// <returns>A <see cref="Position2D"/> representing the start position.</returns>
        private Position2D GetRandomStartPosition()
        {
            Random random = new Random();

            int xPos = random.Next(0, GameSettings.SideLength - 1);
            int yPos = random.Next(0, GameSettings.SideLength - 1);

            return new Position2D(xPos, yPos);
        }

        /// <summary>
        /// Sets a random starting direction for the snake.
        /// </summary>
        private void SetRandomStartDirection()
        {
            int randomDirectionInt = new Random().Next(0, CoreConstants.MAX_DIRECTIONS_VARIANCE);

            this.CurrentDirection = randomDirectionInt switch
            {
                (int)Direction.Up => Direction.Up,
                (int)Direction.Right => Direction.Right,
                (int)Direction.Down => Direction.Down,
                (int)Direction.Left => Direction.Left,
                _ => Direction.Up,
            };
        }

        /// <summary>
        /// Moves the snake's head one step in the current direction.
        /// </summary>
        private void MoveHead()
        {
            this.Head.PreviousPosition = this.Head.CurrentPosition;
            this.Head.CurrentPosition += this.GetCurrentDirectionVector();
        }

        /// <summary>
        /// Returns the movement vector corresponding to the current direction.
        /// </summary>
        /// <returns>A <see cref="Position2D"/> representing the direction vector.</returns>
        public Position2D GetCurrentDirectionVector()
        {
            return this.CurrentDirection switch
            {
                Direction.Up    => DirectionVector.Up,
                Direction.Right => DirectionVector.Right,
                Direction.Down  => DirectionVector.Down,
                Direction.Left  => DirectionVector.Left,
                _               => Position2D.Zero
            };
        }

        /// <summary>
        /// Sets a new movement direction for the snake.
        /// Ignores the input if it is opposite to the current direction.
        /// </summary>
        /// <param name="newDirection">The desired direction.</param>
        public void SetDirection(Direction newDirection)
        {
            if (this.IsOppositeOfCurrentDirection(newDirection)) return;

            this.CurrentDirection = newDirection;
        }

        /// <summary>
        /// Determines whether the given direction is opposite to the current direction.
        /// </summary>
        /// <param name="newDirection">The direction to check.</param>
        /// <returns>True if opposite, otherwise false.</returns>
        private bool IsOppositeOfCurrentDirection(Direction newDirection)
        {
            if (this.CurrentDirection == Direction.Up && newDirection == Direction.Down) return true;
            if (this.CurrentDirection == Direction.Right && newDirection == Direction.Left) return true;
            if (this.CurrentDirection == Direction.Down && newDirection == Direction.Up) return true;
            if (this.CurrentDirection == Direction.Left && newDirection == Direction.Right) return true;

            return false;
        }

        /// <summary>
        /// Wraps the snake around the board if it crosses the boundaries.
        /// </summary>
        private void HandleBoundaryCrossing()
        {
            if (this.Head.CurrentPosition.X < 0) this.Head.CurrentPosition.X += GameSettings.SideLength;
            if (this.Head.CurrentPosition.Y < 0) this.Head.CurrentPosition.Y += GameSettings.SideLength;

            if (this.Head.CurrentPosition.X >= GameSettings.SideLength) this.Head.CurrentPosition.X -= GameSettings.SideLength;
            if (this.Head.CurrentPosition.Y >= GameSettings.SideLength) this.Head.CurrentPosition.Y -= GameSettings.SideLength;
        }

        /// <summary>
        /// Updates the tail segments to follow the head.
        /// </summary>
        private void MoveTail()
        {
            if (this.Tail.IsEmpty()) return;

            this.Tail[0].PreviousPosition = this.Tail[0].CurrentPosition;
            this.Tail[0].CurrentPosition = this.Head.PreviousPosition;

            for (int i = 0; i < this.Tail.Count - 1; i++)
            {
                this.Tail[i + 1].PreviousPosition = this.Tail[i + 1].CurrentPosition;
                this.Tail[i + 1].CurrentPosition = this.Tail[i].PreviousPosition;
            }
        }

        /// <summary>
        /// Adds a new segment to the tail.
        /// </summary>
        public void ExtendTail()
        {
            SnakeElement lastTailElement = this.Tail[this.Tail.Count - 1];
            this.Tail.Add(new SnakeElement(lastTailElement.PreviousPosition));
        }

        /// <summary>
        /// Updates the snake for the current game tick.
        /// Moves the head, wraps around walls, and moves the tail.
        /// </summary>
        public void Update()
        {
            this.MoveHead();
            this.HandleBoundaryCrossing();
            this.MoveTail();
        }
    }
}
