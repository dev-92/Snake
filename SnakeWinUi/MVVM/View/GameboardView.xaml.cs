using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SnakeWinUi.Controller;
using SnakeWinUi.MVVM.ViewModel;
using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using System.Collections.Generic;
using SnakeWinUi.Services.UpdateService;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.MVVM.Model.Entity;
using SnakeWinUi.MVVM.Model.Entity.Prey;

namespace SnakeWinUi.MVVM.View
{
    public sealed partial class GameboardView : UserControl, IUpdateEntity
    {
        private SolidColorBrush _strokeColor { get; set; } = new(Colors.Pink);

        public List<CellViewModel> CellViewModels { get; set; } = new();

        private Prey _prey { get; set; } = new();

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
            this.RegisterAtUpdateGroup();
        }

        private void BuildGameboardStructure()
        {
            for (int row = 0; row < GameSettings.SideLength; row++)
            {
                for (int col = 0; col < GameSettings.SideLength; col++)
                { 
                    Position2D position = new(row, col);
                    this.CellViewModels.Add(new CellViewModel(position));

                    CellModel cellModel = new(position);
                }
            }
        }

        private void BuildGameboardGrid()
        {
            for (int i = 0; i < GameSettings.SideLength; i++)
            {
                this.GameboardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Constants.CELL_HEIGHT) });
                this.GameboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Constants.CELL_WIDTH) });
            }
        }

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
                int borderPosY = (int)cellViewModel.CellModel.  Position.Y;

                Grid.SetRow(border, borderPosX);
                Grid.SetColumn(border, borderPosY);

                this.GameboardGrid.Children.Add(border);
            }
        }

        private void ClearBoard()
        {
            foreach (CellViewModel cellViewModel in this.CellViewModels)
            {
                cellViewModel.CellModel.CellStatus = CellStatus.Empty;
            }
        }

        private void DrawSnake()
        {
            int headIndex = this.GetHeadIndex();
            this.CellViewModels[headIndex].CellModel.CellStatus = CellStatus.Snake;

            foreach (Position2D tailPiece in SnakeModel.Instance.Tail)
            {
                int currentTailPieceIndex = tailPiece.X * GameSettings.SideLength + tailPiece.Y;
                this.CellViewModels[currentTailPieceIndex].CellModel.CellStatus = CellStatus.Snake;
            }
        }

        private void DrawPrey()
        {

        }

        private int GetHeadIndex()
        {
            return SnakeModel.Instance.Head.Y * GameSettings.SideLength + SnakeModel.Instance.Head.X;
        }

        public void Update()
        {
            this.ClearBoard();
            this.DrawSnake();
        }

        public void RegisterAtUpdateGroup()
        {
            GameManager.Instance.AddToUpdateGroup(this);
        }
    }
}
