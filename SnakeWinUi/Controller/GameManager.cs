using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using SnakeViewModel.ViewModel;
using SnakeUi.Config;
using SnakeUi.Enums;
using SnakeUi.MVVM.Model.Entity.Collectables;
using SnakeUi.MVVM.Model.Entity.Snake;
using SnakeUi.MVVM.Model.ValueObject;
using SnakeUi.MVVM.View;
using SnakeUi.Services.Audio;
using SnakeUi.Services.UpdateService;

using System;
using System.Collections.Generic;
using System.Linq;
using SnakeCore.Controller;
using SnakeCore.Services.UpdateService;
using SnakeCore.Enums;
using SnakeCore.Config;

namespace SnakeUi.Controller
{
    /// <summary>
    /// Singleton class that manages the game logic and updates.
    /// Handles game states (start, pause) and regularly updates
    /// all registered game participants.
    /// </summary>
    internal class GameManager
    {
        public GameboardView GameboardView { get; private set; }
        private GameEngine _gameEngine { get; set; } = new(AudioManager.Instance, new GameboardView());

        private InfoboardViewModel _infoboardViewModel { get; set; }

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
             = new(AudioManager.Instance, this.GameboardView);
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
                this._gameEngine.Update();
                this._lastUpdate = now;
            }
        }
    }
 
}
