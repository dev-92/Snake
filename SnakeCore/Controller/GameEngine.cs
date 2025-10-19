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
    /// handles collectables, collisions, and manages the game state.
    /// </summary>
    public class GameEngine
    {
        private UpdateComposite _updateGroup { get; set; } = new();
        public GameboardModel GameboardModel { get; private set; }
        public InfoboardModel InfoboardModel { get; private set; } = new();
        public SnakeModel Snake { get; private set; } = new();

        private IAudioService _audioService { get; set; }

        private List<CollectableItemModel> _collectableItems { get; set; } = new();
        private CollectableEffectHandler _collectableHandler { get; set; }

        public GameState GameState { get; private set; } = GameState.Paused;
        public event Action? GameOver;

        private Direction _currentDirection { get; set; }

        /// <summary>
        /// Initializes a new instance of the GameEngine class.
        /// </summary>
        /// <param name="audioservice">The audio service for music and sound effects.</param>
        public GameEngine(IAudioService audioservice)
        {
            this._audioService = audioservice;

            this.GameboardModel = new GameboardModel(this.Snake);
            this._collectableHandler = new CollectableEffectHandler(this.Snake, this.InfoboardModel);

            this._updateGroup.AddParticipant(this.Snake);
            this._updateGroup.AddParticipant(this.GameboardModel);
            this._updateGroup.AddParticipant(this._collectableHandler);
        }

        /// <summary>
        /// Starts the game and begins the update loop.
        /// </summary>
        public void Run()
        {
            this.GameState = GameState.Running;
            this._audioService.PlayMusic(GameMusicType.GameLoop);
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
        /// Resets the game to its initial state.
        /// </summary>
        public void Reset()
        {
            this.Snake = new SnakeModel();
            this.GameboardModel = new GameboardModel(this.Snake);
            this.InfoboardModel = new InfoboardModel();

            this._collectableHandler = new CollectableEffectHandler(this.Snake, this.InfoboardModel);
            this._collectableItems = new List<CollectableItemModel>();

            this._updateGroup = new UpdateComposite();
            this._updateGroup.AddParticipant(this.Snake);
            this._updateGroup.AddParticipant(this.GameboardModel);
            this._updateGroup.AddParticipant(this._collectableHandler);

            GameSettings.UpdateSpeedMillis = CoreConstants.BASIC_UPDATE_MILLIS;
        }

        /// <summary>
        /// Sets the movement direction for the snake.
        /// </summary>
        /// <param name="newDirection">The new direction for the snake to move.</param>
        public void SetDirection(Direction newDirection)
        {
            this._currentDirection = newDirection;
        }

        /// <summary>
        /// Updates the snake’s movement direction based on the current input.
        /// </summary>
        private void SetSnakeDirection()
        {
            this.Snake.SetDirection(this._currentDirection);
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

            this.SpawnCollectable();
        }

        /// <summary>
        /// Applies the effect of a collected item and plays its sound effect.
        /// </summary>
        /// <param name="item">The collected item to handle.</param>
        private void HandleItemCollected(CollectableItemModel item)
        {
            this._collectableHandler.Handle(item);
            this._audioService.PlayEffect(item.SoundEffect);
        }

        /// <summary>
        /// Checks for collisions between the snake's head and its tail, triggering game over if detected.
        /// </summary>
        private void CheckCollision()
        {
            if (this.HasHeadCollidedWithTail())
            {
                this.Stop();
                this.GameOver?.Invoke();
            }
        }

        /// <summary>
        /// Spawns a new collectable item at a random free position until the maximum count is reached.
        /// </summary>
        private void SpawnCollectable()
        {
            while (this._collectableItems.Count < CollectableConfig.MAX_ITEMS)
            {
                CollectableItemModel newItem = CollectableItemFactory.CreateRandomCollectableItem(this.GetRndFreePosition());
                this._collectableItems.Add(newItem);
                this.GameboardModel.PlaceCollectableItem(newItem);
            }
        }

        /// <summary>
        /// Removes a collectable item from the board and the active item list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        private void RemoveCollectable(CollectableItemModel item)
        {
            this.GameboardModel.RemoveCollectableItem(item);
            this._collectableItems.Remove(item);
        }

        /// <summary>
        /// Finds a random free position on the game board not occupied by the snake.
        /// </summary>
        /// <returns>A free position for placing a collectable.</returns>
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
            while (this.Snake.Tail.Any(s => s.CurrentPosition == freePosition) ||
                   this.Snake.Head.CurrentPosition == freePosition);

            return freePosition;
        }

        /// <summary>
        /// Determines if the snake has collected a specific item.
        /// </summary>
        /// <param name="item">The collectable item to check.</param>
        /// <returns>True if the snake collected the item; otherwise false.</returns>
        private bool HasSnakeCollectedItem(CollectableItemModel item)
        {
            return this.Snake.Head.CurrentPosition == item.Position;
        }

        /// <summary>
        /// Determines if the snake’s head has collided with its tail.
        /// </summary>
        /// <returns>True if a collision occurred; otherwise false.</returns>
        private bool HasHeadCollidedWithTail()
        {
            return this.Snake.Tail.Any(s => s.CurrentPosition == this.Snake.Head.CurrentPosition);
        }
    }
}
