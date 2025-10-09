using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class AppleCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/apple.png";

        public AppleCollectable(Position2D position) : base(AppleCollectable.IMAGE_PATH, position)
        {
          this.LifetimeMillis = CollectableConfig.Apple.APPLE_LIFETIME_MILLIS;
          this.SoundEffect = SoundEffectType.AppleCollected;
        }
    }
}
