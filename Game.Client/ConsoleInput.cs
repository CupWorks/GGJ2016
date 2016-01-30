using System;
using Game.Core;

namespace Game.Client
{
    public class ConsoleInput : IInput
    {
        public event InputEvent OnTextReceived;
        public void Request()
        {
            Console.Write("> ");
            OnTextReceived?.Invoke(Console.ReadLine());
        }
    }
}