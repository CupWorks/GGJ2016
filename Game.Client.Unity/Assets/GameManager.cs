using System.Collections.Generic;
using System.IO;
using Source.Configuration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOutput;
    public GameObject HistoryOutput;
    public GameObject UserInput;

    public GameObject BackgroundAudioSource;
    public GameObject EffectAudioSource;

    private ConfigurationContainer<Audio> AudioContainer { get; set; }
    private ConfigurationContainer<Command> CommandContainer { get; set; }
    private IEnumerable<Command> DefaultCommands { get; set; }
    private ConfigurationContainer<StoryStep> StoryStepContainer { get; set; }

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
        if (input == "play")
        {
            PlayLoop("blind");
        } else if (input == "plays")
        {
            PlaySound("testsound");
        }
		GameOutput.GetComponent<Text>().text += input + "\n";
	}

    private string CleanText(string text)
    {
        return text
            .Replace("\t", "")
            .Replace("\n", "")
            .Replace("{br}", "\n");
    }

    private void PlaySound(string key)
    {
        PlayAudio(key, EffectAudioSource);
    }

    private void PlayLoop(string key)
    {
        PlayAudio(key, BackgroundAudioSource);
    }

    private void PlayAudio(string key, GameObject audioSourceObject)
    {
        var audioResource = AudioContainer.Get(key);
        AudioClip audioFile = Resources.Load(audioResource.File, typeof(AudioClip)) as AudioClip;
        AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
        audioSource.clip = audioFile;
        audioSource.Play();
    }
}
