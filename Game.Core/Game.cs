using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Game.Core.Configuration;
using Action = Game.Core.Configuration.Action;
using System.Text.RegularExpressions;

// This is not the gratest code in the world ...
// This is just a tribute
// Gotta remember the greatest code in the world ...
namespace Game.Core
{
    public class Game
    {
		private int InputTryCounter { get; set; } = 0;

        private IInput Input { get; }
        private IOutput Output { get; }
        private ISoundManager SoundManager { get; }
        private ConfigurationContainer<Defaults> DefaultsContainer { get; }
        private ConfigurationListContainer<Sound> AudioListContainer { get; }
        private ConfigurationListContainer<Command> CommandListContainer { get; }
        private IEnumerable<Command> DefaultCommands { get; } 
        private ConfigurationListContainer<StoryStep> StoryStepListContainer { get; }

        private string CurrentStoryStepKey { get; set; }

        public bool IsRunning { get; set; } = false;

        public Game(IInput input, IOutput output, ISoundManager soundManager, Stream defaultsStream, Stream audioStream, Stream commandsStream, Stream storyStepsStream)
        {
            Input = input;
            Input.OnTextReceived += InputOnOnTextReceived;
            Output = output;
            SoundManager = soundManager;
            DefaultsContainer = new ConfigurationContainer<Defaults>(defaultsStream);
            DefaultsContainer.ReadFromStream();
            AudioListContainer = new ConfigurationListContainer<Sound>(audioStream);
            AudioListContainer.ReadFromStream();
            CommandListContainer = new ConfigurationListContainer<Command>(commandsStream);
            CommandListContainer.ReadFromStream();
            DefaultCommands = CommandListContainer.Get(c => c.IsDefault);
            StoryStepListContainer = new ConfigurationListContainer<StoryStep>(storyStepsStream);
            StoryStepListContainer.ReadFromStream();
        }

        private void InputOnOnTextReceived(string text)
        {
            //check for default command
            foreach (var defaultCommand in DefaultCommands)
            {
                if (!defaultCommand.WordList.Contains(text.Trim().ToLower())) continue;
                PerformDefaultCommand(defaultCommand.Key);
                return;
            }

            //check for action
            var storyStep = StoryStepListContainer.Get(CurrentStoryStepKey);
            foreach (var action in storyStep.ActionList)
            {
                var command = CommandListContainer.Get(action.Command);
                if (!command.WordList.Contains(text.Trim().ToLower())) continue;
                InputTryCounter = 0;
                PerformAction(action);
                return;
            }

			if (InputTryCounter >= DefaultsContainer.Object.InputTries)
			{
			    if (ShowHelpText(storyStep.HelpText)) return;
			}

            InputTryCounter++;
			Output.WriteLine(DefaultsContainer.Object.InputWarnings.GetRandomValue(), OutputType.Warning);
            Input.Request();
        }

        private bool ShowHelpText(string text)
        {
            InputTryCounter = 0;
            if (string.IsNullOrEmpty(text)) return false;

            Output.WriteLine(text, OutputType.Warning);
            Input.Request();
            return true;
        }

        public void Start()
        {
            IsRunning = true;
            UpdateStoryStep("GAME_START");
        }

        private void UpdateStoryStep(string key)
        {
            var storyStep = StoryStepListContainer.Get(key);
            if (!string.IsNullOrEmpty(storyStep.Sound))
            {
                var audioFile = AudioListContainer.Get(CleanText(storyStep.Sound));
                SoundManager.PlayLoop(audioFile.File);
            }
            DisplayTextBlocks(storyStep.Text, OutputType.Normal);
            CurrentStoryStepKey = key;
            if (!string.IsNullOrEmpty(storyStep.NextStep))
            {
                UpdateStoryStep(storyStep.NextStep);
            }
            Input.Request();
        }

        private void PerformAction(Action action)
        {
            DisplayTextBlocks(action.Text, OutputType.Action);

            if (string.IsNullOrEmpty(action.NextStep))
            {
                Input.Request();
            }
            else
            {
                UpdateStoryStep(action.NextStep);
            }
        }

        private void DisplayTextBlocks(IEnumerable<TextBlock> textBlocks, OutputType type)
        {
            Task.Run(async delegate
            {
                foreach (var textBlock in textBlocks)
                {
                    await Task.Delay(textBlock.Delay);
					WriteLine(textBlock.Content, type);
                }
            }).Wait();
        }

        private void PerformDefaultCommand(string key)
        {
            switch (key)
            {
                case "DEFAULT_EXIT":
                    IsRunning = false;
                    break;
                case "DEFAULT_HELP":
                    Output.WriteLine(DefaultsContainer.Object.InstructionText, OutputType.Warning);
                    Input.Request();
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

		private void WriteLine(string text, OutputType type)
		{
			var parts = Regex.Split(CleanText(text), "{h}");
			var count = 0;
			foreach (var part in parts)
			{
			    Output.Write(part, count%2 == 0 ? type : OutputType.Highlight);
			    count++;
			}

		    Output.Write("\n", type);
		}
    }
}
