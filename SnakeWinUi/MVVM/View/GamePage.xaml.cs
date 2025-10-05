using Microsoft.UI.Xaml.Controls;
using SnakeWinUi.Controller;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity;

namespace SnakeWinUi.MVVM.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private static GamePage? _instance;
        public static GamePage? Instance
        {
            get
            {
                if (GamePage._instance == null)
                {
                    GamePage._instance = new GamePage();
                }

                return GamePage._instance;
            }
        }

        public GamePage()
        {
            this.InitializeComponent();
            this.Content = GameboardView.Instance;

            GameManager.Instance.Init();
            GameManager.Instance.StartGame();

            this.KeyDown += this.GamePage_OnKeyDown;
        }

        private void GamePage_OnKeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Up:
                    SnakeModel.Instance.SetDirection(Direction.Up);
                    break;

                case Windows.System.VirtualKey.Down:
                    SnakeModel.Instance.SetDirection(Direction.Down);
                    break;

                case Windows.System.VirtualKey.Left:
                    SnakeModel.Instance.SetDirection(Direction.Left);
                    break;

                case Windows.System.VirtualKey.Right:
                    SnakeModel.Instance.SetDirection(Direction.Right);
                    break;

            }
        }
    }
}
