using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class RabbitCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\rabbit.png";
        private const int SCORE = 3;

        public RabbitCollectable(Position2D position) : base(RabbitCollectable.IMAGE_PATH, position)
        {

        }

        protected override void HandleCollected()
        {
            for (int i = 0; i < RabbitCollectable.SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }
        }
    }
}
