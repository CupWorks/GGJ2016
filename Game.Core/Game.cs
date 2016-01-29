using System.IO;
using System.Linq;
using Game.Core.Configuration;

namespace Game.Core
{
    public class Game
    {
        private IInput Input { get; set; }
        private IOutput Output { get; set; }
        private ConfigurationContainer<Command> CommandContainer { get; set; }
        private ConfigurationContainer<StoryStep> StoryStepContainer { get; set; }

        private string CurrentStoryStepKey { get; set; }

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
            var storyStep = StoryStepContainer.Get(CurrentStoryStepKey);
            foreach (var action in 
                from action 
                in storyStep.CommandActionList
                let command = CommandContainer.Get(action.Command)
                where command.WordList.Contains(text.Trim().ToLower())
                select action)
            {
                PerformAction(action);
            }
        }

        public void Start()
        {
            IsRunning = true;
            Output.WriteLine("Start game \\o/", OutputType.System);
            UpdateStoryStep("GAME_START");
        }

        private void UpdateStoryStep(string key)
        {
            var storyStep = StoryStepContainer.Get(key);
            Output.WriteLine(storyStep.Text, OutputType.Normal);
            CurrentStoryStepKey = key;
        }

        private void PerformAction(Action action)
        {
            Output.WriteLine(action.Text, OutputType.Action);

            if (string.IsNullOrEmpty(action.NextStep)) return;

            UpdateStoryStep(action.NextStep);
        }
    }
}
