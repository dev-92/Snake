using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;
using SnakeCore.Services.UpdateService;

using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Services;

namespace SnakeCore.Controller
{
    public class GameEngine
    {
        private const int MAX_ITEMS = 5;
        private UpdateComposite _updateGroup { get; set; } = new();

        private GameboardModel _gameboardModel { get; set; } = new();
        private InfoboardModel _infoboardModel { get; set; } = new();
        private SnakeModel _snake { get; set; } = new();

        private IAudioService _audioService { get; set; }
        private ICollectableRenderService _gameboardView { get; set; }

        private List<CollectableItemModel> _collectableItems { get; set; } = new();
        private CollectableHandler _collectableHandler { get; set; }

        private GameState _gameState { get; set; } = GameState.Paused;
        private Direction _currentDirection { get; set; } 

        public GameEngine(IAudioService audioservice, ICollectableRenderService gameboardView)
        {
            this._audioService = audioservice;
            this._gameboardView = gameboardView;

            this._collectableHandler = new CollectableHandler(this._snake, this._infoboardModel);

            this._updateGroup.AddParticipant(this._snake);
            this._updateGroup.AddParticipant(this._gameboardModel);
        }

        /// <summary>
        /// Starts the game.
        /// The update loop will run, and Snake & Board will be updated continuously.
        /// </summary>
        public void StartGame()
        {
            this._gameState = GameState.Running;
            this._audioService.PlayMusic(GameMusicType.GameLoop1);
        }

        /// <summary>
        /// Pauses the game.
        /// The update loop stops until StartGame is called again.
        /// </summary>
        public void PauseGame()
        {
            this._gameState = GameState.Paused;
            this._audioService.StopMusic();
        }

        public void SetNewDirection(Direction newDirection)
        {
            this._currentDirection = newDirection;
        }

        private void SetSnakeDirection()
        {
            this._snake.SetDirection(this._currentDirection);
        }

        /// <summary>
        /// Calls the Update method on all participants registered in the UpdateGroup.
        /// </summary>
        public void Update()
        {
            this.UpdateCollectables();
            this.CheckCollision();

            this._updateGroup.Update();
            this.SetSnakeDirection();
        }

        private void UpdateCollectables()
        {
            while (this._collectableItems.Count < GameEngine.MAX_ITEMS)
            {
                this.SpawnCollectable();
            }

            foreach (CollectableItemModel item in this._collectableItems.ToList())
            {
                bool shouldBeRemoved = false;

                if (this.HasSnakeCollectedItem(item))
                {
                    this.HandleItemCollected(item);
                    this._audioService.PlayEffect(item.SoundEffect);

                    shouldBeRemoved = true;
                }
                else if (item.IsExpired())
                {
                    shouldBeRemoved = true;
                }

                if (shouldBeRemoved)
                {
                    this.RemoveCollectableItem(item);
                }
            }
        }

        private void HandleItemCollected(CollectableItemModel item)
        {
            this._collectableHandler.Handle(item);
        }

        private void CheckCollision()
        {
            if (this.HasHeadCollidedWithTail())
            {
                this.PauseGame();
            }
        }

        /// <summary>
        /// Adds a participant to the UpdateGroup so it will be regularly updated
        /// in the game loop.
        /// </summary>
        /// <param name="participant">The object implementing IUpdateEntity.</param>
        public void AddToUpdateGroup(IUpdateable participant)
        {
            this._updateGroup.AddParticipant(participant);
        }

        private void SpawnCollectable()
        {
            CollectableItemModel newItem = CollectableItemFactory.CreateRandomCollectableItem(this.GetRandomFreePosition());
            this._collectableItems.Add(newItem);

            this._gameboardView.RenderCollectableItem(newItem);
        }

        private Position2D GetRandomFreePosition()
        {
            Random random = new();
            Position2D freePosition;

            do
            {
                int xPos = random.Next(0, GameSettings.SideLength - 1);
                int yPos = random.Next(0, GameSettings.SideLength - 1);

                freePosition = new Position2D(xPos, yPos);
            }
            while (this._snake.Tail.Any(s => s.CurrentPosition == freePosition) ||
                   this._snake.Head.CurrentPosition == freePosition);

            return freePosition;
        }

        private bool HasSnakeCollectedItem(CollectableItemModel item)
        {
            return this._snake.Head.CurrentPosition == item.Position;
        }

        private void RemoveCollectableItem(CollectableItemModel item)
        {
            this._gameboardView.EraseCollectableItem(item);
            this._collectableItems.Remove(item);
        }

        private bool HasHeadCollidedWithTail()
        {
            return this._snake.Tail.Any(s => s.CurrentPosition == this._snake.Head.CurrentPosition);
        }
    }
}
