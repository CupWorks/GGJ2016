using IrrKlang;
using Game.Core;


namespace Game.Client
{
    public class SoundManager : ISoundManager
    {
        private ISoundEngine SoundEngine { get; } = new ISoundEngine();
        private ISound CurrentEffect { get; set; }
        private ISound CurrentBackground { get; set; }

        public SoundManager()
        {
        }

        public void PlaySound(string soundFile)
        {
            CurrentEffect = SoundEngine.Play2D(soundFile, false);
        }

        public void StopSound()
        {
            CurrentEffect.Stop();
        }

        public void PlayLoop(string soundFile)
        {
            CurrentBackground = SoundEngine.Play2D(soundFile, true);
        }

        public void StopLoop()
        {
            CurrentBackground.Stop();
        }
    }
}
