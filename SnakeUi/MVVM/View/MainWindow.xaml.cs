using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using SnakeCore.Enums;
using SnakeUi.Controller;
using Windows.Graphics;
using Windows.System;

namespace SnakeUi.MVVM.View
{
    public sealed partial class MainWindow : Window
    {
        private const int WINDOW_WIDTH = 1145;
        private const int WINDOW_HEIGHT = 1260;

        private const string WINDOW_TITLE = "Snake";
        private const string PATH_TO_SNAKE_ICON = @"Assets/snake_icon.ico";

        private readonly PointInt32 _windowOpeningPos = new PointInt32(0,0);

        private GameManager _gameManager { get; set; }
        private GameView _gameView { get; set; }

        private AppStateManager _appStateManager { get; set; } = new AppStateManager();

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetWindowConfiguration();

            this._gameManager = new GameManager();

            this._gameView = new GameView(cellModels: this._gameManager.GameEngine.GameboardModel.Cells)
            {
                Margin = new Thickness(10, 40, 10, 10)
            };
            this.WindowLayout.Children.Add(this._gameView);

            this._gameManager.StartGame();
        }

        private void SetWindowConfiguration()
        {
            this.AppWindow.Title = MainWindow.WINDOW_TITLE;
            this.AppWindow.SetIcon(MainWindow.PATH_TO_SNAKE_ICON);

            this.AppWindow.Resize(new SizeInt32(MainWindow.WINDOW_WIDTH, MainWindow.WINDOW_HEIGHT));
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
        /// <para>
        /// This KeyDown event is subscribed in XAML on the parent Grid
        /// (<c>WindowLayout</c>), ensuring that arrow keys are reliably
        /// detected regardless of which child element currently has focus.
        /// </para>
        /// </summary>
        /// <param name="sender">The object that raised the event (the <c>WindowLayout</c> Grid).</param>
        /// <param name="e">Event arguments containing information about the pressed key.</param>
        private void CoreWindow_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
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
               //GameManager.Instance.SetDirection(newDirection.Value);
            }
        }
    }
}
