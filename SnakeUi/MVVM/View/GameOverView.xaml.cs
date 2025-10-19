using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using SnakeUi.Controller;

namespace SnakeUi.MVVM.View;

public sealed partial class GameOverView : UserControl
{
    private AppStateManager _appStateManager { get; set; }

    public GameOverView(AppStateManager appstateManager)
    {
        this.InitializeComponent();
        this._appStateManager = appstateManager;
    }

    private void TryAgain_Click(object sender, RoutedEventArgs e)
    {
        this._appStateManager.SetStateToPlaying();
    }

    private void MainMenu_Click(object sender, RoutedEventArgs e)
    {
        this._appStateManager.SetStateToMainMenu();
    }
}
