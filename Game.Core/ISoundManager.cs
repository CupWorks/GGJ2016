namespace Game.Core
{
    public interface ISoundManager
    {
        void PlayLoop(string soundFile);
        void StopLoop();
    }
}
