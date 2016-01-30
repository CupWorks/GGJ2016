using System;
using System.IO;

namespace Game.Client
{
    public class Program
    {
		private const int WindowHeight = 50;
		private const int WindowWidth  = 100;

        private static void Main(string[] args)
        {
            CheckWindowSize();
            var commandsFileStream = new FileStream("Files/Commands.xml", FileMode.Open);
            var storyStepsFileStream = new FileStream("Files/StorySteps.xml", FileMode.Open);

            var input = new ConsoleInput();
            var game = new Core.Game(
                input,
                new ConsoleOutput(),
                new SoundManager(),
                commandsFileStream,
                storyStepsFileStream);
            game.Start();
            do
            {
				CheckWindowSize();
            } while (game.IsRunning);

            commandsFileStream.Close();
            storyStepsFileStream.Close();
        }

		private static void CheckWindowSize()
		{
		    if (Console.WindowHeight == WindowHeight && Console.WindowWidth == WindowWidth) return;

          //  Console.SetBufferSize(WindowWidth, WindowWidth + 200);
            Console.SetWindowSize(WindowWidth, WindowHeight);
		}
    }
}
