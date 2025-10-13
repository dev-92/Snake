using SnakeCore.Model.Entity.Collectables;

namespace SnakeCore.Services
{
    public interface ICollectableRenderService
    {
        public void RenderCollectableItem(CollectableItemModel newItem);
        public void EraseCollectableItem(CollectableItemModel item);
    }
}
