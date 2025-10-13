using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class DuckCollectable : CollectableItemModel
    {
        public CollectableItemType ItemType { get; private set; } = CollectableItemType.Duck;

        public DuckCollectable(Position2D position) : base(DuckCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Duck.Duck_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.DuckCollected;
        }
    }
}
