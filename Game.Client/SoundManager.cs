using System.Media;
using Game.Core;

namespace Game.Client
{
    public class SoundManager : ISoundManager
    {
        private SoundPlayer EffectPlayer { get; } = new SoundPlayer();
        private SoundPlayer BackgroundPlayer { get; } = new SoundPlayer();

        public SoundManager()
        {
        }

        public void PlaySound(string soundFile)
        {
            EffectPlayer.SoundLocation = soundFile;
            EffectPlayer.Play();
        }

        public void PlayLoop(string soundFile)
        {
            BackgroundPlayer.SoundLocation = soundFile;
            BackgroundPlayer.PlayLooping();
        }
    }
}
