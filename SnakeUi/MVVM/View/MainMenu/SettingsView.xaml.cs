using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SnakeUi.Controller;

namespace SnakeUi.MVVM.View.MainMenu
{
    /// <summary>
    /// Represents the settings screen of the application.
    /// </summary>
    public sealed partial class SettingsView : UserControl
    {
        private AppStateManager _appstateManager { get; set; }

        public SettingsView(AppStateManager appStateManager)
        {
            this.InitializeComponent();
            this._appstateManager = appStateManager;
        }

        /// <summary>
        /// Handles the click event for the "Back" button.
        /// Returns the player to the main menu screen.
        /// </summary>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this._appstateManager.SetStateToMainMenu();
        }

        /// <summary>
        /// Handles changes to the volume slider.
        /// Updates the internal volume value based on the user's input.
        /// </summary>
        private void VolumeSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            double newVolume = e.NewValue / 100.0;
            // TODO: Apply the new volume to the AudioManager when integrated.
        }
    }
}
