using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class AppleCollectable : CollectableItemModel
    {
        public CollectableItemType ItemType { get; private set; } = CollectableItemType.Apple; 

        public AppleCollectable(Position2D position) : base(AppleCollectable.IMAGE_PATH, position)
        {
          this.LifetimeMillis = CollectableConfig.Apple.APPLE_LIFETIME_MILLIS;
          this.SoundEffect = SoundEffectType.AppleCollected;
        }
    }
}
