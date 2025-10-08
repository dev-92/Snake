using SnakeWinUi.MVVM.Model.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class RabbitCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\rabbit.png";

        public RabbitCollectable(Position2D position) : base(RabbitCollectable.IMAGE_PATH, position)
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
