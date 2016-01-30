namespace Game.Core
{
    public interface ISoundManager
    {
        void PlaySound(string soundFile);
        void PlayLoop(string soundFile);
    }
}
