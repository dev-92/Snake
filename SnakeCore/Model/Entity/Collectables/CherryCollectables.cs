using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    /// <summary>
    /// Represents a collectible cherry in the game.
    /// </summary>
    public class CherryCollectable : CollectableItemModel
    {
        public CherryCollectable(Position2D position) : base(position)
        {
            this.LifetimeMillis = CollectableConfig.Cherry.CHERRY_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CherryCollected;
        }
    }
}
