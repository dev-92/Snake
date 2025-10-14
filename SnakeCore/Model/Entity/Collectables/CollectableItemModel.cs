using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    public abstract class CollectableItemModel
    {
        public SoundEffectType SoundEffect { get; protected set; }

        public Position2D Position {  get; set; }

        public double LifetimeMillis { get; set; } = 0;

        private DateTime _spawnTime;

        public CollectableItemModel(Position2D position)
        {
            this.Position = position;
            this._spawnTime = DateTime.Now;
        } 

        public bool IsExpired()
        {
            return (DateTime.Now - this._spawnTime).TotalMilliseconds >= this.LifetimeMillis;
        }
    }
}
