using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SnakeUi.Config;
using SnakeUi.Controller;
using SnakeUi.Enums;
using SnakeUi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SnakeUi.MVVM.View.MainMenu
{
    /// <summary>
    /// Represents the settings screen of the application.
    /// </summary>
    public sealed partial class SettingsView : UserControl, INotifyPropertyChanged
    {
        private AppStateManager _appstateManager { get; set; }
        private ThemeManager _themeManager { get; set; } = new();

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

        private void ThemeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorTheme chosenColorTheme = this.GetChosenColorTheme(sender);
            this._themeManager.ChangeThemeTo(chosenColorTheme);

            this.RefreshSettingsView();
        }

        private ColorTheme GetChosenColorTheme(object sender)
        {
            if (sender is not ComboBox themeBox)
            {
                return ColorTheme.Dark;
            }

            if (themeBox.SelectedItem is not ComboBoxItem selectedItem)
            {
                return ColorTheme.Dark;
            }

            return selectedItem.Content switch
            {
                "Dark"       => ColorTheme.Dark,
                "Light"      => ColorTheme.Light,
                "Retro"      => ColorTheme.Retro,
                "OceanTech"  => ColorTheme.OceanTech,
                "Cyberpunk"  => ColorTheme.Cyberpunk,
                _            => ColorTheme.Dark,
            };
        }

        private void RefreshSettingsView()
        {
            var currentDataContext = this.DataContext;

            var parent = this.Parent as Panel;
            if (parent == null)
            {
                return;
            }

            int index = parent.Children.IndexOf(this);

            var newView = new SettingsView(this._appstateManager);
            newView.DataContext = currentDataContext;

            parent.Children.RemoveAt(index);
            parent.Children.Insert(index, newView);
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
        /// <summary>
        /// Handles changes to the gridsize slider.
        /// Updates the amount of the gameboards cells.
        /// </summary>
        private void GridSizeSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            // TODO
        }

        /// <summary>
        /// Handles changes to the cellshape slider.
        /// Handles if the cell is represented more edgy or round.
        /// </summary>
        private void CellShapeSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            // TODO
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
