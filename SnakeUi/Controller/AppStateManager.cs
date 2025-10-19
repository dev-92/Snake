using SnakeCore.Enums;
using SnakeCore.Services;
using SnakeUi.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeUi.Controller
{
    public class AppStateManager : INotifyPropertyChanged
    {
        private AppState _appState {  get; set; }
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

        public void SetStateToMainMenu()
        {
            this._audioService.PlayMusic(GameMusicType.MenuLoop);
            this.AppState = AppState.MainMenu;
        }

        public void SetStateToPlaying()
        {
            this.AppState = AppState.Playing;
            this.GameManager.StartGame();
        }

        public void SetStateToGameOver()
        {
            this.AppState = AppState.GameOver;

            this.GameManager.StopGame();
            this.GameManager.Reset();

            this._audioService.PlayMusic(GameMusicType.MenuLoop);
        }

        public void SetStateToSettings()
        {
            this.AppState = AppState.Settings;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
