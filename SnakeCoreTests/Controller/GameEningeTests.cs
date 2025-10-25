using SnakeCore.Config;
using SnakeCore.Controller;
using SnakeCore.Enums;
using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;

namespace SnakeCoreTests;

public class GameEningeTests
{
    private GameEngine _gameEngine;
    private SnakeModel _snakeModel;

    public GameEningeTests()
    {
        this._gameEngine = new GameEngine();
        this._snakeModel = this._gameEngine.Snake;
    }

    [Fact]
    public void Run_EngineStarted_SetGameStateAndStartMusicLoop()
    {
        // Arrange

        // Act
        this._gameEngine.Run();

        // Assert
        Assert.Equal(GameState.Running, this._gameEngine.GameState);
    }

    [Fact]
    public void Stop_EngineStopped_SetGameStateAndStopMusicLoop()
    {
        // Arrange

        // Act
        this._gameEngine.Stop();

        // Assert 
        Assert.Equal(GameState.Paused, this._gameEngine.GameState);
    }

    #region Update => UpdateCollectables 

    [Fact]
    public void Update_LessItemsExistThanShould_CollectableShouldBeSpawned()
    {
        // Arrange
        List<CellModel> cellsWithCollectableItem_AtStart = this.GetCellModels();

        // Act
        this._gameEngine.Update();
        List<CellModel> cellsWithCollectableItem_AfterUpdate = this.GetCellModels();

        // Assert
        Assert.Empty(cellsWithCollectableItem_AtStart);
        Assert.Equal(CollectableConfig.MAX_ITEMS, cellsWithCollectableItem_AfterUpdate.Count);
    }

    [Fact]
    public void Update_EnoughItemsExists_CollectableItemsAreTheSame()
    {
        // Arrange
        this._gameEngine.Update();
        List<CellModel> cellsWithCollectableItem_AtStart = this.GetCellModels();

        // Act
        this._gameEngine.Update();
        List<CellModel> cellsWithCollectableItem_AfterUpdate = this.GetCellModels();

        // Assert
        Assert.Equal(CollectableConfig.MAX_ITEMS, cellsWithCollectableItem_AtStart.Count);
        Assert.Equal(CollectableConfig.MAX_ITEMS, cellsWithCollectableItem_AfterUpdate.Count);
    }

    [Fact]
    public void Update_SnakeCollectsItem_ItemIsRemovedFromList()
    {
        // Arrange
        this._gameEngine.Update();

        CollectableItemModel itemToCollect = this.GetCellModels()[0].CollectableItem;
        Position2D itemToCollectPos = itemToCollect.Position;

        this._snakeModel.Head.CurrentPosition = itemToCollectPos;
        List<CollectableItemModel?> itemsBefore = this.GetCollectableItems();

        // Act
        this._gameEngine.Update();
        List<CollectableItemModel?> itemsAfter = this.GetCollectableItems();

        // Assert
        Assert.Contains(itemToCollect, itemsBefore);
        Assert.DoesNotContain(itemToCollect, itemsAfter);
    }

    [Fact]
    public void Update_SnakeCollectsItem_MaxItemsRemainCorrect()
    {
        // Arrange
        this._gameEngine.Update();

        CollectableItemModel itemToCollect = this.GetCellModels()[0].CollectableItem;
        Position2D itemToCollectPos = itemToCollect.Position;
        this._snakeModel.Head.CurrentPosition = itemToCollectPos;

        // Act
        this._gameEngine.Update();
        List<CollectableItemModel?> itemsAfter = this.GetCollectableItems();

        // Assert
        Assert.Equal(CollectableConfig.MAX_ITEMS, itemsAfter.Count);
    }

    [Fact]
    public void Update_CollectableIsExpired_CollectableShouldBeRemoved()
    {
        // Arrange 


        // Act
        this._gameEngine.Update();

        // Assert

    }

    #endregion


    [Fact]
    public void Update_SnakeHasColliededWithItself_GameStops()
    {
        // Arrange
        this._snakeModel.Head.CurrentPosition = this._snakeModel.Tail[0].CurrentPosition;

        // Act
        this._gameEngine.Update();

        // Assert
        Assert.Equal(GameState.Paused, this._gameEngine.GameState);
    }
        
    #region Helpers
    private List<CellModel> GetCellModels()
    {
        return this._gameEngine.GameboardModel.Cells
               .Where(cell => cell.CellStatus == CellStatus.Collectable)
               .ToList();
    }

    private List<CollectableItemModel?> GetCollectableItems()
    {
        return this._gameEngine.GameboardModel.Cells
               .Where(cell => cell.CellStatus == CellStatus.Collectable)
               .Select(cell => cell.CollectableItem)
               .ToList();
    }
    #endregion
}
