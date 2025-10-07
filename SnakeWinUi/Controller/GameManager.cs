using Microsoft.UI.Xaml.Media;

using SnakeWinUi.Config;
using SnakeWinUi.MVVM.Model.Entity.Snake;
using SnakeWinUi.MVVM.View;
using SnakeWinUi.Services.UpdateService;

using System;

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

        private DateTime _lastUpdate { get; set; }
        private bool _isRunning { get; set; } = false;

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
            this._isRunning = true;
        }

        /// <summary>
        /// Pauses the game.
        /// The update loop stops until StartGame is called again.
        /// </summary>
        public void PauseGame()
        {
            this._isRunning = false;
        }

        /// <summary>
        /// Internal event handler for CompositionTarget.Rendering.
        /// Calculates elapsed time since the last update and calls Update() if enough time has passed.
        /// </summary>
        private void OnRendering(object? sender, object e)
        {
            if (!this._isRunning)
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
    }
}
