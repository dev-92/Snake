using SnakeCore.Config;
using SnakeCore.Controller;
using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;

namespace SnakeCoreTests
{
    public class CollectableEffectHandlerTests
    {
        private SnakeModel _snake { get; set; }
        private InfoboardModel _infoboardModel { get; set; }
        private CollectableEffectHandler _collectableEffectHandler { get; set; }

        private double _oldScore { get; set; }
        private double _oldSpeedFactor { get; set; }
        private int _oldTailLength { get; set; }
        private int _oldLengthOfSnake { get; set; }

        public CollectableEffectHandlerTests()
        {
            // Arrange common setup
            this._snake = new SnakeModel();
            this._infoboardModel = new InfoboardModel();
            this._collectableEffectHandler = new CollectableEffectHandler(this._snake, this._infoboardModel);

            this._oldScore = this._infoboardModel.Score;
            this._oldSpeedFactor = this._infoboardModel.SpeedFactor;
            this._oldTailLength = this._snake.Tail.Count;
            this._oldLengthOfSnake = this._infoboardModel.LengthOfSnake;
        }

        // Apple
        [Fact]
        public void HandleApple_UpdateScore_ScoreIsLowerThanBefore()
        {
            // Arrange
            AppleCollectable apple = new AppleCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(apple);

            // Assert
            Assert.True(this._infoboardModel.Score < this._oldScore);
        }

        [Fact]
        public void HandleApple_UpdateSpeedFactor_SpeedFactorIsLowerThanBefore()
        {
            // Arrange
            AppleCollectable apple = new AppleCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(apple);

            // Assert
            Assert.True(this._infoboardModel.SpeedFactor < this._oldSpeedFactor);
        }

        [Fact]
        public void HandleApple_UpdateTailLength_TailLengthIsSameAsBefore()
        {
            // Arrange
            AppleCollectable apple = new AppleCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(apple);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.Equal(this._oldTailLength, this._snake.Tail.Count);
        }

        // Cherry
        [Fact]
        public void HandleCherry_UpdateScore_ScoreIsHigherThanBefore()
        {
            // Arrange
            CherryCollectable cherry = new CherryCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(cherry);

            // Assert
            Assert.True(this._infoboardModel.Score > this._oldScore);
        }

        [Fact]
        public void HandleCherry_UpdateSpeedFactor_SpeedFactorIsHigherThanBefore()
        {
            // Arrange
            CherryCollectable cherry = new CherryCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(cherry);

            // Assert
            Assert.True(this._infoboardModel.SpeedFactor > this._oldSpeedFactor);
        }

        [Fact]
        public void HandleCherry_UpdateTailLength_TailLengthIsSameAsBefore()
        {
            // Arrange
            CherryCollectable cherry = new CherryCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(cherry);

            // Assert
            Assert.Equal(this._oldTailLength, this._snake.Tail.Count);
        }

        // Duck
        [Fact]
        public void HandleDuck_UpdateScore_ScoreIsHigherThanBefore()
        {
            // Arrange
            DuckCollectable duck = new DuckCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(duck);

            // Assert
            Assert.True(this._infoboardModel.Score > this._oldScore);
        }

        [Fact]
        public void HandleDuck_UpdateTailLength_TailLengthIsGreaterThanBefore()
        {
            // Arrange
            DuckCollectable duck = new DuckCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(duck);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.True(this._snake.Tail.Count > this._oldTailLength);
        }

        [Fact]
        public void HandleDuck_UpdateTailLength_LengthIncreasedByBaseScore()
        {
            // Arrange
            DuckCollectable duck = new DuckCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(duck);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.Equal(this._oldLengthOfSnake + CollectableConfig.Duck.BASE_SCORE, this._infoboardModel.LengthOfSnake);
        }

        // Mouse
        [Fact]
        public void HandleMouse_UpdateScore_ScoreIsHigherThanBefore()
        {
            // Arrange
            MouseCollectable mouse = new MouseCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(mouse);

            // Assert
            Assert.True(this._infoboardModel.Score > this._oldScore);
        }

        [Fact]
        public void HandleMouse_UpdateTailLength_TailLengthIsGreaterThanBefore()
        {
            // Arrange
            MouseCollectable mouse = new MouseCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(mouse);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.True(this._snake.Tail.Count > this._oldTailLength);
        }

        [Fact]
        public void HandleMouse_UpdateTailLength_LengthIncreasedByBaseScore()
        {
            // Arrange
            MouseCollectable mouse = new MouseCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(mouse);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.Equal(this._oldLengthOfSnake + CollectableConfig.Mouse.BASE_SCORE, this._infoboardModel.LengthOfSnake);
        }

        // Rabbit
        [Fact]
        public void HandleRabbit_UpdateScore_ScoreIsHigherThanBefore()
        {
            // Arrange
            RabbitCollectable rabbit = new RabbitCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(rabbit);

            // Assert
            Assert.True(this._infoboardModel.Score > this._oldScore);
        }

        [Fact]
        public void HandleRabbit_UpdateTailLength_TailLengthIsGreaterThanBefore()
        {
            // Arrange
            RabbitCollectable rabbit = new RabbitCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(rabbit);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.True(this._snake.Tail.Count > this._oldTailLength);
        }

        [Fact]
        public void HandleRabbit_UpdateTailLength_LengthIncreasedByBaseScore()
        {
            // Arrange
            RabbitCollectable rabbit = new RabbitCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(rabbit);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.Equal(this._oldLengthOfSnake + CollectableConfig.Rabbit.BASE_SCORE, this._infoboardModel.LengthOfSnake);
        }

        // Bomb
        [Fact]
        public void HandleBomb_UpdateScore_ScoreIsLowerThanBefore()
        {
            // Arrange
            BombCollectable bomb = new BombCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(bomb);

            // Assert
            Assert.True(this._infoboardModel.Score < this._oldScore);
        }

        [Fact]
        public void HandleBomb_UpdateTailLength_TailLengthIsSameAsBefore()
        {
            // Arrange
            BombCollectable bomb = new BombCollectable(Position2D.Zero);

            // Act
            this._collectableEffectHandler.Handle(bomb);
            this._collectableEffectHandler.Update();

            // Assert
            Assert.Equal(this._oldTailLength, this._snake.Tail.Count);
        }
    }
}
