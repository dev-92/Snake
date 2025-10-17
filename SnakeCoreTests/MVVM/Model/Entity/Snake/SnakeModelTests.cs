
using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;

namespace SnakeCoreTests.MVVM.Model.Entity.Snake
{
    public class SnakeModelTests
    {
        private SnakeModel _snake { get; set; }
        private const int INIT_REP_FACTOR = 15;

        public SnakeModelTests()
        {
            this._snake = new SnakeModel();
        }

        [Fact]
        public void Constructor_ModelIsInitializedMultipleTimes_HeadHasDifferentPositions()
        {
            // Arrange
            Position2D currentPosition = this._snake.Head.CurrentPosition;
            List<Position2D> createdHeadPositions = new List<Position2D> { currentPosition };

            // Act
            for(int i = 0; i < SnakeModelTests.INIT_REP_FACTOR; i++)
            {
                this._snake = new SnakeModel();
                createdHeadPositions.Add(this._snake.Head.CurrentPosition);
            }

            // Assert
            Assert.True(createdHeadPositions.Distinct().Count() > 1);
        }

        [Fact]
        public void Constructor_ModelIsInitializedMultipleTimes_TailHasAlwaysTheSameLength()
        {
            // Arrange
            List<int> tailLengths = new();

            // Act
            for (int i = 0; i < SnakeModelTests.INIT_REP_FACTOR; i++)
            {
                this._snake = new SnakeModel();
                tailLengths.Add(this._snake.Tail.Count);
            }

            // Assert
            Assert.True(tailLengths.Distinct().Count() == 1);
        }

        [Fact]
        public void Constructor_ModelIsInitializedMultipleTimes_StartingDirectionDiffers()
        {
            // Arrange
            List<Direction> startingDirections = new();

            // Act
            for (int i = 0; i < SnakeModelTests.INIT_REP_FACTOR; i++)
            {
                this._snake = new SnakeModel();
                startingDirections.Add(this._snake.CurrentDirection);
            }

            // Assert
            Assert.True(startingDirections.Distinct().Count() > 1);
        }

        [Fact]
        public void SetDirection_NoOppositeDirection_CurrentDirectionWillBeSetToNewDirection()
        {
            // Arrange
            Direction currentDirection = this._snake.CurrentDirection;

            Direction newDirection = currentDirection switch
            {
                Direction.Up or Direction.Down      => Direction.Left,
                Direction.Left or Direction.Right   => Direction.Up,
                _                                   => Direction.Up
            };

            // Act
            this._snake.SetDirection(newDirection);

            // Assert
            Assert.Equal(newDirection, this._snake.CurrentDirection);
        }

        [Fact]
        public void SetDirection_OppositeDirection_CurrentDirectionWillBeTheSame()
        {
            // Arrange
            Direction currentDirection = this._snake.CurrentDirection;

            Direction oppositeDirection = currentDirection switch
            {
                Direction.Up    => Direction.Down,
                Direction.Down  => Direction.Up,
                Direction.Left  => Direction.Right,
                Direction.Right => Direction.Left,
                _               => currentDirection
            };

            // Act
            this._snake.SetDirection(oppositeDirection);

            // Assert
            Assert.Equal(currentDirection, this._snake.CurrentDirection); 
        }

        [Fact]
        public void Update_HeadWrapsAroundBoard_CorrectlyWrapped()
        {
            // Arrange
            var initialPositions = new List<Position2D>
            {
                new Position2D(-1, 5),                 
                new Position2D(5, -1),                  
                new Position2D(GameSettings.SideLength, 5),  
                new Position2D(5, GameSettings.SideLength)   
            };

            foreach (Position2D pos in initialPositions)
            {
                this._snake.Head.CurrentPosition = pos;

                Position2D afterMoveHead = this._snake.Head.CurrentPosition + this._snake.GetCurrentDirectionVector();

                Position2D expectedWrapped = new Position2D(
                    (afterMoveHead.X + GameSettings.SideLength) % GameSettings.SideLength,
                    (afterMoveHead.Y + GameSettings.SideLength) % GameSettings.SideLength
                );

                // Act
                this._snake.Update();

                // Assert
                Assert.Equal(expectedWrapped, this._snake.Head.CurrentPosition);
            }
        }

        [Fact]
        public void Update_TailIsNotEmpty_AllTailPartsWillBeMoved()
        {
            // Arrange
            this._snake.Head.CurrentPosition = new Position2D(5, 5);
            this._snake.Head.PreviousPosition = new Position2D(5, 4);

            this._snake.Tail[0].CurrentPosition = new Position2D(4, 4);
            this._snake.Tail[1].CurrentPosition = new Position2D(3, 4);

            var oldTailPositions = this._snake.Tail.Select(tail => tail.CurrentPosition).ToList();

            // Act
            this._snake.Update(); 

            // Assert
            Assert.Equal(this._snake.Head.PreviousPosition, this._snake.Tail[0].CurrentPosition);
            Assert.Equal(oldTailPositions[0], this._snake.Tail[1].CurrentPosition);
        }

        [Fact]
        public void ExtendTail_ExtendTailIsCalled_OnePartWillBeAdded()
        {
            // Arrange
            int tailLengthBefore = this._snake.Tail.Count;

            // Act
            this._snake.ExtendTail();
            int tailLengthAfter = this._snake.Tail.Count;

            // Assert
            Assert.Equal(tailLengthBefore + 1, tailLengthAfter);
        }
    }
}
