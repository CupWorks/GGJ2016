using System;
using System.IO;

namespace Game.Client
{
    public class Program
    {

		private const int windowHeight = 30;
		private const int windowWidth  = 80;

        private static void Main(string[] args)
        {
            var commandsFileStream = new FileStream("Commands.xml", FileMode.Open);
            var storyStepsFileStream = new FileStream("StorySteps.xml", FileMode.Open);

            var input = new ConsoleInput();
            var game = new Core.Game(
                input,
                new ConsoleOutput(),
                new SoundManager(),
                commandsFileStream,
                storyStepsFileStream);
            game.Start();

			CheckWindowSize ();

            do
            {
                Console.Write("> ");
                input.Read(Console.ReadLine());
				CheckWindowSize();
            } while (game.IsRunning);

            commandsFileStream.Close();
            storyStepsFileStream.Close();
        }

		static void CheckWindowSize()
		{
			if (Console.WindowHeight != windowHeight || Console.WindowWidth != windowWidth)
			{
				Console.SetWindowSize(windowWidth, windowHeight);
			}
		}
    }
}
