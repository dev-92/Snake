using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using SnakeWinUi.Controller;
using SnakeWinUi.MVVM.Model;
using SnakeWinUi.MVVM.ViewModel;
using SnakeWinUi.UpdateService;
using SnakeWinUi.Config;
using System;
using System.Collections.Generic;

namespace SnakeWinUi.MVVM.View
{
    public sealed partial class GameboardView : UserControl, IUpdateEntity
    {
        public int SideLength { get; set; }

        private SolidColorBrush _strokeColor { get; set; } = new SolidColorBrush(Colors.Pink);

        public List<CellViewModel> CellViewModels { get; set; } = new List<CellViewModel>();
        public Dictionary<Position2D, CellModel> CellLookup { get; set; } = new Dictionary<Position2D, CellModel>();

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

            this.SetSideLength();

            this.BuildGameboardStructure();
            this.BuildGameboardGrid();

            this.BuildBorderElements();
            this.RegisterAtUpdateGroup();
        }

        private void BuildGameboardStructure()
        {
            for (int row = 0; row < this.SideLength; row++)
            {
                for (int col = 0; col < this.SideLength; col++)
                {
                    Position2D position = new Position2D(row, col);
                    this.CellViewModels.Add(new CellViewModel(position));

                    CellModel cellModel = new CellModel(position);
                    this.CellLookup[position] = cellModel;
                }
            }
        }

        private void BuildGameboardGrid()
        {
            for (int i = 0; i < this.SideLength; i++)
            {
                this.GameboardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Constants.CELL_HEIGHT) });
                this.GameboardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Constants.CELL_WIDTH) });
            }
        }

        private void BuildBorderElements()
        {
            foreach (CellViewModel cellViewModel in this.CellViewModels)
            {
                Border border = new Border
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

        private void SetSideLength()
        {
            this.SideLength = (int)Math.Sqrt(Constants.CELL_AMOUNT);
        }

        private void ClearBoard()
        {
            foreach (CellViewModel cellViewModel in this.CellViewModels)
            {
                cellViewModel.CellModel.CellStatus = CellModel.Status.Empty;
            }
        }

        private void DrawSnake()
        {
            int headIndex = SnakeModel.Instance.Head.X * GameboardView.Instance.SideLength + SnakeModel.Instance.Head.Y;
            GameboardView.Instance.CellViewModels[headIndex].CellModel.CellStatus = CellModel.Status.Snake;

            foreach (Position2D tailPiece in SnakeModel.Instance.Tail)
            {
                int currentTailPieceIndex = tailPiece.X * GameboardView.Instance.SideLength + tailPiece.Y;
                GameboardView.Instance.CellViewModels[currentTailPieceIndex].CellModel.CellStatus = CellModel.Status.Snake;
            }
        }

        private void HandleWallCollision(int headIndex) // ToDo
        {
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
