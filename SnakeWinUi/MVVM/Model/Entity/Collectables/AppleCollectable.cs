using SnakeWinUi.Config;
using SnakeWinUi.Controller;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;
using System.Runtime.CompilerServices;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class AppleCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/apple.png";

        public AppleCollectable(Position2D position) : base(AppleCollectable.IMAGE_PATH, position)
        {
          this.LifetimeMillis = CollectableConfig.Apple.APPLE_LIFETIME_MILLIS;
        }

        protected override void HandleCollected()
        {
            GameSettings.UpdateSpeedMillis = (int)(GameSettings.UpdateSpeedMillis * CollectableConfig.Apple.APPLE_SPEED_FACTOR);
        }

    }
}
