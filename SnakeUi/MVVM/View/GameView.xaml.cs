using Microsoft.UI.Xaml.Controls;
using SnakeCore.Model.Entity;
using System.Collections.Generic;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the main game page, hosting the game board and handling keyboard input.
    /// Implements a singleton pattern to ensure only one instance exists.
    /// </summary>
    public sealed partial class GameView : Page
    {
        private GameboardView _gameboardView { get; set;}
        private InfoboardView _infoboardView { get; set; } = new InfoboardView();

        public GameView(List<CellModel> cellModels)
        {
            this.InitializeComponent();

            this._gameboardView = new GameboardView(cellModels);

            this.AddGameboardTo(this.UiLayout);
            this.AddInfoboardTo(this.UiLayout);

            this.Content = this.UiLayout;
            this.UiLayout.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
        }

        private void AddGameboardTo(Grid uiLayout)
        {
            Grid.SetRow(this._gameboardView, 1);
            Grid.SetColumn(this._gameboardView, 0);

            uiLayout.Children.Add(this._gameboardView);
        }

        private void AddInfoboardTo(Grid uiLayout)
        {
            Grid.SetRow(this._infoboardView, 0);
            Grid.SetColumn(this._infoboardView, 0);

            uiLayout.Children.Add(this._infoboardView);
        }

        /*
        public void SetCurrentDirection(Direction newDirection)
        {
            GameManager.Instance.SetNewDirection(newDirection);
        }
        */
        
    }
}
