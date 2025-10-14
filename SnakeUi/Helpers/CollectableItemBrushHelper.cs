using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

using SnakeCore.Model.Entity.Collectables;

using SnakeUi.Config;
using System;

namespace SnakeUi.Helpers
{
    /// <summary>
    /// Provides helper methods to generate <see cref="ImageBrush"/> instances for collectible items.
    /// </summary>
    internal static class CollectableItemBrushHelper
    {
        /// <summary>
        /// Returns the file path to the image associated with the specified collectible item.
        /// </summary>
        private static string GetImagePath(CollectableItemModel item)
        {
            return item switch
            {
                AppleCollectable    => ViewModelConstants.APPLE_IMAGE_PATH,
                BombCollectable     => ViewModelConstants.BOMB_IMAGE_PATH,
                CherryCollectable   => ViewModelConstants.CHERRY_IMAGE_PATH,
                DuckCollectable     => ViewModelConstants.DUCK_IMAGE_PATH,
                RabbitCollectable   => ViewModelConstants.RABBIT_IMAGE_PATH,
                MouseCollectable    => ViewModelConstants.MOUSE_IMAGE_PATH,
                _                   => ViewModelConstants.MOUSE_IMAGE_PATH
            };
        }

        /// <summary>
        /// Returns an <see cref="ImageBrush"/> for the specified collectible item.
        /// </summary>
        /// <param name="item">The collectible item to generate the brush for.</param>
        public static ImageBrush GetImageBrush(CollectableItemModel item)
        {
            BitmapImage bitmapImage = new(new Uri(GetImagePath(item)));

            return new ImageBrush
            {
                ImageSource = bitmapImage,
                Stretch = Stretch.Uniform,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center
            };
        }
    }
}
