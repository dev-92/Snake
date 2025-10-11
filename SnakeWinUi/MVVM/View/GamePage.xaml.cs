using Microsoft.UI.Xaml.Controls;

using SnakeWinUi.Controller;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using Windows.System;

namespace SnakeWinUi.MVVM.View
{
    /// <summary>
    /// Represents the main game page, hosting the game board and handling keyboard input.
    /// Implements a singleton pattern to ensure only one instance exists.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private SnakeModel _snake {  get; set; }
        private GameboardView _gameboardView { get; set;}
        private InfoboardView _infoboardView { get; set; }

        private static GamePage? _instance;
        public static GamePage? Instance
        {
            get
            {
                if (GamePage._instance == null)
                {
                    GamePage._instance = new GamePage();
                }

                return GamePage._instance;
            }
        }

        public GamePage()
        {
            this.InitializeComponent();           

            this._snake = new SnakeModel();
            this._gameboardView = new GameboardView(this._snake);
            this._infoboardView = new InfoboardView();

            _ = GameManager.Instance;
            GameManager.Instance.Initialize(
                gameboardView: this._gameboardView,
                infoboardViewModel: this._infoboardView.InfoboardViewModel,
                snake: this._snake
                );

            this.AddGameboardTo(this.UiLayout);
            this.AddInfoboardTo(this.UiLayout);

            this.Content = this.UiLayout;

            this.KeyDown += this.GamePage_OnKeyDown;
            GameManager.Instance.StartGame();
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

        /// <summary>
        /// Handles key press events to change the snake's direction.
        /// Ignores keys that do not correspond to a movement direction.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">Key event arguments.</param>
        private void GamePage_OnKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Direction? newDirection = e.Key switch
            {
                VirtualKey.Up    => Direction.Up,
                VirtualKey.Down  => Direction.Down,
                VirtualKey.Left  => Direction.Left,
                VirtualKey.Right => Direction.Right,
                _                => null
            };

            if (newDirection.HasValue)
            {
                GameManager.Instance.SetNewDirection(newDirection.Value);
            }
        }
    }
}
