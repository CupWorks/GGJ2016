using System;
using Game.Core;

namespace Game.Client
{
    public class ConsoleOutput : IOutput
    {
        public void Write(string text, OutputType type)
        {
            switch (type)
            {
                case OutputType.System:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case OutputType.Normal:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}