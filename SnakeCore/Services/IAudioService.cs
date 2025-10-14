using SnakeCore.Enums;

namespace SnakeCore.Services
{
    /// <summary>
    /// Provides audio functionality for the game, including playing sound effects and music.
    /// </summary>
    public interface IAudioService
    {
        /// <summary>
        /// Plays a specific sound effect.
        /// </summary>
        /// <param name="soundEffect">The sound effect to play.</param>
        void PlayEffect(SoundEffectType soundEffect);

        /// <summary>
        /// Starts playing background music for the game.
        /// </summary>
        /// <param name="gameMusic">The type of music to play.</param>
        void PlayMusic(GameMusicType gameMusic);

        /// <summary>
        /// Stops any currently playing background music.
        /// </summary>
        void StopMusic();
    }
}
