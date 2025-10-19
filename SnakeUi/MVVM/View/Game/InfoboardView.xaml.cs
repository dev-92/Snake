using Microsoft.UI.Xaml.Controls;
using SnakeCore.Model.Entity;

namespace SnakeUi.MVVM.View
{
    /// <summary>
    /// Represents the user interface view for the information board,
    /// displaying score, snake length, and speed factor.
    /// Binds to an <see cref="InfoboardModel"/> as its data context.
    /// </summary>
    public sealed partial class InfoboardView : UserControl
    {
        public InfoboardModel InfoboardModel { get; private set; }

        public InfoboardView(InfoboardModel infoboardModel)
        {
            this.InitializeComponent();
            this.InfoboardModel = infoboardModel;
            this.DataContext = this.InfoboardModel;
        }


    }
}
