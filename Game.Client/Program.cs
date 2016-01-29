using System;

namespace Game.Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var input = new ConsoleInput();
            var game = new Core.Game(input, new ConsoleOutput());
            game.Start();

            do
            {
                input.Read(Console.ReadLine());
            } while (game.IsRunning);


            Console.ReadKey(false);
        }
    }
}
