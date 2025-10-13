using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class RabbitCollectable : CollectableItemModel
    {
        public CollectableItemType ItemType { get; private set; } = CollectableItemType.Rabbit;

        public RabbitCollectable(Position2D position) : base(RabbitCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Rabbit.RABBIT_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CollectedItem;
        }
    }
}
