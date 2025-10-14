using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    /// <summary>
    /// Abstract base class for all collectible items in the game.
    /// Provides lifetime tracking and common functionality for derived collectible items.
    /// </summary>
    public abstract class CollectableItemModel
    {
        public SoundEffectType SoundEffect { get; protected set; }

        public Position2D Position { get; set; }

        public double LifetimeMillis { get; set; } = 0;

        private DateTime _spawnTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectableItemModel"/> class
        /// at the specified position.
        /// </summary>
        /// <param name="position">The spawn position of the collectible item.</param>
        public CollectableItemModel(Position2D position)
        {
            this.Position = position;
            this._spawnTime = DateTime.Now;
        }

        /// <summary>
        /// Determines whether the collectible item has expired based on its lifetime.
        /// </summary>
        /// <returns><c>true</c> if the item's lifetime has elapsed; otherwise, <c>false</c>.</returns>
        public bool IsExpired()
        {
            return (DateTime.Now - this._spawnTime).TotalMilliseconds >= this.LifetimeMillis;
        }
    }
}
