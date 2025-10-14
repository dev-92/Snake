using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    /// <summary>
    /// Represents a collectible rabbit in the game. 
    /// </summary>
    public class RabbitCollectable : CollectableItemModel
    {
        public RabbitCollectable(Position2D position) : base(position)
        {
            this.LifetimeMillis = CollectableConfig.Rabbit.RABBIT_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CollectedItem;
        }
    }
}
