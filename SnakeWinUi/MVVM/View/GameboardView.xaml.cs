using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using SnakeWinUi.Controller;
using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.Services.UpdateService;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.Extensions;

using System.Collections.Generic;
using SnakeWinUi.MVVM.Model.Entity.Collectables;
using SnakeWinUi.MVVM.Model.Entity;


namespace SnakeWinUi.MVVM.View
{
    /// <summary>
    /// Represents the game board UI and handles rendering of cells and the snake.
    /// Implements <see cref="IUpdateable"/> to update the board every game tick.
    /// </summary>
    public sealed partial class GameboardView : UserControl, IUpdateable
    {
        public List<CellViewModel> CellViewModels { get; set; } = new();

        private static GameboardView? _instance;
        public static GameboardView Instance
        {
            get
            {
                if (GameboardView._instance == null)
                {
                    GameboardView._instance = new GameboardView();
                }

                return GameboardView._instance;
            }
        }

        private GameboardView()
        {
            this.InitializeComponent();

            this.BuildGameboardStructure();
            this.BuildGameboardGrid();

            this.BuildAndAppendBorderElementsToGrid();
            this.RegisterAtUpdateComposite();
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
                    this.CellViewModels.Add(new CellViewModel(position));
                }
            }
        }

        /// <summary>
        /// Builds the visual grid layout for the game board using RowDefinitions and ColumnDefinitions.
        /// </summary>
        private void BuildGameboardGrid()
        {
            for (int i = 0; i < GameSettings.SideLength; i++)
            {
                this.GameboardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Constants.CELL_HEIGHT) });
                this.GameboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Constants.CELL_WIDTH) });
            }
        }

        /// <summary>
        /// Adds border elements to each cell and binds their background to the respective <see cref="CellViewModel"/>.
        /// </summary>
        private void BuildAndAppendBorderElementsToGrid()
        {
            foreach (CellViewModel cellViewModel in this.CellViewModels)
            {
                Border border = new()
                {
                    CornerRadius = new CornerRadius(Constants.CORNER_RADIUS),
                    BorderThickness = new Thickness(Constants.BORDER_THICKNESS)
                };

                border.SetBinding(Border.BackgroundProperty, new Microsoft.UI.Xaml.Data.Binding
                {
                    Path = new PropertyPath("BackgroundBrush"),
                    Source = cellViewModel,
                    Mode = Microsoft.UI.Xaml.Data.BindingMode.TwoWay
                });

                int borderPosX = (int)cellViewModel.CellModel.Position.X;
                int borderPosY = (int)cellViewModel.CellModel.Position.Y;

                Grid.SetColumn(border, borderPosX);
                Grid.SetRow(border, borderPosY);

                this.GameboardGrid.Children.Add(border);
            }
        }

        /// <summary>
        /// Draws the snake on the board by setting the relevant cells to <see cref="CellStatus.Snake"/>.
        /// </summary>
        private void DrawSnake()
        {
            int headIndex = this.GetCellIndex(SnakeModel.Instance.Head.CurrentPosition);
            this.CellViewModels[headIndex].CellModel.CellStatus = CellStatus.Snake;

            if (SnakeModel.Instance.Tail.IsEmpty()) return;

            foreach (SnakeElement tailPiece in SnakeModel.Instance.Tail)
            {
                int currentTailIndex = this.GetCellIndex(tailPiece.CurrentPosition);
                this.CellViewModels[currentTailIndex].CellModel.CellStatus = CellStatus.Snake;
            }

            this.ClearLastTailCell();
        }

        /// <summary>
        /// Clears the last tail cell by setting its status to <see cref="CellStatus.Empty"/>.
        /// This avoids a full board cleanup each tick.
        /// </summary>
        private void ClearLastTailCell()
        {
            SnakeElement lastElementOfTail = SnakeModel.Instance.Tail[SnakeModel.Instance.Tail.Count - 1];

            int lastTailIndex = this.GetCellIndex(lastElementOfTail.CurrentPosition);
            CellModel tailCellModel = this.CellViewModels[lastTailIndex].CellModel;

            tailCellModel.CellStatus = CellStatus.Empty;
        }

        public void DrawCollectableItem(CollectableItem item)
        {
            int itemIndex = this.GetCellIndex(item.Position);
            CellModel itemCellModel = this.CellViewModels[itemIndex].CellModel;

            itemCellModel.CollectableItem = item;
        }

        public void EraseCollectableItem(CollectableItem item)
        {
            int itemIndex = this.GetCellIndex(item.Position);
            CellModel itemCellModel = this.CellViewModels[itemIndex].CellModel;

            itemCellModel.CollectableItem = null;
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

        /// <summary>
        /// Updates the game board by drawing the snake in its current position.
        /// </summary>
        public void Update()
        {
            this.DrawSnake();
        }

        /// <summary>
        /// Registers this view in the <see cref="GameManager"/> update loop.
        /// </summary>
        public void RegisterAtUpdateComposite()
        {
            GameManager.Instance.AddToUpdateGroup(this);
        }
    }
}