using System.IO;
using Game.Core.Configuration;

namespace Game.Core
{
    public class Game
    {
        private IInput Input { get; set; }
        private IOutput Output { get; set; }
        private ConfigurationContainer<Command> CommandContainer { get; set; }
        private ConfigurationContainer<StoryStep> StoryStepContainer { get; set; }

        public bool IsRunning { get; set; } = false;

        public Game(IInput input, IOutput output, Stream commandsStream, Stream storyStepsStream)
        {
            Input = input;
            Input.OnTextReceived += InputOnOnTextReceived;
            Output = output;
            CommandContainer = new ConfigurationContainer<Command>(commandsStream);
            CommandContainer.ReadFromStream();
            StoryStepContainer = new ConfigurationContainer<StoryStep>(storyStepsStream);
            StoryStepContainer.ReadFromStream();
        }

        private void InputOnOnTextReceived(string text)
        {
            Output.Write("... " + text, OutputType.Normal);
        }

        public void Start()
        {
            IsRunning = true;
            Output.Write("Start game \\o/", OutputType.System);
        }
    }
}
