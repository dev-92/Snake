using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SnakeUi.Controller;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the main menu screen of the game.
    /// </summary>
    public sealed partial class MainMenuView : UserControl
    {
        private AppStateManager _appStateManager { get; set; }

        public MainMenuView(AppStateManager appStateManager)
        {
            this.InitializeComponent();
            this._appStateManager = appStateManager;
        }

        /// <summary>
        /// Handles the click event for the "Start Game" button.
        /// Switches the application state to start a new gameplay session.
        /// </summary>
        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            this._appStateManager.SetStateToPlaying();
        }

        /// <summary>
        /// Handles the click event for the "Settings" button.
        /// Opens the settings screen where the player can adjust preferences.
        /// </summary>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this._appStateManager.SetStateToSettings();
        }
    }
}
