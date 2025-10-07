using Microsoft.UI.Xaml.Controls;
using SnakeWinUi.Controller;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using Windows.System;

namespace SnakeWinUi.MVVM.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
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

            GameManager.Instance.Init();
            GameManager.Instance.StartGame();

            this.KeyDown += this.GamePage_OnKeyDown;
            this.Content = GameboardView.Instance;
        }

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
                SnakeModel.Instance.SetDirection(newDirection.Value);
            }
        }
    }
}
