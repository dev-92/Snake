using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SnakeUi.Controller;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the "Game Over" screen displayed when the player loses the game.
    /// Provides options to restart the game or return to the main menu.
    /// </summary>
    public sealed partial class GameOverView : UserControl
    {
        private AppStateManager _appStateManager { get; set; }

        public GameOverView(AppStateManager appstateManager)
        {
            this.InitializeComponent();
            this._appStateManager = appstateManager;
        }

        /// <summary>
        /// Handles the click event for the "Try Again" button.
        /// Resets the game state and starts a new play session.
        /// </summary>
        private void TryAgain_Click(object sender, RoutedEventArgs e)
        {
            this._appStateManager.SetStateToPlaying();
        }

        /// <summary>
        /// Handles the click event for the "Main Menu" button.
        /// Returns the player to the main menu screen.
        /// </summary>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            this._appStateManager.SetStateToMainMenu();
        }
    }
}
