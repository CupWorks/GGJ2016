using System.Collections.Generic;
using System.IO;
using System.Xml;
using Source.Configuration;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ConfigurationContainer<Audio> AudioContainer { get; set; }
    private ConfigurationContainer<Command> CommandContainer { get; set; }
    private IEnumerable<Command> DefaultCommands { get; set; }
    private ConfigurationContainer<StoryStep> StoryStepContainer { get; set; }

    protected void Start()
    {
        var commandsData = Resources.Load("Commands", typeof(TextAsset)) as TextAsset;
        var audioFilesData = Resources.Load("AudioFiles", typeof(TextAsset)) as TextAsset;
        var storyStepsData = Resources.Load("StorySteps", typeof(TextAsset)) as TextAsset;

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
    }

	public void GetInput(string input)
	{
        var historyLog = GameObject.FindWithTag("HistoryLog");
        var tempLog = historyLog.GetComponent<Text>().text;
        tempLog += input + "\n";
        historyLog.GetComponent<Text>().text = tempLog;
	}
}
