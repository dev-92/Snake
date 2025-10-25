using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using SnakeCore.Enums;
using SnakeUi.Config;
using SnakeUi.Controller;
using SnakeUi.Enums;
using SnakeUi.MVVM.View.MainMenu;
using SnakeUi.Services;

using System.ComponentModel;

using Windows.Graphics;
using Windows.System;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the main application window.
    /// Hosts the game view, manages window configuration, and handles keyboard input for the snake.
    /// </summary>
    public sealed partial class MainWindow : Window , INotifyPropertyChanged
    {
        private readonly PointInt32 _windowOpeningPos = new PointInt32(0, 0);
        private Thickness _margin = new Thickness(10, 40, 10, 10);

        private AppStateManager _appStateManager { get; set; } = new AppStateManager(AudioManager.Instance);

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetWindowConfiguration();

            this._appStateManager.PropertyChanged += this.SetCurrentScreen;
            this._appStateManager.AppState = AppState.MainMenu;
        }


        /// <summary>
        /// Configures the window title, icon, size, position, and behavior (non-resizable, non-maximizable).
        /// </summary>
        private void SetWindowConfiguration()
        {
            this.AppWindow.Title = UiConstants.WINDOW_TITLE;

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SetCurrentScreen(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(AppStateManager.AppState)) return;

            this.ContentGrid.Children.Clear();

            switch (this._appStateManager.AppState)
            {
                case AppState.MainMenu:
                    this.ContentGrid.Children.Add(new MainMenuView(this._appStateManager));
                    break;

                case AppState.Playing:
                    this.ContentGrid.Children.Add(new GameView(this._appStateManager.GameManager)
                    {
                        Margin = this._margin
                    });
                    break;

                case AppState.GameOver:
                    this.ContentGrid.Children.Add(new GameOverView(this._appStateManager));
                    break;

                case AppState.Settings:
                    this.ContentGrid.Children.Add(new SettingsView(this._appStateManager));
                    break;
            }
        }

        /// <summary>
        /// Handles keyboard input for controlling the snake or entering the main menu while playing.
        /// Detects arrow key presses and updates the snake's movement direction via <see cref="GameManager"/>.
        /// </summary>
        /// <param name="sender">The object that raised the event (the parent Grid).</param>
        /// <param name="e">Event arguments containing information about the pressed key.</param>
        private void CoreWindow_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.Escape)
            {
                this.HandleEscapeKey();
                return;
            }

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
                this._appStateManager.GameManager.SetDirection(newDirection.Value);
            }
        }

        /// <summary>
        /// Brings up the maain menu screen by changing the AppStateManager.
        /// </summary>
        private void HandleEscapeKey()
        {
            this._appStateManager.SetStateToMainMenu();
        }
    }
}
