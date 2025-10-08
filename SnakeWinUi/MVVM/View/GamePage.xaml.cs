using Microsoft.UI.Xaml.Controls;

using SnakeWinUi.Controller;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.Utils;
using Windows.System;

namespace SnakeWinUi.MVVM.View
{
    /// <summary>
    /// Represents the main game page, hosting the game board and handling keyboard input.
    /// Implements a singleton pattern to ensure only one instance exists.
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
            ProjectTreePrinter.PrintProjectTree(@"C:\Users\ty-ro\source\repos\SnakeWinUi");

            this.InitializeComponent();

            GameManager.Instance.Init();
            GameManager.Instance.StartGame();

            this.KeyDown += this.GamePage_OnKeyDown;
            this.Content = GameboardView.Instance;
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
                SnakeModel.Instance.SetDirection(newDirection.Value);
            }
        }
    }
}
