using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

using SnakeWinUi.Controller;
using SnakeWinUi.MVVM.ViewModel;
using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.Services.UpdateService;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.Extensions;

using System.Collections.Generic;
using SnakeWinUi.MVVM.Model.Entity.Collectables;


namespace SnakeWinUi.MVVM.View
{
    /// <summary>
    /// Represents the game board UI and handles rendering of cells and the snake.
    /// Implements <see cref="IUpdateable"/> to update the board every game tick.
    /// </summary>
    public sealed partial class GameboardView : UserControl, IUpdateable
    {
        private SolidColorBrush _strokeColor { get; set; } = new(Colors.Pink);

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

            this.BuildBorderElements();
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
        private void BuildBorderElements()
        {
            foreach (CellViewModel cellViewModel in this.CellViewModels)
            {
                Border border = new()
                {
                    CornerRadius = new CornerRadius(Constants.CORNER_RADIUS),
                    BorderBrush = this._strokeColor,
                    BorderThickness = new Thickness(Constants.BORDER_THICKNESS)
                };

                border.SetBinding(Border.BackgroundProperty, new Microsoft.UI.Xaml.Data.Binding
                {
                    Path = new PropertyPath("BackgroundColor"),
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
            int headIndex = this.GetSnakeElementIndex(SnakeModel.Instance.Head);
            this.CellViewModels[headIndex].CellModel.CellStatus = CellStatus.Snake;

            if (SnakeModel.Instance.Tail.IsEmpty()) return;

            foreach (SnakeElement tailPiece in SnakeModel.Instance.Tail)
            {
                int currentTailIndex = this.GetSnakeElementIndex(tailPiece);
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
            SnakeElement lastTailElement = SnakeModel.Instance.Tail[SnakeModel.Instance.Tail.Count - 1];
            int lastTailIndex = this.GetSnakeElementIndex(lastTailElement);
            this.CellViewModels[lastTailIndex].CellModel.CellStatus = CellStatus.Empty;
        }

        /// <summary>
        /// Calculates the index of a given snake element in the cell list.
        /// </summary>
        /// <param name="snakeElement">The snake element to find.</param>
        /// <returns>The index of the element in <see cref="CellViewModels"/>.</returns>
        private int GetSnakeElementIndex(SnakeElement snakeElement)
        {
            return snakeElement.CurrentPosition.Y * GameSettings.SideLength + snakeElement.CurrentPosition.X;
        }

        public void DrawCollectableItem(CollectableItem item)                                       // Hier weitermachen 
        {
            int itemIndex = item.Position.Y * GameSettings.SideLength + item.Position.X;
            this.CellViewModels[itemIndex].CellModel.CellStatus = CellStatus.CollectAble;
            (this.GameboardGrid.Children[itemIndex] as Border).Background = item.ImageBrush;
        }

        public void EraseCollectableItem(CollectableItem item)
        {
            int itemIndex = item.Position.Y * GameSettings.SideLength + item.Position.X;            // Funktion mit GetSnakeElementIndex zusammenlegen
            this.CellViewModels[itemIndex].CellModel.CellStatus = CellStatus.Empty;
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