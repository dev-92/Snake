using Microsoft.UI.Xaml.Controls;
using SnakeCore.Model.Entity;

namespace SnakeUi.MVVM.View
{
    public sealed partial class InfoboardView : UserControl
    {
        public InfoboardModel InfoboardModel { get; private set; } = new();
        public InfoboardView()
        {
            this.InitializeComponent();
            this.DataContext = this.InfoboardModel;
        }
    }
}
