using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeTests.MVVM.Model.Entity.Snake
{
    public class SnakeModelTests
    {
        [Fact]
        public void Constructor_ShouldInitializeHeadAndTail_True()
        {
            SnakeModel snake = new();

            Assert.NotNull(snake.Head);
            Assert.NotNull(snake.Tail);

            Assert.Equal(Constants.INITIAL_SNAKE_LENGTH, snake.Tail.Count);
        }

        [Theory]
        [InlineData(Direction.Up,    nameof(DirectionVector.Up))]
        [InlineData(Direction.Right, nameof(DirectionVector.Right))]
        [InlineData(Direction.Down,  nameof(DirectionVector.Down))]
        [InlineData(Direction.Left,  nameof(DirectionVector.Left))]
        public void GetCurrentDirection_CurrentDirectionIsUp_True(Direction direction, string vectorName)
        {
            SnakeModel snake = new();
            snake.SetDirection(direction);

            Position2D currentDirection = snake.GetCurrentDirectionVector();
            Position2D expected = vectorName switch
            {
                nameof(DirectionVector.Up)    => DirectionVector.Up,
                nameof(DirectionVector.Right) => DirectionVector.Right,
                nameof(DirectionVector.Down)  => DirectionVector.Down,
                nameof(DirectionVector.Left)  => DirectionVector.Left,
                _                             => Position2D.Zero
            };

            Assert.Equal(expected, snake.GetCurrentDirectionVector());
        }

        [Fact]
        public void SetDirection_NewDirectionIsNoOpposite_True()
        {
            SnakeModel snake = new();
            Position2D currentDirection = snake.GetCurrentDirectionVector();

        }

        [Fact]
        public void SetDirection_NewDirectionIsOpposite_True()
        {
            SnakeModel snake = new();
            Assert.NotNull(snake);
        }
    }
}
