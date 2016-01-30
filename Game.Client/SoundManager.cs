using System.Media;
using Game.Core;

namespace Game.Client
{
    public class SoundManager : ISoundManager
    {
        private SoundPlayer EffectPlayer;
        private SoundPlayer BackgroundPlayer;

        public SoundManager()
        {
            EffectPlayer = new SoundPlayer();
            BackgroundPlayer = new SoundPlayer();
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
