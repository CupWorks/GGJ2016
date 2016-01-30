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

        public void StopSound()
        {
            EffectPlayer.Stop();
        }

        public void PlayLoop(string soundFile)
        {
            BackgroundPlayer.SoundLocation = soundFile;
            BackgroundPlayer.PlayLooping();
        }

        public void StopLoop()
        {
            BackgroundPlayer.Stop();
        }
    }
}
