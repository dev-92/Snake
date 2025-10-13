using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class BombCollectable : CollectableItemModel
    {
        public CollectableItemType ItemType { get; private set; } = CollectableItemType.Bomb;

        public BombCollectable(Position2D position) : base(BombCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Bomb.BOMB_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.BombCollected;
        }
    }
}
