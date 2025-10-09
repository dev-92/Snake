using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class CherryCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/cherry.png";
        public CherryCollectable(Position2D position) : base(CherryCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Cherry.CHERRY_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.CherryCollected;
        }
    }
}
