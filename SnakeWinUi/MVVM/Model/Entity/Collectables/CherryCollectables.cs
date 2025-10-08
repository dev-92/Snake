using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class CherryCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\cherry.png";

        public CherryCollectable(Position2D position) : base(CherryCollectable.IMAGE_PATH, position)
        {
           
        }

        protected override void HandleCollected()
        {
            throw new NotImplementedException();
        }

        protected override bool WasCollected()
        {
            throw new NotImplementedException();
        }
    }
}
