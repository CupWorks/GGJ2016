using System;
using Game.Core;

namespace Game.Client
{
    public class ConsoleOutput : IOutput
    {
        public void Write(string text, OutputType type)
        {
            Console.ForegroundColor = GetColor(type);
            Console.Write(text);
            Console.ResetColor();
        }

        public void WriteLine(string text, OutputType type)
        {
            Console.ForegroundColor = GetColor(type);
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private ConsoleColor GetColor(OutputType type)
        {
            switch (type)
            {
                case OutputType.System:
                    return ConsoleColor.Cyan;
                case OutputType.Normal:
                    return ConsoleColor.White;
                case OutputType.Action:
                    return ConsoleColor.Green;
                case OutputType.Warning:
                    return ConsoleColor.Yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}