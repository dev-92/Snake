using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using SnakeCore.Enums;
using SnakeUi.Config;
using SnakeUi.Controller;
using System;
using Windows.Graphics;
using Windows.System;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the main application window.
    /// Hosts the game view, manages window configuration, and handles keyboard input for the snake.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly PointInt32 _windowOpeningPos = new PointInt32(0, 0);
        private Thickness _margin = new Thickness(10, 40, 10, 10);

        private GameManager _gameManager { get; set; }
        private GameView _gameView { get; set; }

        private AppStateManager _appStateManager { get; set; } = new AppStateManager();

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetWindowConfiguration();
    
            this._gameManager = new GameManager();

            this._gameView = new GameView(cells: this._gameManager.Cells,
                                          infoboardModel: this._gameManager.InfoboardModel)
            {
                Margin = this._margin
            };
            this.WindowLayout.Children.Add(this._gameView);

            this._gameManager.StartGame();

        }

        /// <summary>
        /// Configures the window title, icon, size, position, and behavior (non-resizable, non-maximizable).
        /// </summary>
        private void SetWindowConfiguration()
        {
            this.AppWindow.Title = UiConstants.WINDOW_TITLE;
            //this.AppWindow.SetIcon(@"Assets/snake_outline.png");

            this.AppWindow.Resize(new SizeInt32(UiConstants.WINDOW_WIDTH, UiConstants.WINDOW_HEIGHT));
            this.AppWindow.Move(this._windowOpeningPos);

            if (this.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsResizable = false;
                presenter.IsMaximizable = false;
            }

            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(this.CustomTitleBar);
        }

        /// <summary>
        /// Handles keyboard input for controlling the snake.
        /// Detects arrow key presses and updates the snake's movement direction via <see cref="GameManager"/>.
        /// </summary>
        /// <param name="sender">The object that raised the event (the parent Grid).</param>
        /// <param name="e">Event arguments containing information about the pressed key.</param>
        private void CoreWindow_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Direction? newDirection = e.Key switch
            {
                VirtualKey.Up => Direction.Up,
                VirtualKey.Down => Direction.Down,
                VirtualKey.Left => Direction.Left,
                VirtualKey.Right => Direction.Right,
                _ => null
            };

            if (newDirection.HasValue)
            {
                this._gameManager.SetDirection(newDirection.Value);
            }
        }
    }
}
