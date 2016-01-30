using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Game.Core.Configuration;
using Action = Game.Core.Configuration.Action;

namespace Game.Core
{
    public class Game
    {
        private List<string> DefaultInputWarnings { get; } = new List<string>
        {
                "You can't do that here",
                "Learn to play",
                "I am your father ... Luke"
        };

        private IInput Input { get; }
        private IOutput Output { get; }
        private ISoundManager SoundManager { get; }
        private ConfigurationContainer<Audio> AudioContainer { get; }
        private ConfigurationContainer<Command> CommandContainer { get; }
        private IEnumerable<Command> DefaultCommands { get; } 
        private ConfigurationContainer<StoryStep> StoryStepContainer { get; }

        private string CurrentStoryStepKey { get; set; }

        public bool IsRunning { get; set; } = false;

        public Game(IInput input, IOutput output, ISoundManager soundManager, Stream audioStream, Stream commandsStream, Stream storyStepsStream)
        {
            Input = input;
            Input.OnTextReceived += InputOnOnTextReceived;
            Output = output;
            SoundManager = soundManager;
            AudioContainer = new ConfigurationContainer<Audio>(audioStream);
            AudioContainer.ReadFromStream();
            CommandContainer = new ConfigurationContainer<Command>(commandsStream);
            CommandContainer.ReadFromStream();
            DefaultCommands = CommandContainer.Get(c => c.IsDefault);
            StoryStepContainer = new ConfigurationContainer<StoryStep>(storyStepsStream);
            StoryStepContainer.ReadFromStream();
        }

        private void InputOnOnTextReceived(string text)
        {
            //check for default command
            foreach (var defaultCommand in DefaultCommands)
            {
                if (defaultCommand.WordList.Contains(text.Trim().ToLower()))
                {
                    PerformDefaultCommand(defaultCommand.Key);
                    return;
                }
            }

            //check for action
            var storyStep = StoryStepContainer.Get(CurrentStoryStepKey);
            foreach (var action in storyStep.ActionList)
            {
                var command = CommandContainer.Get(action.Command);
                if (command.WordList.Contains(text.Trim().ToLower()))
                {
                    PerformAction(action);
                    return;
                }
            }

            //write waring text
            Output.WriteLine(DefaultInputWarnings.GetRandomValue(), OutputType.Warning);
            Input.Request();
        }

        public void Start()
        {
            IsRunning = true;
            UpdateStoryStep("GAME_START");
        }

        private void UpdateStoryStep(string key)
        {
            var storyStep = StoryStepContainer.Get(key);
            foreach (var textBlock in storyStep.Text)
            {
                Output.WriteLine(CleanText(textBlock.Content), OutputType.Normal);
            }
            CurrentStoryStepKey = key;
            if (!string.IsNullOrEmpty(storyStep.NextStep))
            {
                UpdateStoryStep(storyStep.NextStep);
            }
            Input.Request();
        }

        private void PerformAction(Action action)
        {
            Output.WriteLine(CleanText(action.Text), OutputType.Action);

            if (!string.IsNullOrEmpty(action.Sound))
            {
                var audioFile = AudioContainer.Get(CleanText(action.Sound));
                if ("sound" == audioFile.Type)
                {
                    SoundManager.PlaySound(audioFile.File);
                } else
                {
                    SoundManager.PlayLoop(audioFile.File);
                }
            }

            if (string.IsNullOrEmpty(action.NextStep))
            {
                Input.Request();
            }
            else
            {
                UpdateStoryStep(action.NextStep);
            }
        }

        private void PerformDefaultCommand(string key)
        {
            switch (key)
            {
                case "DEFAULT_EXIT":
                    IsRunning = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }

        private string CleanText(string text)
        {
            return text
                .Replace("\t", "")
                .Replace("\n", "")
                .Replace("{br}", "\n");
        }
    }
}
