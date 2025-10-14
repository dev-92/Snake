using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Extensions;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;
using SnakeCore.Services.UpdateService;

namespace SnakeCore.Model.Entity
{
    public class GameboardModel : IUpdateable
    {
        public List<CellModel> Cells {  get; set; } = new List<CellModel>();
        private SnakeModel _snake { get; set; }

        public GameboardModel(SnakeModel snake) 
        { 
            this._snake = snake;
            this.BuildGameboardStructure();
        }

        /// <summary>
        /// Creates the internal data structure for all cells on the game board.
        /// </summary>
        private void BuildGameboardStructure()
        {
            for (int row = 0; row < GameSettings.SideLength; row++)
            {
                for (int col = 0; col < GameSettings.SideLength; col++)
                {
                    Position2D position = new(col, row);
                    this.Cells.Add(new CellModel(position));
                }
            }
        }

        public void PlaceCollectableItem(CollectableItemModel item)
        {
            int itemIndex = this.GetCellIndex(item.Position);
            CellModel itemCellModel = this.Cells[itemIndex];

            itemCellModel.CollectableItem = item;
        }

        public void RemoveCollectableItem(CollectableItemModel item)
        {
            int itemIndex = this.GetCellIndex(item.Position);
            CellModel itemCellModel = this.Cells[itemIndex];

            itemCellModel.CollectableItem = null;
        }

        /// <summary>
        /// Draws the snake on the board by setting the relevant cells to <see cref="CellStatus.Snake"/>.
        /// </summary>
        private void UpdateSnakeCells()
        {
            int headIndex = this.GetCellIndex(this._snake.Head.CurrentPosition);
            this.Cells[headIndex].CellStatus = CellStatus.Snake;

            if (this._snake.Tail.IsEmpty()) return;

            foreach (SnakeElement tailPiece in this._snake.Tail)
            {
                int currentTailIndex = this.GetCellIndex(tailPiece.CurrentPosition);
                this.Cells[currentTailIndex].CellStatus = CellStatus.Snake;
            }

            this.ClearLastTailCell();
        }

        /// <summary>
        /// Clears the last tail cell by setting its status to <see cref="CellStatus.Empty"/>.
        /// This avoids a full board cleanup each tick.
        /// </summary>
        private void ClearLastTailCell()
        {
            SnakeElement lastElementOfTail = this._snake.Tail[this._snake.Tail.Count - 1];

            int lastTailIndex = this.GetCellIndex(lastElementOfTail.CurrentPosition);
            CellModel tailCellModel = this.Cells[lastTailIndex];

            tailCellModel.CellStatus = CellStatus.Empty;
        }

        /// <summary>
        /// Calculates the index of a given snake element in the cell list.
        /// </summary>
        /// <param name="snakeElement">The snake element to find.</param>
        /// <returns>The index of the element in <see cref="CellViewModels"/>.</returns>
        private int GetCellIndex(Position2D position)
        {
            return position.Y * GameSettings.SideLength + position.X;
        }

        public void Update()
        {
            this.UpdateSnakeCells();
        }
    }
}
