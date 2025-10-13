using SnakeCore.Enums;
using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace SnakeCore.Services.UpdateService
{
    internal class AudioManager
    {
        private static AudioManager? _instance;
        public static AudioManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AudioManager();
                }

                return _instance;
            }
        }

        private readonly Dictionary<SoundEffectType, string> _effectPaths;
        private readonly Dictionary<GameMusicType, string> _musicPaths;

        private MediaPlayer? _musicPlayer { get; set; }
        private readonly List<MediaPlayer> _activeEffects = new();
        private AudioManager()
        {
            this._effectPaths = this.GetEffectPaths();
            this._musicPaths = this.GetMusicPaths();
        }

        private Dictionary<SoundEffectType, string> GetEffectPaths()
        {
            return new()
            {
                { SoundEffectType.AppleCollected, "ms-appx:///Assets/Sounds/SlowDownSound.mp3" },
                { SoundEffectType.CherryCollected, "ms-appx:///Assets/Sounds/BoostSound.mp3" },
                { SoundEffectType.BombCollected, "ms-appx:///Assets/Sounds/ExplosionSound.mp3" },
                { SoundEffectType.DuckCollected, "ms-appx:///Assets/Sounds/DuckCollectedSound.mp3" },
                { SoundEffectType.CollectedItem, "ms-appx:///Assets/Sounds/PreyCollectedSound.mp3" },
            };
        }

        private Dictionary<GameMusicType, string> GetMusicPaths()
        {
            return new()
            {
                { GameMusicType.GameLoop1, "ms-appx:///Assets/Sounds/GameSoundLoop1.mp3" },
                { GameMusicType.GameLoop2, "ms-appx:///Assets/Sounds/GameSoundLoop2.mp3" }
            };
        }

        public void PlayEffect(SoundEffectType type)
        {
            if (!this._effectPaths.TryGetValue(type, out var path)) return;

            MediaPlayer player = new()
            {
                Source = MediaSource.CreateFromUri(new Uri(path)),
                Volume = 0.8
            };

            player.MediaEnded += (s, e) =>
            {
                player.Dispose();
                this._activeEffects.Remove(player);
            };

            this._activeEffects.Add(player);
            player.Play();
        }

        public void PlayMusic(GameMusicType type)
        {
            if (!this._musicPaths.TryGetValue(type, out var path)) return;

            this._musicPlayer?.Dispose();
            this._musicPlayer = new MediaPlayer
            {
                Source = MediaSource.CreateFromUri(new Uri(path)),
                IsLoopingEnabled = true,
                Volume = 0.4
            };
            this._musicPlayer.Play();
        }

        public void StopMusic()
        {
            this._musicPlayer?.Pause();
        }
    }
}

