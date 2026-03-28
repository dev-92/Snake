using SnakeCore.Config;
using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Services.UpdateService;

namespace SnakeCore.Controller
{
    /// <summary>
    /// Handles the effects of collectable items when they are picked up by the snake.
    /// Updates the snake model and infoboard model accordingly.
    /// </summary>
    public class CollectableEffectHandler : IUpdateable
    {
        private readonly SnakeModel _snakeModel;
        private readonly InfoboardModel _infoboardModel;

        private readonly List<int> _pendingTailExtensions = new();

        public CollectableEffectHandler(SnakeModel snakeModel, InfoboardModel infoboardModel)
        {
            this._snakeModel = snakeModel;
            this._infoboardModel = infoboardModel;
        }

        /// <summary>
        /// Handles the given collectable item by applying its effect to the snake and infoboard.
        /// </summary>
        /// <param name="item">The collectable item that was collected.</param>
        public void Handle(CollectableItemModel item)
        {
            switch (item)
            {
                case AppleCollectable apple:
                    this.HandleApple(apple);
                    break;

                case CherryCollectable cherry:
                    this.HandleCherry(cherry);
                    break;

                case DuckCollectable duck:
                    this.HandleDuck(duck);
                    break;

                case MouseCollectable mouse:
                    this.HandleMouse(mouse);
                    break;

                case RabbitCollectable rabbit:
                    this.HandleRabbit(rabbit);
                    break;

                case BombCollectable bomb:
                    this.HandleBomb(bomb);
                    break;
            }
        }

        /// <summary>
        /// Applies the effect of an apple collectable.
        /// Decreases score and speed factor.
        /// </summary>
        private void HandleApple(AppleCollectable apple)
        {
            GameSettings.UpdateSpeedMillis = (int)(GameSettings.UpdateSpeedMillis * CollectableConfig.Apple.APPLE_SPEED_FACTOR);

            this._infoboardModel.SpeedFactor /= InfoboardModel.SPEED_UI_FACTOR;
            this._infoboardModel.Score -= this._infoboardModel.SpeedFactor * CollectableConfig.Apple.BASE_SCORE;
        }

        /// <summary>
        /// Applies the effect of a cherry collectable.
        /// Increases score and speed factor.
        /// </summary>
        private void HandleCherry(CherryCollectable cherry)
        {
            GameSettings.UpdateSpeedMillis = (int)(GameSettings.UpdateSpeedMillis * CollectableConfig.Cherry.CHERRY_SPEED_FACTOR);

            this._infoboardModel.SpeedFactor *= InfoboardModel.SPEED_UI_FACTOR;
            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Cherry.BASE_SCORE;
        }

        /// <summary>
        /// Applies the effect of a duck collectable.
        /// Extends the snake tail and increases score and snake length.
        /// </summary>
        private void HandleDuck(DuckCollectable duck)
        {
            this.AddTailExtension(CollectableConfig.Duck.BASE_SCORE);

            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Duck.BASE_SCORE;
            this._infoboardModel.LengthOfSnake += CollectableConfig.Duck.BASE_SCORE;
        }

        /// <summary>
        /// Applies the effect of a mouse collectable.
        /// Extends the snake tail and increases score and snake length.
        /// </summary>
        private void HandleMouse(MouseCollectable mouse)
        {
            this.AddTailExtension(CollectableConfig.Mouse.BASE_SCORE);

            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Mouse.BASE_SCORE;
            this._infoboardModel.LengthOfSnake += CollectableConfig.Mouse.BASE_SCORE;
        }

        /// <summary>
        /// Applies the effect of a rabbit collectable.
        /// Extends the snake tail and increases score and snake length.
        /// </summary>
        private void HandleRabbit(RabbitCollectable rabbit)
        {
            this.AddTailExtension(CollectableConfig.Rabbit.BASE_SCORE);

            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Rabbit.BASE_SCORE;
            this._infoboardModel.LengthOfSnake += CollectableConfig.Rabbit.BASE_SCORE;
        }

        /// <summary>
        /// Applies the effect of a bomb collectable.
        /// Decreases score without affecting snake length.
        /// </summary>
        private void HandleBomb(BombCollectable bomb)
        {
            this._infoboardModel.Score -= this._infoboardModel.SpeedFactor * CollectableConfig.Bomb.BASE_SCORE;
        }

        /// <summary>
        /// Adds the given number of elements to the snakes tail
        /// </summary>
        /// <param name="length"></param>
        private void AddTailExtension(int length)
        {
            this._pendingTailExtensions.Add(length);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            for (int i = this._pendingTailExtensions.Count - 1; i >= 0; i--)
            {
                this._snakeModel.ExtendTail();
                this._pendingTailExtensions[i]--;

                if (this._pendingTailExtensions[i] <= 0)
                {
                    this._pendingTailExtensions.RemoveAt(i);
                }
            }
        }
    }
}
