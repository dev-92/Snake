

using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.ValueObject;

namespace SnakeCoreTests.MVVM.Model.Entity.Collectables
{
    public class CollectableItemFactoryTests
    {
        private Position2D _initialItemPosition { get; set; } = Position2D.Zero;
        private const int MAX_ITEMS_TO_TEST = 100;

        [Fact]
        public void CreateRandomCollectableItem_OneItemIsCreated_ItemIsNotNull()
        {
            // Arrange

            // Act
            CollectableItemModel collectableItem = CollectableItemFactory.CreateRandomCollectableItem(this._initialItemPosition);

            // Assert
            Assert.NotNull(collectableItem);
        }

        [Fact]
        public void CreateRandomCollectableItem_ManyItemsAreCreated_DifferentItemsAreCreated()
        {
            // Arrange
            HashSet<Type> collectableItems = new();
            
            // Act
            for(int i = 0; i < CollectableItemFactoryTests.MAX_ITEMS_TO_TEST; i++)
            {
                collectableItems.Add(CollectableItemFactory.CreateRandomCollectableItem(this._initialItemPosition).GetType());
            }

            // Assert
            Assert.True(collectableItems.Count > 1);
        }
    }
}
