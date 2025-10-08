using SnakeWinUi.Config;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class BirdCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/bird.png";

        public BirdCollectable(Position2D position) : base(BirdCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Bird.BIRD_LIFETIME_MILLIS;
        }

        protected override void HandleCollected()
        {
            for(int i = 0; i < CollectableConfig.Bird.SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }
        }
    }
}
