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

        public event PropertyChangedEventHandler? PropertyChanged;

        public AppStateManager()
        {
            this.AppState = AppState.MainMenu;
            this.GameManager.OnGameOver += () =>
            {
                this.SetStateToGameOver();
            };
        }

        public void SetStateToMainMenu()
        {
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
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
