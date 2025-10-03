using Microsoft.UI.Xaml.Media;
using SnakeWinUi.Config;
using SnakeWinUi.MVVM.Model;
using SnakeWinUi.MVVM.View;
using SnakeWinUi.Services.UpdateService;
using System;

namespace SnakeWinUi.Controller
{
    internal class GameManager
    {
        private UpdateGroup _updateGroup { get; set; }

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
            this._updateGroup = new UpdateGroup();
            this._lastUpdate = DateTime.Now;

            CompositionTarget.Rendering += this.OnRendering;
        }

        /// <summary>
        /// Ensures to initialize lazy singleton instances
        /// </summary>
        public void Init()
        {
            _ = SnakeModel.Instance;
            _ = GameboardView.Instance;
        }

        public void StartGame()
        {
            this._isRunning = true;
        }

        public void PauseGame()
        {
            this._isRunning = false;
        }

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

        public void Update()
        {
            this._updateGroup.Update();
        }

        public void AddToUpdateGroup(IUpdateEntity participant)
        {
            this._updateGroup.AddParticipant(participant);
        }
    }
}
