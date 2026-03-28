
using Microsoft.UI.Xaml;
using SnakeUi.Config;
using SnakeUi.MVVM.View;

namespace SnakeUi
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;

        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            this.SynchronizeRessources();

            this._window = new MainWindow();
            this._window.Activate();
        }

        /// <summary>
        /// Synchronizes (global) ressources like the global window height with classes like <see cref="UiConstants"/>
        /// </summary>
        private void SynchronizeRessources()
        {
            this.Resources["GlobalWindowWidth"] = (double)UiConstants.WINDOW_WIDTH;
            this.Resources["GlobalWindowHeight"] = (double)UiConstants.WINDOW_HEIGHT;

            this.Resources["InfoboardHeight"] = (double)UiConstants.INFOBOARD_HEIGHT;
            this.Resources["GameboardSize"] = (double)UiConstants.GAMEBOARD_SIZE;
        }
    }
}
