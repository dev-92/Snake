using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    public class MouseCollectable : CollectableItemModel
    {
        public MouseCollectable(Position2D position) : base(position)
        {
            this.LifetimeMillis = CollectableConfig.Mouse.MOUSE_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CollectedItem;
        }
    }
}
