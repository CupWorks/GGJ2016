using System.Collections.Generic;
using System.IO;
using Source.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Action = System.Action;

public class GameManager : MonoBehaviour
{
    public GameObject GameOutput;
    public GameObject HistoryOutput;
    public GameObject UserInput;

    private Action _timerAction;
    private float _currentTimer = 0.0f;

    private ConfigurationContainer<Audio> AudioContainer { get; set; }
    private ConfigurationContainer<Command> CommandContainer { get; set; }
    private IEnumerable<Command> DefaultCommands { get; set; }
    private ConfigurationContainer<StoryStep> StoryStepContainer { get; set; }

	private string CurrentStoryStepKey { get; set; }

    protected void Start()
    {
        var commandsData = Resources.Load("Commands", typeof (TextAsset)) as TextAsset;
        var audioFilesData = Resources.Load("AudioFiles", typeof (TextAsset)) as TextAsset;
        var storyStepsData = Resources.Load("StorySteps", typeof (TextAsset)) as TextAsset;

        if (audioFilesData != null)
        {
            AudioContainer = new ConfigurationContainer<Audio>(new StringReader(audioFilesData.text));
        }
        if (commandsData != null)
        {
            CommandContainer = new ConfigurationContainer<Command>(new StringReader(commandsData.text));
            DefaultCommands = CommandContainer.Get(c => c.IsDefault);
        }
        if (storyStepsData != null)
        {
            StoryStepContainer = new ConfigurationContainer<StoryStep>(new StringReader(storyStepsData.text));
        }

        StartTimer(10000, () => Debug.Log("Foo"));

		GameOutput.GetComponent<Text> ().text = "";
		UpdateStoryStep("GAME_START");
    }

    protected void Update()
    {
        if (_currentTimer <= 0.0f)
        {
            if (_timerAction != null)
            {
                _timerAction.Invoke();
                _timerAction = null;
            }
        }
        else
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    private void StartTimer(int milliseconds, Action action)
    {
        _currentTimer = milliseconds / 1000.0f;
        _timerAction = action;
    }

    public void GetInput(string input)
    {
        string tempLog = HistoryOutput.GetComponent<Text>().text;
        tempLog += input + "\n";
        HistoryOutput.GetComponent<Text>().text = tempLog;

        InputField userInputField = UserInput.GetComponent<InputField>();
        userInputField.text = "";
        EventSystem.current.SetSelectedGameObject(userInputField.gameObject, null);
        PointerEventData emptyData = new PointerEventData(EventSystem.current);
        userInputField.OnPointerClick(emptyData);

        ProcessInput(input);
	}

	protected void ProcessInput(string input)
	{
		foreach (var defaultCommand in DefaultCommands)
		{
			if (defaultCommand.WordList.Contains(input.Trim().ToLower()))
			{
				//PerformDefaultCommand(defaultCommand.Key);
				return;
			}
		}

		//check for action
		var storyStep = StoryStepContainer.Get(CurrentStoryStepKey);
		foreach (var action in storyStep.ActionList)
		{
			var command = CommandContainer.Get(action.Command);
			if (command.WordList.Contains(input.Trim().ToLower()))
			{
				PerformAction(action);
				return;
			}
		}
	}

	private void PerformAction(Source.Configuration.Action action)
	{
		DisplayTextBlocks(action.Text);
		if (!string.IsNullOrEmpty(action.Sound))
		{
			var audioFile = AudioContainer.Get(CleanText(action.Sound));
			if ("sound" == audioFile.Type)
			{
				//SoundManager.PlaySound(audioFile.File);
			} else
			{
				//SoundManager.PlayLoop(audioFile.File);
			}
		}

		if (!string.IsNullOrEmpty(action.NextStep))
		{
			UpdateStoryStep(action.NextStep);
		}
	}

	private void UpdateStoryStep(string key)
	{
		var storyStep = StoryStepContainer.Get(key);
		DisplayTextBlocks(storyStep.Text);
		CurrentStoryStepKey = key;
		if (!string.IsNullOrEmpty(storyStep.NextStep))
		{
			UpdateStoryStep(storyStep.NextStep);
		}
	}

	private void DisplayTextBlocks(IEnumerable<TextBlock> textBlocks)
	{
		foreach (var textBlock in textBlocks)
		{
			GameOutput.GetComponent<Text> ().text += CleanText(textBlock.Content);
			GameOutput.GetComponent<Text> ().text += "\n";
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
