using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class BombCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/bomb.png";

        public BombCollectable(Position2D position) : base(BombCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Bomb.BOMB_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.BombCollected;
        }

        protected override void HandleCollected()
        {

        }
    }
}
