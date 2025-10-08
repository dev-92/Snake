using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class AppleCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\apple.png";

        public AppleCollectable(Position2D position) : base(AppleCollectable.IMAGE_PATH, position)
        {
           
        }

        protected override void HandleCollected()
        {
            
        }


    }
}
