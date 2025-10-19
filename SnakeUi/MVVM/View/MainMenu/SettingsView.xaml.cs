using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SnakeUi.Controller;

namespace SnakeUi.MVVM.View.MainMenu;

public sealed partial class SettingsView : UserControl
{
    private AppStateManager _appstateManager {  get; set; }
    public SettingsView(AppStateManager appStateManager)
    {
        this.InitializeComponent();
        this._appstateManager = appStateManager;

    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        this._appstateManager.SetStateToMainMenu();
    }

    private void VolumeSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        double newVolume = e.NewValue / 100.0; // z. B. 0.0–1.0
    }
}
