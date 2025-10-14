using Microsoft.UI.Xaml.Media;
using System;

using SnakeCore.Controller;
using SnakeCore.Enums;
using SnakeCore.Config;
using SnakeUi.Services;
using SnakeUi.MVVM.View;
using SnakeCore.Model.Entity;
using System.Diagnostics;

namespace SnakeUi.Controller
{
    /// <summary>
    /// Singleton class that manages the game logic and updates.
    /// Handles game states (start, pause) and regularly updates
    /// all registered game participants.
    /// </summary>
    internal class GameManager
    {
        public GameEngine GameEngine { get; set; } = new(AudioManager.Instance);
        private DateTime _lastUpdate { get; set; } = DateTime.Now;

        public GameManager()
        {
            CompositionTarget.Rendering += this.OnRendering;
        }

        public void StartGame()
        {
            this.GameEngine.Run();
        }

        public void PauseGame()
        {
            this.GameEngine.Stop();
        }

        public void SetDirection(Direction newDirection)
        {
            this.GameEngine.SetDirection(newDirection);
        }

        /// <summary>
        /// Internal event handler for CompositionTarget.Rendering.
        /// Calculates elapsed time since the last update and calls Update() if enough time has passed.
        /// </summary>
        private void OnRendering(object? sender, object e)
        {
            if (this.GameEngine.GameState == GameState.Paused)
            {
                return;
            }

            var now = DateTime.Now;
            var delta = (now - this._lastUpdate).TotalMilliseconds;

            if (delta >= GameSettings.UpdateSpeedMillis)
            {
                this.GameEngine.Update();
                this._lastUpdate = now;
            }
        }
    }
 
}
