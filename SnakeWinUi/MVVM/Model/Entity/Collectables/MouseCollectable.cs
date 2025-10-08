using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class MouseCollectable : CollectableItem
    {
        private const string IMAGE_PATH = @"C:\Users\ty-ro\source\repos\Snake\SnakeWinUi\Assets\Collectables\mouse.png";
        private const int SCORE = 1;

        public MouseCollectable(Position2D position) : base(MouseCollectable.IMAGE_PATH, position)
        {

        }

        protected override void HandleCollected()
        {
            for (int i = 0; i < MouseCollectable.SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }
        }

    }
}
