using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SnakeUi.Controller;
using SnakeUi.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeUi.MVVM.View.MainMenu
{
    /// <summary>
    /// Represents the settings screen of the application.
    /// </summary>
    public sealed partial class SettingsView : UserControl, INotifyPropertyChanged
    {
        private AppStateManager _appstateManager { get; set; }

        private double _musicVol;
        public double MusicVol
        {
            get => this._musicVol;
            set
            {
                this._musicVol = value;
                this.OnPropertyChanged();
            }
        }

        private double _effectVol;
        public double EffectVol
        {
            get => this._effectVol;
            set
            {
                this._effectVol = value;
                this.OnPropertyChanged();
            }
        }

        public SettingsView(AppStateManager appStateManager)
        {
            this.InitializeComponent();           
            this._appstateManager = appStateManager;

            this.DataContext = this;

            this.MusicVol = AudioManager.Instance.MusicVolume * 100;
            this.EffectVol = AudioManager.Instance.EffectVolume * 100;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
        private void MusicSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            double newVolume = e.NewValue / 100.0;
            AudioManager.Instance.MusicVolume = newVolume;
        }

        /// <summary>
        /// Handles changes to the volume slider.
        /// Updates the internal volume value based on the user's input.
        /// </summary>
        private void EffectSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            double newVolume = e.NewValue / 100.0;
            AudioManager.Instance.EffectVolume = newVolume;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
