using Microsoft.UI.Xaml.Media;

using SnakeWinUi.Config;
using SnakeWinUi.Enums;
using SnakeWinUi.Extensions;
using SnakeWinUi.MVVM.Model.Entity.Collectables;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.Model.ValueObject;
using SnakeWinUi.MVVM.View;
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
        private UpdateComposite _updateGroup { get; set; }
        private List<CollectableItem> _collectableItems { get; set; } = new();

        private DateTime _lastUpdate { get; set; }
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
            this._updateGroup = new UpdateComposite();
            this._lastUpdate = DateTime.Now;

            CompositionTarget.Rendering += this.OnRendering;
        }

        /// <summary>
        /// Initializes required singleton instances, such as GameboardView and SnakeModel.
        /// Should be called once at the start of the game to ensure lazy initialization.
        /// </summary>
        public void Init()
        {
            _ = GameboardView.Instance;
            _ = SnakeModel.Instance;
        }

        /// <summary>
        /// Starts the game.
        /// The update loop will run, and Snake & Board will be updated continuously.
        /// </summary>
        public void StartGame()
        {
            this._gameState = GameState.Running;
        }

        /// <summary>
        /// Pauses the game.
        /// The update loop stops until StartGame is called again.
        /// </summary>
        public void PauseGame()
        {
            this._gameState = GameState.Paused;
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
            if(this._collectableItems.IsEmpty())
            {
                this.CreateCollectable();
            }

            if(this.HasSnakeCollectedItem(this._collectableItems[0]))
            {
                this._collectableItems[0].Collect();
                this.RemoveColectableItem();
                this.CreateCollectable();
            }

            if(this.HasHeadCollidedWithTail())
            {
                this.PauseGame();
            }

            this._updateGroup.Update();
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

        private void CreateCollectable()
        {           
            CollectableItem newItem = CollectableItemFactory.CreateRandomCollectableItem(this.GetRandomFreePosition());
            this._collectableItems.Add(newItem);

            GameboardView.Instance.DrawCollectableItem(newItem);          
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
            while (SnakeModel.Instance.Tail.Any(s => s.CurrentPosition == freePosition) ||
                   SnakeModel.Instance.Head.CurrentPosition == freePosition);

            return freePosition;
        }

        private bool HasSnakeCollectedItem(CollectableItem item)
        {
            return SnakeModel.Instance.Head.CurrentPosition == item.Position;
        }

        private void RemoveColectableItem()
        {
            GameboardView.Instance.EraseCollectableItem(this._collectableItems[0]);
            this._collectableItems.Remove(this._collectableItems[0]);
        }

        private bool HasHeadCollidedWithTail()
        {
            return SnakeModel.Instance.Tail.Any(s => s.CurrentPosition == SnakeModel.Instance.Head.CurrentPosition);
        }
    }
}
