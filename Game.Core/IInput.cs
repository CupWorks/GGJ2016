namespace Game.Core
{
    public delegate void InputEvent(string text);

    public interface IInput
    {
        event InputEvent OnTextReceived;
        void Read(string text);
    }
}
