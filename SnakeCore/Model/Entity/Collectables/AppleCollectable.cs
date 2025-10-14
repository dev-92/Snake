using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    public class AppleCollectable : CollectableItemModel
    {
        public AppleCollectable(Position2D position) : base(position)
        {
          this.LifetimeMillis = CollectableConfig.Apple.APPLE_LIFETIME_MILLIS;
          this.SoundEffect = SoundEffectType.AppleCollected;
        }
    }
}
