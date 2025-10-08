using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Services.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class BombCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/bomb.png";

        public BombCollectable(Position2D position) : base(BombCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Bomb.BOMB_LIFETIME_MILLIS;
        }

        protected override void HandleCollected()
        {
            SoundManager.Instance.PlayEffect(SoundEffectType.BombCollected);

        }
    }
}
