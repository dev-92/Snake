using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    public abstract class CollectableItem
    {
        protected readonly string _imagePath;
        public SoundEffectType SoundEffect { get; protected set; }

        public Position2D Position {  get; set; }
        public ImageBrush? ImageBrush { get; set; }

        public double LifetimeMillis { get; set; } = 0;
        private DateTime _spawnTime;

        public CollectableItem(string imagePath, Position2D position)
        {
            this._imagePath = imagePath;
            this.Position = position;

            this._spawnTime = DateTime.Now;
            this.ImageBrush = this.GetImageBrush();
        }

        protected ImageBrush GetImageBrush()
        {
            BitmapImage bitmapImage = new(new Uri(this._imagePath));
            return new ImageBrush
            {
                ImageSource = bitmapImage,
                Stretch = Stretch.Uniform,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center
            };
        }   

        public bool IsExpired()
        {
            return (DateTime.Now - this._spawnTime).TotalMilliseconds >= this.LifetimeMillis;
        }

        public void OnCollected()
        {
            this.HandleCollected();
        }

        protected abstract void HandleCollected();
    }
}
