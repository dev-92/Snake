using Windows.Media.Core;
using Windows.Media.Playback;

using SnakeCore.Enums;
using SnakeCore.Services;

using System;
using System.Collections.Generic;

namespace SnakeUi.Services
{
    /// <summary>
    /// Singleton class that manages all game audio.
    /// Provides functionality to play sound effects and background music.
    /// Implements the <see cref="IAudioService"/> interface.
    /// </summary>
    internal class AudioManager : IAudioService
    {
        private static AudioManager? _instance;

        /// <summary>
        /// Gets the singleton instance of the <see cref="AudioManager"/>.
        /// </summary>
        public static AudioManager Instance
        {
            get
            {
                if (AudioManager._instance == null)
                {
                    AudioManager._instance = new AudioManager();
                }
                return AudioManager._instance;
            }
        }

        private readonly Dictionary<SoundEffectType, string> _effectPaths;
        private readonly Dictionary<GameMusicType, string> _musicPaths;

        private MediaPlayer? _musicPlayer { get; set; }
        private GameMusicType? _currentMusicType = null;
        private readonly List<MediaPlayer> _activeEffects = new();

        private AudioManager()
        {
            this._effectPaths = this.GetEffectPaths();
            this._musicPaths = this.GetMusicPaths();
        }

        /// <summary>
        /// Returns a mapping of all sound effect types to their corresponding file paths.
        /// </summary>
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

        /// <summary>
        /// Returns a mapping of all background music types to their corresponding file paths.
        /// </summary>
        private Dictionary<GameMusicType, string> GetMusicPaths()
        {
            return new()
            {
                { GameMusicType.GameLoop, "ms-appx:///Assets/Sounds/GameLoop.mp3" },
                { GameMusicType.MenuLoop, "ms-appx:///Assets/Sounds/MenuLoop.mp3" }
            };
        }

        /// <summary>
        /// Plays the specified sound effect once.
        /// </summary>
        /// <param name="type">The type of sound effect to play.</param>
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

        /// <summary>
        /// Prüft, ob gerade die gewünschte Musik schon läuft.
        /// </summary>
        /// <param name="type">Der Typ der Musik, die geprüft werden soll.</param>
        /// <returns>True, wenn die Musik bereits läuft, sonst false.</returns>
        private bool IsMusicAlreadyPlaying(GameMusicType type)
        {
            return this._currentMusicType.HasValue
                   && this._currentMusicType.Value == type
                   && this._musicPlayer != null
                   && this._musicPlayer.CurrentState == MediaPlayerState.Playing;
        }

        /// <summary>
        /// Plays the specified background music in a loop, replacing any currently playing music.
        /// If the requested music is already playing, it continues without restarting.
        /// </summary>
        /// <param name="type">The type of background music to play.</param>
        public void PlayMusic(GameMusicType type)
        {
            if (this.IsMusicAlreadyPlaying(type))return;

            if (!this._musicPaths.TryGetValue(type, out var path)) return;

            this._musicPlayer?.Dispose();
            this._musicPlayer = new MediaPlayer
            {
                Source = MediaSource.CreateFromUri(new Uri(path)),
                IsLoopingEnabled = true,
                Volume = 0.4
            };
            this._musicPlayer.Play();
            this._currentMusicType = type;
        }

        /// <summary>
        /// Stops the currently playing background music, if any.
        /// </summary>
        public void StopMusic()
        {
            this._musicPlayer?.Pause();
            this._currentMusicType = null;
        }
    }
}
