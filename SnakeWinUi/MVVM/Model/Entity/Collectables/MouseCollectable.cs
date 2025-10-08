using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Services.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class MouseCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/mouse.png";

        public MouseCollectable(Position2D position) : base(MouseCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Mouse.MOUSE_LIFETIME_MILLIS;
        }

        protected override void HandleCollected()
        {
            for (int i = 0; i < CollectableConfig.Mouse.SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }

            SoundManager.Instance.PlayEffect(SoundEffectType.CollectedItem);
        }

    }
}
