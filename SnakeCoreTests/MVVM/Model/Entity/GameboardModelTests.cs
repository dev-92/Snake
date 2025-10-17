
using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;

namespace SnakeCoreTests.MVVM.Model.Entity
{
    public class GameboardModelTests
    {
        private SnakeModel _snakeModel { get; set; }
        private GameboardModel _gameboardModel { get; set; }
        private const int CELL_AMOUNT = 144;
        private readonly int _sideLength;

        public GameboardModelTests()
        {
            GameSettings.CellAmount = GameboardModelTests.CELL_AMOUNT;
            this._sideLength = GameSettings.SideLength;

            this._snakeModel = new SnakeModel();    
            this._gameboardModel = new GameboardModel(this._snakeModel);
        }

        [Fact]
        public void Constructor_TotalCellAmountIs144_ThereWillBe12RowsAndColumns()
        {
            // Arrange
            int sideLength = (int)Math.Sqrt(GameboardModelTests.CELL_AMOUNT);
            Position2D expectedPositionOfLastCell = new Position2D(sideLength - 1, sideLength - 1);

            // Act
            CellModel lastCell = this._gameboardModel.Cells[^1];

            // Assert
            Assert.Equal(GameboardModelTests.CELL_AMOUNT, this._gameboardModel.Cells.Count);
            Assert.Equal(expectedPositionOfLastCell, lastCell.Position);
        }

        [Fact]
        public void PlaceCollectableItem_ItemPositionIsPositionZero_ItemIsAddedToSpecificCell()
        {
            // Arrange
            CollectableItemModel collectableItem = CollectableItemFactory.CreateRandomCollectableItem(Position2D.Zero);

            // Act
            this._gameboardModel.PlaceCollectableItem(collectableItem);

            // Assert
            Assert.Equal(this._gameboardModel.Cells[0].CollectableItem, collectableItem);
        }

        [Fact]
        public void RemoveCollectableItem_ItemPositionIsPositionZero_ItemIsRemovedFromCell()
        {
            // Arrange
            CollectableItemModel collectableItem = CollectableItemFactory.CreateRandomCollectableItem(Position2D.Zero);
            this._gameboardModel.Cells[0].CollectableItem = collectableItem;

            // Act
            this._gameboardModel.RemoveCollectableItem(collectableItem);

            // Assert
            Assert.Null(this._gameboardModel.Cells[0].CollectableItem);
        }

        [Fact]
        public void UpdateSnakeCells_HeadPositionIsPositionZero_FirstCellIsCellStatusSnake()
        {
            // Arrange        
            this._snakeModel.Head.CurrentPosition = new Position2D(this._sideLength - 1, this._sideLength - 1);
            int headIndex = GameboardModelTests.CELL_AMOUNT - 1;
            CellStatus statusBefore = this._gameboardModel.Cells[headIndex].CellStatus;

            // Act
            this._gameboardModel.Update();

            CellStatus statusAfter = this._gameboardModel.Cells[headIndex].CellStatus;

            // Assert

            Assert.NotEqual(statusBefore, statusAfter);
            Assert.Equal(CellStatus.Snake, statusAfter);
        }

        [Fact]
        public void UpdateSnakeCells_TailPositionsAreUpdated_LastTailCellIsCleared()
        {
            // Arrange
            this._snakeModel.Head.CurrentPosition = new Position2D(0, 0); 
            this._snakeModel.Tail[0].CurrentPosition = new Position2D(5, 5); 
            this._snakeModel.Tail[1].CurrentPosition = new Position2D(6, 5); 
            this._snakeModel.Tail[2].CurrentPosition = new Position2D(7, 5); // empty

            int firstTailIndex = this._gameboardModel.Cells.FindIndex(c => c.Position == this._snakeModel.Tail[0].CurrentPosition);
            int secondTailIndex = this._gameboardModel.Cells.FindIndex(c => c.Position == this._snakeModel.Tail[1].CurrentPosition);
            int lastTailIndex = this._gameboardModel.Cells.FindIndex(c => c.Position == this._snakeModel.Tail[2].CurrentPosition);

            CellStatus beforeFirstTail = this._gameboardModel.Cells[firstTailIndex].CellStatus;
            CellStatus beforeSecondTail = this._gameboardModel.Cells[secondTailIndex].CellStatus;
            CellStatus beforeLastTail = this._gameboardModel.Cells[lastTailIndex].CellStatus;

            // Act
            this._gameboardModel.Update();

            CellStatus afterFirstTail = this._gameboardModel.Cells[firstTailIndex].CellStatus;
            CellStatus afterSecondTail = this._gameboardModel.Cells[secondTailIndex].CellStatus;
            CellStatus afterLastTail = this._gameboardModel.Cells[lastTailIndex].CellStatus;

            // Assert
            Assert.Equal(CellStatus.Snake, afterFirstTail);    
            Assert.Equal(CellStatus.Snake, afterSecondTail);  
            Assert.Equal(CellStatus.Empty, afterLastTail);     
        }
    }
}
