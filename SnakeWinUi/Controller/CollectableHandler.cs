using SnakeWinUi.Config;
using SnakeWinUi.MVVM.Model.Entity.Collectables;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.ViewModel;

namespace SnakeWinUi.Controller
{
    internal class CollectableHandler
    {
        private readonly SnakeModel _snake;
        private readonly InfoboardViewModel _infoboard;

        public CollectableHandler(SnakeModel snake, InfoboardViewModel infoboard)
        {
            this._snake = snake;
            this._infoboard = infoboard;
        }

        public void Handle(CollectableItem item)
        {
            switch(item)
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

        private void HandleApple(AppleCollectable apple)
        {
            GameSettings.UpdateSpeedMillis = (int)(GameSettings.UpdateSpeedMillis * CollectableConfig.Apple.APPLE_SPEED_FACTOR);
            
            this._infoboard.SpeedFactor /= InfoboardViewModel.SPEED_UI_FACTOR;
            this._infoboard.Score -= this._infoboard.SpeedFactor * CollectableConfig.Apple.BASE_SCORE;
        }

        private void HandleCherry(CherryCollectable cherry)
        {
            GameSettings.UpdateSpeedMillis = (int)(GameSettings.UpdateSpeedMillis * CollectableConfig.Cherry.CHERRY_SPEED_FACTOR);

            this._infoboard.SpeedFactor *= InfoboardViewModel.SPEED_UI_FACTOR;
            this._infoboard.Score += this._infoboard.SpeedFactor * CollectableConfig.Cherry.BASE_SCORE;
        }

        private void HandleDuck(DuckCollectable duck)
        {
            for (int i = 0; i < CollectableConfig.Duck.BASE_SCORE; i++)
                this._snake.ExtendTail();

            this._infoboard.Score += this._infoboard.SpeedFactor * CollectableConfig.Duck.BASE_SCORE;
            this._infoboard.LengthOfSnake += CollectableConfig.Duck.BASE_SCORE;
        }

        private void HandleMouse(MouseCollectable mouse)
        {
            for (int i = 0; i < CollectableConfig.Mouse.BASE_SCORE; i++)
                this._snake.ExtendTail();

            this._infoboard.Score += this._infoboard.SpeedFactor * CollectableConfig.Mouse.BASE_SCORE;
            this._infoboard.LengthOfSnake += CollectableConfig.Mouse.BASE_SCORE;
        }

        private void HandleRabbit(RabbitCollectable rabbit)
        {
            for (int i = 0; i < CollectableConfig.Rabbit.BASE_SCORE; i++)
                this._snake.ExtendTail();

            this._infoboard.Score += this._infoboard.SpeedFactor * CollectableConfig.Rabbit.BASE_SCORE;
            this._infoboard.LengthOfSnake += CollectableConfig.Rabbit.BASE_SCORE;
        }

        private void HandleBomb(BombCollectable bomb)
        {
            this._infoboard.Score -= this._infoboard.SpeedFactor * CollectableConfig.Bomb.BASE_SCORE;
        }
    }
}
