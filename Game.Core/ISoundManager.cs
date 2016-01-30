namespace Game.Core
{
    public interface ISoundManager
    {
        void PlaySound(string soundFile);
        void StopSound();
        void PlayLoop(string soundFile);
        void StopLoop();
    }
}
