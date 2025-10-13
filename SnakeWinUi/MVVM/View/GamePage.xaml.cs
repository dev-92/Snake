using Microsoft.UI.Xaml.Controls;

using SnakeUi.Controller;
using SnakeUi.Enums;
using SnakeUi.MVVM.Model.Entity.Snake;
using Windows.System;

namespace SnakeUi.MVVM.View
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
            this.UiLayout.Focus(Microsoft.UI.Xaml.FocusState.Programmatic);

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

        public void SetCurrentDirection(Direction newDirection)
        {
            GameManager.Instance.SetNewDirection(newDirection);
        }
    }
}
