using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class MouseCollectable : CollectableItemModel
    {
        public CollectableItemType ItemType { get; private set; } = CollectableItemType.Mouse;

        public MouseCollectable(Position2D position) : base(MouseCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Mouse.MOUSE_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CollectedItem;
        }
    }
}
