using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Services.Audio;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class RabbitCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/rabbit.png";

        public RabbitCollectable(Position2D position) : base(RabbitCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Rabbit.RABBIT_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CollectedItem;
        }

        protected override void HandleCollected()
        {
            for (int i = 0; i < CollectableConfig.Rabbit.BASE_SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }
        }
    }
}
