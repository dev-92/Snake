using Microsoft.UI.Xaml;
using SnakeWinUi.MVVM.Model;
using SnakeWinUi.MVVM.View;
using SnakeWinUi.UpdateService;
using System;

namespace SnakeWinUi.Controller
{
    internal class GameManager
    {
        private const int UPDATE_SPEED_MILLIS = 1000;
        private UpdateGroup _updateGroup { get; set; }

        public DispatcherTimer GameTimer { get; set; }

        private static GameManager? _instance;
        public static GameManager Instance
        {
            get
            {
                if(GameManager._instance == null)
                {
                    GameManager._instance = new GameManager();
                }

                return GameManager._instance;
            }
        }

        private GameManager()
        {
            this._updateGroup = new UpdateGroup();

            this.GameTimer = new DispatcherTimer();
            this.GameTimer.Interval = TimeSpan.FromMilliseconds(GameManager.UPDATE_SPEED_MILLIS);
            this.GameTimer.Tick += (s, e) => this.Update();
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
            this.GameTimer.Start();
        }

        public void PauseGame()
        {
            this.GameTimer.Stop();
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
