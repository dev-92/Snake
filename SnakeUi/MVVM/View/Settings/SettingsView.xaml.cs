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
        private ThemeManager _themeManager { get; set; } = new();

        public SettingsView(AppStateManager appStateManager)
        {
            this.InitializeComponent();           
            this._appstateManager = appStateManager;

            this.DataContext = this;

            this.DesignSettingsSection.Initialize(this._themeManager);
            this.DesignSettingsSection.ThemeSelectionChanged += this.RefreshSettingsView;
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this._appstateManager.SetStateToMainMenu();
        }


    }
}
