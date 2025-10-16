using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SnakeCore.Config;
using SnakeCore.Model.Entity;
using SnakeUi.Config;

using System.Collections.Generic;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the game board UI and handles rendering of cells and the snake.
    /// Implements <see cref="IUpdateable"/> to update the board every game tick.
    /// </summary>
    public sealed partial class GameboardView : UserControl
    {
        private List<CellViewModel> _cellViewModels { get; set; } = new List<CellViewModel>();

        public GameboardView(List<CellModel> cells)
        {
            this.InitializeComponent();
            this.InitializeCellViewModels(cells);    

            this.BuildGameboardGrid();
            this.BuildAndAppendBorderElementsToGrid();
        }

        /// <summary>
        /// Initializes the collection of CellViewModels from the game board's cells.
        /// </summary>
        private void InitializeCellViewModels(List<CellModel> cellModels)
        {
            foreach (CellModel cellModel in cellModels)
            {
                this._cellViewModels.Add(new CellViewModel(cellModel));
            }
        }

        /// <summary>
        /// Builds the visual grid layout for the game board using RowDefinitions and ColumnDefinitions.
        /// </summary>
        private void BuildGameboardGrid()
        {
            for (int i = 0; i < GameSettings.SideLength; i++)
            {
                this.GameboardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(UiConstants.CELL_HEIGHT) });
                this.GameboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(UiConstants.CELL_WIDTH) });
            }
        }

        /// <summary>
        /// Adds border elements to each cell and binds their background to the respective <see cref="CellViewModel"/>.
        /// </summary>
        private void BuildAndAppendBorderElementsToGrid()
        {
            foreach (CellViewModel cellViewModel in this._cellViewModels)
            {
                Border border = new()
                {
                    CornerRadius = new CornerRadius(UiConstants.CORNER_RADIUS),
                    BorderThickness = new Thickness(UiConstants.BORDER_THICKNESS),
                    Margin = new Thickness(UiConstants.BORDER_MARGIN)
                };
                
                border.SetBinding(Border.BackgroundProperty, new Microsoft.UI.Xaml.Data.Binding
                {
                    Path = new PropertyPath("BackgroundBrush"),
                    Source = cellViewModel,
                    Mode = Microsoft.UI.Xaml.Data.BindingMode.TwoWay,                  
                });
                
                int borderPosX = (int)cellViewModel.CellModel.Position.X;
                int borderPosY = (int)cellViewModel.CellModel.Position.Y;

                Grid.SetColumn(border, borderPosX);
                Grid.SetRow(border, borderPosY);

                this.GameboardGrid.Children.Add(border);
            }
        }

    }
}