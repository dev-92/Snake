using Microsoft.UI.Xaml.Media;

using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.MVVM.Model.Entity.Collectables;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.MVVM.View;
using SnakeWinUi.MVVM.ViewModel;
using SnakeWinUi.Services.Audio;
using SnakeWinUi.Services.UpdateService;

using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeWinUi.Controller
{
    /// <summary>
    /// Singleton class that manages the game logic and updates.
    /// Handles game states (start, pause) and regularly updates
    /// all registered game participants.
    /// </summary>
    internal class GameManager
    {
        private const int MAX_ITEMS = 5;
        private UpdateComposite _updateGroup { get; set; } = new UpdateComposite();
        private List<CollectableItem> _collectableItems { get; set; } = new();

        public GameboardView GameboardView { get; private set; }
        private SnakeModel _snake {  get; set; }
        private InfoboardViewModel _infoboardViewModel { get; set; }
        private CollectableHandler _collectableHandler { get; set; }

        private Direction _currentDirection { get; set; }

        private DateTime _lastUpdate { get; set; } = DateTime.Now;
        private GameState _gameState { get; set; } = GameState.Paused;

        private static GameManager? _instance;

        public static GameManager Instance
        {
            get
            {
                if (GameManager._instance == null)
                {
                    GameManager._instance = new GameManager();
                }

                return GameManager._instance;
            }
        }

        private GameManager()
        {

        }

        public void Initialize(GameboardView gameboardView, InfoboardViewModel infoboardViewModel, SnakeModel snake)
        {
            this._snake = snake;
            this.GameboardView = gameboardView;

            this._infoboardViewModel = infoboardViewModel;
            this._collectableHandler = new CollectableHandler(this._snake, this._infoboardViewModel);

            this._updateGroup.AddParticipant(this._snake);
            this._updateGroup.AddParticipant(this.GameboardView);

            CompositionTarget.Rendering += this.OnRendering;
        }

        /// <summary>
        /// Starts the game.
        /// The update loop will run, and Snake & Board will be updated continuously.
        /// </summary>
        public void StartGame()
        {
            this._gameState = GameState.Running;            
            SoundManager.Instance.PlayMusic(GameMusicType.GameLoop1);
        }

        /// <summary>
        /// Pauses the game.
        /// The update loop stops until StartGame is called again.
        /// </summary>
        public void PauseGame()
        {
            this._gameState = GameState.Paused;
            SoundManager.Instance.StopMusic();
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
        /// Internal event handler for CompositionTarget.Rendering.
        /// Calculates elapsed time since the last update and calls Update() if enough time has passed.
        /// </summary>
        private void OnRendering(object? sender, object e)
        {
            if (this._gameState == GameState.Paused)
            {
                return;
            }

            var now = DateTime.Now;
            var delta = (now - this._lastUpdate).TotalMilliseconds;

            if (delta >= GameSettings.UpdateSpeedMillis)
            {
                this.Update();
                this._lastUpdate = now;
            }
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
            while (this._collectableItems.Count < GameManager.MAX_ITEMS)
            {
                this.SpawnCollectable();
            }

            foreach (CollectableItem item in this._collectableItems.ToList())
            {
                bool shouldBeRemoved = false;

                if (this.HasSnakeCollectedItem(item))
                {
                    this.HandleItemCollected(item);
                    SoundManager.Instance.PlayEffect(item.SoundEffect);

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

        private void HandleItemCollected(CollectableItem item)
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
            CollectableItem newItem = CollectableItemFactory.CreateRandomCollectableItem(this.GetRandomFreePosition());
            this._collectableItems.Add(newItem);

            this.GameboardView.DrawCollectableItem(newItem);          
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

        private bool HasSnakeCollectedItem(CollectableItem item)
        {
            return this._snake.Head.CurrentPosition == item.Position;
        }

        private void RemoveCollectableItem(CollectableItem item)
        {
            this.GameboardView.EraseCollectableItem(item);
            this._collectableItems.Remove(item);
        }

        private bool HasHeadCollidedWithTail()
        {
            return this._snake.Tail.Any(s => s.CurrentPosition == this._snake.Head.CurrentPosition);
        }
    }
}
