
using Snake.Controller;

namespace Snake.MVVM.View;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		this.InitializeComponent();

		GameController controller = new GameController();
	}
}