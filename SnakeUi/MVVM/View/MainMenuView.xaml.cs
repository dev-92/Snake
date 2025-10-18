
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SnakeUi.Controller;

namespace SnakeUi.MVVM.View;

public sealed partial class MainMenuView : UserControl
{
    private AppStateManager _appStateManager {  get; set; }

    public MainMenuView(AppStateManager appStateManager)
    {
        this.InitializeComponent();
        this._appStateManager = appStateManager;
    }

    private void StartGame_Click(object sender, RoutedEventArgs e)
    {
        this._appStateManager.SetStateToPlaying();
    }
}
