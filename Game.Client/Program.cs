using System;

namespace Game.Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var game = new Core.Game(new ConsoleInput(), new ConsoleOutput());
            game.Start();

            Console.ReadKey(false);
        }
    }
}
