using SnakeCore.Config;
using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;

namespace SnakeCore.Controller
{
    internal class CollectableEffectHandler
    {
        private readonly SnakeModel _snakeModel;
        private readonly InfoboardModel _infoboardModel;

        public CollectableEffectHandler(SnakeModel snakeModel, InfoboardModel infoboardModel)
        {
            this._snakeModel = snakeModel;
            this._infoboardModel = infoboardModel;
        }

        public void Handle(CollectableItemModel item)
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
            
            this._infoboardModel.SpeedFactor /= InfoboardModel.SPEED_UI_FACTOR;
            this._infoboardModel.Score -= this._infoboardModel.SpeedFactor * CollectableConfig.Apple.BASE_SCORE;
        }

        private void HandleCherry(CherryCollectable cherry)
        {
            GameSettings.UpdateSpeedMillis = (int)(GameSettings.UpdateSpeedMillis * CollectableConfig.Cherry.CHERRY_SPEED_FACTOR);

            this._infoboardModel.SpeedFactor *= InfoboardModel.SPEED_UI_FACTOR;
            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Cherry.BASE_SCORE;
        }

        private void HandleDuck(DuckCollectable duck)
        {
            for (int i = 0; i < CollectableConfig.Duck.BASE_SCORE; i++)
            {
                this._snakeModel.ExtendTail();
            }

            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Duck.BASE_SCORE;
            this._infoboardModel.LengthOfSnake += CollectableConfig.Duck.BASE_SCORE;
        }

        private void HandleMouse(MouseCollectable mouse)
        {
            for (int i = 0; i < CollectableConfig.Mouse.BASE_SCORE; i++)
            {
                this._snakeModel.ExtendTail();
            }

            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Mouse.BASE_SCORE;
            this._infoboardModel.LengthOfSnake += CollectableConfig.Mouse.BASE_SCORE;
        }

        private void HandleRabbit(RabbitCollectable rabbit)
        {
            for (int i = 0; i < CollectableConfig.Rabbit.BASE_SCORE; i++)
            {
                this._snakeModel.ExtendTail();
            }

            this._infoboardModel.Score += this._infoboardModel.SpeedFactor * CollectableConfig.Rabbit.BASE_SCORE;
            this._infoboardModel.LengthOfSnake += CollectableConfig.Rabbit.BASE_SCORE;
        }

        private void HandleBomb(BombCollectable bomb)
        {
            this._infoboardModel.Score -= this._infoboardModel.SpeedFactor * CollectableConfig.Bomb.BASE_SCORE;
        }
    }
}
