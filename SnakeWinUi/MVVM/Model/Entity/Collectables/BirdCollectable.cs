using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class BirdCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\bird.png";
        private const int SCORE = 2;


        public BirdCollectable(Position2D position) : base(BirdCollectable.IMAGE_PATH, position)
        {

        }

        protected override void HandleCollected()
        {
            for(int i = 0; i < BirdCollectable.SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }
        }
    }
}
