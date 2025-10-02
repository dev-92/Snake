using Snake.MVVM.Model;
using Snake.MVVM.View;
using Snake.UpdateService;

namespace Snake.Controller
{
    internal class GameController : IUpdateEntity
    {
        private const int UPDATE_SPEED_MILLIS = 1000;

        internal GameboardView GameboardView {  get; set; }
        private SnakeModel _snakeModel { get; set; }
        private UpdateGroup _updateGroup {  get; set; }

        private IDispatcherTimer _gameTimer { get; set; } 

        public GameController() 
        {
            GameboardView = new GameboardView(100);
            _snakeModel = SnakeModel.Instance; 

            _updateGroup = new UpdateGroup();
            _updateGroup.AddParticipant(_snakeModel);
            _updateGroup.AddParticipant(GameboardView);

            _gameTimer = Application.Current.Dispatcher.CreateTimer();
            _gameTimer.Interval = TimeSpan.FromMilliseconds(UPDATE_SPEED_MILLIS);
            _gameTimer.Tick += (s, e) => Update();
            _gameTimer.IsRepeating = true;
            
            StartGame();
        }

        public void StartGame()
        {
            _gameTimer.Start();
        }

        public void PauseGame()
        {
            _gameTimer.Stop();
        }

        private void DrawSnake()
        {
            int headIndex = _snakeModel.Head.X * GameboardView.SideLength + _snakeModel.Head.Y;
            GameboardView.CellViewModels[headIndex].CellModel.CellStatus = CellModel.Status.Snake;

            foreach(Position2D tailPiece in _snakeModel.Tail)
            {
                int currentTailPieceIndex = tailPiece.X * GameboardView.SideLength + tailPiece.Y;
                GameboardView.CellViewModels[currentTailPieceIndex].CellModel.CellStatus = CellModel.Status.Snake;
            }
        }

        public void Update()
        {
            _updateGroup.Update();
            DrawSnake();
        }
    }
}
