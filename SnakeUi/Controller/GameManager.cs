using Microsoft.UI.Xaml.Media;

using SnakeCore.Controller;
using SnakeCore.Enums;
using SnakeCore.Config;
using SnakeUi.Services;
using SnakeCore.Model.Entity;
using System.Collections.Generic;

using System;
using Windows.UI.Notifications;


namespace SnakeUi.Controller
{
    /// <summary>
    /// Singleton class that manages the game logic and updates.
    /// Handles game states (start, pause) and regularly updates all registered game participants.
    /// </summary>
    internal class GameManager
    {
        private GameEngine _gameEngine { get; set; } = new(AudioManager.Instance);
        private DateTime _lastUpdate { get; set; } = DateTime.Now;
        public List<CellModel> Cells
        {
            get => this._gameEngine.GameboardModel.Cells;
        }
        public InfoboardModel InfoboardModel { get; private set; }

        public GameManager()
        {
            this.InfoboardModel = this._gameEngine.InfoboardModel;
            CompositionTarget.Rendering += this.OnRendering;
        }

        /// <summary>
        /// Starts the game by running the game engine.
        /// </summary>
        public void StartGame()
        {
            this._gameEngine.Run();
        }

        /// <summary>
        /// Pauses the game by stopping the game engine.
        /// </summary>
        public void PauseGame()
        {
            this._gameEngine.Stop();
        }

        /// <summary>
        /// Sets a new movement direction for the snake.
        /// </summary>
        /// <param name="newDirection">The desired direction.</param>
        public void SetDirection(Direction newDirection)
        {
            this._gameEngine.SetDirection(newDirection);
        }

        /// <summary>
        /// Internal event handler for <see cref="CompositionTarget.Rendering"/>.
        /// Calculates elapsed time since the last update and calls <see cref="GameEngine.Update"/> if enough time has passed.
        /// </summary>
        private void OnRendering(object? sender, object e)
        {
            if (this._gameEngine.GameState == GameState.Paused)
            {
                return;
            }

            var now = DateTime.Now;
            var delta = (now - this._lastUpdate).TotalMilliseconds;

            if (delta >= GameSettings.UpdateSpeedMillis)
            {
                this._gameEngine.Update();
                this._lastUpdate = now;
            }
        }
    }
}
