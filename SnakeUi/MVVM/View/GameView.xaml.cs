using Microsoft.UI.Xaml.Controls;
using SnakeCore.Model.Entity;
using SnakeUi.Controller;
using System.Collections.Generic;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the main game page, hosting the game board and the information board.
    /// Handles UI layout and keyboard focus for the game.
    /// Implements a singleton pattern to ensure only one instance exists.
    /// </summary>
    public sealed partial class GameView : Page
    {
        private GameboardView _gameboardView { get; set; }
        private InfoboardView _infoboardView { get; set; }

        public GameView(GameManager gameManger)
        {
            this.InitializeComponent();

            this._gameboardView = new GameboardView(gameManger.Cells);
            this._infoboardView = new InfoboardView(gameManger.InfoboardModel);

            this.AddGameboardTo(this.UiLayout);
            this.AddInfoboardTo(this.UiLayout);

            this.Content = this.UiLayout;
            this.UiLayout.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);
        }

        /// <summary>
        /// Adds the game board view to the specified UI layout grid.
        /// </summary>
        private void AddGameboardTo(Grid uiLayout)
        {
            Grid.SetRow(this._gameboardView, 1);
            Grid.SetColumn(this._gameboardView, 0);

            uiLayout.Children.Add(this._gameboardView);
        }

        /// <summary>
        /// Adds the information board view to the specified UI layout grid.
        /// </summary>
        private void AddInfoboardTo(Grid uiLayout)
        {
            Grid.SetRow(this._infoboardView, 0);
            Grid.SetColumn(this._infoboardView, 0);

            uiLayout.Children.Add(this._infoboardView);
        }
    }
}
