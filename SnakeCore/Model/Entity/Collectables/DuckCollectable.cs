using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    public class DuckCollectable : CollectableItemModel
    {
        public DuckCollectable(Position2D position) : base(position)
        {
            this.LifetimeMillis = CollectableConfig.Duck.Duck_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.DuckCollected;
        }
    }
}
