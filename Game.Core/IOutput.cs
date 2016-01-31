namespace Game.Core
{
    public interface IOutput
    {
        void Write(string text, OutputType type);
        void WriteLine(string text, OutputType type);
    }
}
