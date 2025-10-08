using SnakeWinUi.MVVM.Model.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class BombCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\bomb.png";

        public BombCollectable(Position2D position) : base(BombCollectable.IMAGE_PATH, position)
        {

        }

        protected override void HandleCollected()
        {
           
        }
    }
}
