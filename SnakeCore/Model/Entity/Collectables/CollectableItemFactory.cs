using SnakeCore.Enums;
using SnakeCore.Model.ValueObject;
using SnakeCore.MVVM.Model.Entity.Collectables;

namespace SnakeCore.Model.Entity.Collectables
{
    internal class CollectableItemFactory
    {
        public static CollectableItemModel CreateRandomCollectableItem(Position2D freePosition)
        {
            int possibleOptions = Enum.GetValues(typeof(CollectableItemType)).Length;
            
            return new Random().Next(0, possibleOptions) switch
            {
                (int)CollectableItemType.Apple     => new AppleCollectable(freePosition),
                (int)CollectableItemType.Duck      => new DuckCollectable(freePosition),
                (int)CollectableItemType.Bomb      => new BombCollectable(freePosition),
                (int)CollectableItemType.Cherry    => new CherryCollectable(freePosition),
                (int)CollectableItemType.Mouse     => new MouseCollectable(freePosition),
                (int)CollectableItemType.Rabbit    => new RabbitCollectable(freePosition),
                _                               => new MouseCollectable(freePosition),
            };       
            
        }

    }
}
