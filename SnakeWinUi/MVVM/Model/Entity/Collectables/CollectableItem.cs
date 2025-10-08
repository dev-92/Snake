using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using SnakeWinUi.MVVM.Model.ValueObject;
using System;

namespace SnakeWinUi.MVVM.Model.Entity.Collectables
{
    public abstract class CollectableItem
    {
        protected readonly string _imagePath;

        public Position2D Position {  get; set; }
        public ImageBrush? ImageBrush { get; set; } 
        
        public CollectableItem(string imagePath, Position2D position)
        {
            this._imagePath = imagePath;
            this.Position = position;
            this.ImageBrush = this.GetImageBrush();
        }

        protected ImageBrush GetImageBrush()
        {
            BitmapImage bitmapImage = new(new Uri(this._imagePath));
            return new ImageBrush
            {
                ImageSource = bitmapImage,
                Stretch = Stretch.UniformToFill
            };
        }
        protected abstract bool WasCollected();

        protected abstract void HandleCollected();
    }
}
