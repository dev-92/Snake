using SnakeCore.Enums;

namespace SnakeCore.Services
{
    public interface IAudioService
    {
        public void PlayEffect(SoundEffectType soundEffect);
        public void PlayMusic(GameMusicType gameMusic);
        public void StopMusic();
    }
}
