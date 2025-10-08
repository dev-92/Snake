using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class CollectableItemFactory
    {
        public static CollectableItem CreateRandomCollectableItem(Position2D freePosition)
        {
            int possibleOptions = Enum.GetValues(typeof(CollectableItems)).Length;
            
            return new Random().Next(0, possibleOptions) switch
            {
                (int)CollectableItems.Apple     => new AppleCollectable(freePosition),
                (int)CollectableItems.Bird      => new BirdCollectable(freePosition),
                (int)CollectableItems.Bomb      => new BombCollectable(freePosition),
                (int)CollectableItems.Cherry    => new CherryCollectable(freePosition),
                (int)CollectableItems.Mouse     => new MouseCollectable(freePosition),
                (int)CollectableItems.Rabbit    => new RabbitCollectable(freePosition),
                _                               => new MouseCollectable(freePosition),
            };       
            
        }

    }
}
