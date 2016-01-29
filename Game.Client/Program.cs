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
			var windowHeight = 30;
			var windowWidth = 80;

            do
            {
                Console.Write("> ");
                input.Read(Console.ReadLine());
				if (Console.WindowHeight != windowHeight || Console.WindowWidth != windowWidth)
				{
					Console.WriteLine("Resseting to {0} x {1}", windowWidth, windowHeight);
					Console.SetWindowSize(windowWidth, windowHeight);
				}
            } while (game.IsRunning);

            commandsFileStream.Close();
            storyStepsFileStream.Close();
        }
    }
}
