using SnakeCore.Config;
using SnakeCore.Enums;
using SnakeCore.Model.Entity;
using SnakeCore.Model.Entity.Collectables;
using SnakeCore.Model.Entity.Snake;
using SnakeCore.Model.ValueObject;
using SnakeCore.Services;
using SnakeCore.Services.UpdateService;

namespace SnakeCore.Controller
{
    /// <summary>
    /// Core controller that manages the main game loop, updates all entities,
    /// and handles collectables, collisions, and game state changes.
    /// </summary>
    public class GameEngine
    {
        private const int MAX_ITEMS = 5;
        private UpdateComposite _updateGroup { get; set; } = new();

        public GameboardModel GameboardModel { get; private set; }
        public InfoboardModel InfoboardModel { get; private set; } = new();
        private SnakeModel _snake { get; set; } = new();

        private IAudioService _audioService { get; set; }

        private List<CollectableItemModel> _collectableItems { get; set; } = new();
        private CollectableEffectHandler _collectableHandler { get; set; }

        public GameState GameState { get; private set; } = GameState.Paused;
        private Direction _currentDirection { get; set; }

        public GameEngine(IAudioService audioservice)
        {
            this._audioService = audioservice;

            this.GameboardModel = new GameboardModel(this._snake);
            this._collectableHandler = new CollectableEffectHandler(this._snake, this.InfoboardModel);

            this._updateGroup.AddParticipant(this._snake);
            this._updateGroup.AddParticipant(this.GameboardModel);
            this._updateGroup.AddParticipant(this._collectableHandler);
        }

        /// <summary>
        /// Starts the game and begins the update loop.
        /// </summary>
        public void Run()
        {
            this.GameState = GameState.Running;
            this._audioService.PlayMusic(GameMusicType.GameLoop1);
        }

        /// <summary>
        /// Stops the game and pauses all activity.
        /// </summary>
        public void Stop()
        {
            this.GameState = GameState.Paused;
            this._audioService.StopMusic();
        }

        /// <summary>
        /// Sets the direction for the snake movement.
        /// </summary>
        public void SetDirection(Direction newDirection)
        {
            this._currentDirection = newDirection;
        }

        /// <summary>
        /// Updates the snake’s movement direction.
        /// </summary>
        private void SetSnakeDirection()
        {
            this._snake.SetDirection(this._currentDirection);
        }

        /// <summary>
        /// Executes one full update cycle including collectables, movement, and collisions.
        /// </summary>
        public void Update()
        {
            this.UpdateCollectables();
            this.CheckCollision();

            this._updateGroup.Update();
            this.SetSnakeDirection();
        }

        /// <summary>
        /// Manages spawning, collecting, and removing collectables.
        /// </summary>
        private void UpdateCollectables()
        {
            this.SpawnCollectable();
            
            foreach (CollectableItemModel item in this._collectableItems.ToList())
            {
                bool shouldBeRemoved = false;

                if (this.HasSnakeCollectedItem(item))
                {
                    this.HandleItemCollected(item);
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

        /// <summary>
        /// Applies the effect of a collected item.
        /// </summary>
        private void HandleItemCollected(CollectableItemModel item)
        {
            this._collectableHandler.Handle(item);
            this._audioService.PlayEffect(item.SoundEffect);
        }

        /// <summary>
        /// Checks for collisions with the snake’s tail and stops the game if one occurs.
        /// </summary>
        private void CheckCollision()
        {
            if (this.HasHeadCollidedWithTail())
            {
                this.Stop();
            }
        }

        /// <summary>
        /// Spawns a new collectable item at a random free position.
        /// </summary>
        private void SpawnCollectable()
        {
            while(this._collectableItems.Count < GameEngine.MAX_ITEMS)
            {
                CollectableItemModel newItem = CollectableItemFactory.CreateRandomCollectableItem(this.GetRndFreePosition());
                this._collectableItems.Add(newItem);
                this.GameboardModel.PlaceCollectableItem(newItem);
            }
        }

        /// <summary>
        /// Removes a collectable from the board and active list.
        /// </summary>
        private void RemoveCollectable(CollectableItemModel item)
        {
            this.GameboardModel.RemoveCollectableItem(item);
            this._collectableItems.Remove(item);
        }

        /// <summary>
        /// Finds a random free position on the game board that is not occupied by the snake.
        /// </summary>
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

        /// <summary>
        /// Checks if the snake has collected a given item.
        /// </summary>
        private bool HasSnakeCollectedItem(CollectableItemModel item)
        {
            return this._snake.Head.CurrentPosition == item.Position;
        }

        /// <summary>
        /// Checks if the snake’s head has collided with its tail.
        /// </summary>
        private bool HasHeadCollidedWithTail()
        {
            return this._snake.Tail.Any(s => s.CurrentPosition == this._snake.Head.CurrentPosition);
        }
    }
}
