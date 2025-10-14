using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Services.UpdateService;

using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Services;
using SnakeCore.Model.ValueObject;

namespace SnakeCore.Controller
{
    public class GameEngine
    {
        private const int MAX_ITEMS = 5;
        private UpdateComposite _updateGroup { get; set; } = new();

        public GameboardModel GameboardModel { get; set; }
        private InfoboardModel _infoboardModel { get; set; } = new();
        private SnakeModel _snake { get; set; } = new();

        private IAudioService _audioService { get; set; }

        private List<CollectableItemModel> _collectableItems { get; set; } = new();
        private CollectableEffectHandler _collectableHandler { get; set; }

        public GameState GameState { get; set; } = GameState.Paused;
        private Direction _currentDirection { get; set; } 

        public GameEngine(IAudioService audioservice)
        {
            this._audioService = audioservice;
            
            this.GameboardModel = new GameboardModel(this._snake);
            this._collectableHandler = new CollectableEffectHandler(this._snake, this._infoboardModel);

            this._updateGroup.AddParticipant(this._snake);
            this._updateGroup.AddParticipant(this.GameboardModel);
        }

        /// <summary>
        /// Starts the game.
        /// The update loop will run, and Snake & Board will be updated continuously.
        /// </summary>
        public void Run()
        {
            this.GameState = GameState.Running;
            this._audioService.PlayMusic(GameMusicType.GameLoop1);
        }

        /// <summary>
        /// Pauses the game.
        /// The update loop stops until StartGame is called again.
        /// </summary>
        public void Stop()
        {
            this.GameState = GameState.Paused;
            this._audioService.StopMusic();
        }

        public void SetDirection(Direction newDirection)
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
                    this.RemoveCollectable(item);
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
                this.Stop();
            }
        }

        private void SpawnCollectable()
        {
            CollectableItemModel newItem = CollectableItemFactory.CreateRandomCollectableItem(this.GetRndFreePosition());
            this._collectableItems.Add(newItem);

            this.GameboardModel.PlaceCollectableItem(newItem);
        }

        private void RemoveCollectable(CollectableItemModel item)
        {
            this.GameboardModel.RemoveCollectableItem(item);
            this._collectableItems.Remove(item);
        }

        private Position2D GetRndFreePosition()
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

        private bool HasHeadCollidedWithTail()
        {
            return this._snake.Tail.Any(s => s.CurrentPosition == this._snake.Head.CurrentPosition);
        }
    }
}
