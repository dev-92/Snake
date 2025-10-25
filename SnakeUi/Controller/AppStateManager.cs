using SnakeCore.Enums;
using SnakeCore.Services;
using SnakeUi.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeUi.Controller
{
    /// <summary>
    /// Manages the current state of the application and coordinates transitions between different screens.
    /// Handles game start, game over, main menu, and settings, as well as triggering audio playback.
    /// </summary>
    public class AppStateManager : INotifyPropertyChanged
    {
        private AppState _appState { get; set; }
        public AppState AppState
        {
            get => this._appState;
            set
            {
                this._appState = value;
                this.OnPropertyChanged(nameof(this.AppState));
            }
        }

        public GameManager GameManager { get; private set; } = new();

        private IAudioService _audioService { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AppStateManager(IAudioService audioService)
        {
            this._audioService = audioService;
            this.SetStateToMainMenu();

            this.GameManager.OnGameOver += () =>
            {
                this.SetStateToGameOver();
            };
        }

        /// <summary>
        /// Sets the application state to the main menu.
        /// Plays the menu background music.
        /// </summary>
        public void SetStateToMainMenu()
        {
            this._audioService.PlayMusic(GameMusicType.MenuLoop);
            this.AppState = AppState.MainMenu;

            this.GameManager.StopGame();
        }

        /// <summary>
        /// Sets the application state to playing and starts a new game session.
        /// </summary>
        public void SetStateToPlaying()
        {
            this.AppState = AppState.Playing;
            this.GameManager.StartGame();
        }

        /// <summary>
        /// Sets the application state to game over.
        /// Stops and resets the current game and plays the menu music.
        /// </summary>
        public void SetStateToGameOver()
        {
            this.AppState = AppState.GameOver;

            this.GameManager.StopGame();
            this.GameManager.Reset();

            this._audioService.PlayMusic(GameMusicType.MenuLoop);
        }

        /// <summary>
        /// Sets the application state to the settings screen.
        /// </summary>
        public void SetStateToSettings()
        {
            this.AppState = AppState.Settings;
        }

        /// <summary>
        /// Raises the PropertyChanged event for data binding.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. Automatically supplied by the compiler if not specified.</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
