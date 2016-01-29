using Game.Core;

namespace Game.Client
{
    public class ConsoleInput : IInput
    {
        public event InputEvent OnTextReceived;

        public void Read(string text)
        {
            OnTextReceived?.Invoke(text);
        }
    }
}