using System;
using System.IO;

namespace Game.Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var commandsFileStream = new FileStream("Commands.xml", FileMode.Open);
            var storyStepsFileStream = new FileStream("StorySteps.xml", FileMode.Open);

            var input = new ConsoleInput();
            var game = new Core.Game(
                input,
                new ConsoleOutput(),
                commandsFileStream,
                storyStepsFileStream);
            game.Start();

            do
            {
                Console.Write("> ");
                input.Read(Console.ReadLine());
            } while (game.IsRunning);

            commandsFileStream.Close();
            storyStepsFileStream.Close();

            Console.ReadKey(false);
        }
    }
}
