using Microsoft.UI.Xaml.Controls;

using SnakeWinUi.MVVM.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SnakeWinUi.MVVM.View
{
    public sealed partial class InfoboardView : UserControl
    {
        public InfoboardViewModel InfoboardViewModel { get; private set; } = new InfoboardViewModel();
        public InfoboardView()
        {
            this.InitializeComponent();
            this.DataContext = this.InfoboardViewModel;
        }
    }
}
