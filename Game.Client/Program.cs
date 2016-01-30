using System;
using System.IO;

namespace Game.Client
{
    public class Program
    {
		private const int WindowHeight = 30;
		private const int WindowWidth  = 80;

        private static void Main(string[] args)
        {
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
			if (Console.WindowHeight != WindowHeight || Console.WindowWidth != WindowWidth)
			{
				Console.SetWindowSize(WindowWidth, WindowHeight);
			}
		}
    }
}
