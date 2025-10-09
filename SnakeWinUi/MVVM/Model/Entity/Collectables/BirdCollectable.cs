using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.Services.Audio;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    internal class DuckCollectable : CollectableItem
    {
        private const string IMAGE_PATH = "ms-appx:///Assets/Collectables/duck.png";

        public DuckCollectable(Position2D position) : base(DuckCollectable.IMAGE_PATH, position)
        {
            this.LifetimeMillis = CollectableConfig.Duck.Duck_LIFETIME_MILLIS;
            this.SoundEffect = SoundEffectType.DuckCollected;
        }

        protected override void HandleCollected()
        {
            for(int i = 0; i < CollectableConfig.Duck.BASE_SCORE; i++)
            {
                SnakeModel.Instance.ExtendTail();
            }
        }
    }
}
