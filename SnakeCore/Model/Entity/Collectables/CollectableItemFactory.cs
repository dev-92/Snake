using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Model.Entity.Collectables
{
    /// <summary>
    /// Factory class responsible for creating instances of collectible items in the game.
    /// It generates a random <see cref="CollectableItemModel"/> based on available types.
    /// </summary>
    internal class CollectableItemFactory
    {
        /// <summary>
        /// Creates a random collectible item at the specified free position on the game field.
        /// The type of item is chosen randomly from the <see cref="CollectableItemType"/> enum.
        /// </summary>
        /// <param name="freePosition">
        /// The position on the game field where the collectible item should be spawned.
        /// </param>
        /// <returns>
        /// A new instance of a class derived from <see cref="CollectableItemModel"/>.
        /// </returns>
        public static CollectableItemModel CreateRandomCollectableItem(Position2D freePosition)
        {
            int possibleOptions = Enum.GetValues(typeof(CollectableItemType)).Length;

            return new Random().Next(0, possibleOptions) switch
            {
                (int)CollectableItemType.Apple  => new AppleCollectable(freePosition),
                (int)CollectableItemType.Duck   => new DuckCollectable(freePosition),
                (int)CollectableItemType.Bomb   => new BombCollectable(freePosition),
                (int)CollectableItemType.Cherry => new CherryCollectable(freePosition),
                (int)CollectableItemType.Mouse  => new MouseCollectable(freePosition),
                (int)CollectableItemType.Rabbit => new RabbitCollectable(freePosition),
                _                               => new MouseCollectable(freePosition),
            };
        }
    }
}
