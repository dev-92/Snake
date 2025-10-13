using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class CherryCollectable : CollectableItemModel
    {
        public CollectableItemType ItemType { get; private set; } = CollectableItemType.Bomb;
        public CherryCollectable(Position2D position) : base(CherryCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Cherry.CHERRY_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CherryCollected;
        }
    }
}
