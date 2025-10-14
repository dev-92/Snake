using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    public class BombCollectable : CollectableItemModel
    {
        public BombCollectable(Position2D position) : base(position)
        {
            this.LifetimeMillis = CollectableConfig.Bomb.BOMB_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.BombCollected;
        }
    }
}
