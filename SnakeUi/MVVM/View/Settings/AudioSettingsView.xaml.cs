using Microsoft.UI.Xaml.Controls;
using SnakeUi.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SnakeUi.MVVM.View.Settings;

public sealed partial class AudioSettingsView : UserControl, INotifyPropertyChanged
{
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

    public event PropertyChangedEventHandler? PropertyChanged;

    public AudioSettingsView()
    {
        this.InitializeComponent();

        this.MusicVol = AudioManager.Instance.MusicVolume * 100;
        this.EffectVol = AudioManager.Instance.EffectVolume * 100;
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
