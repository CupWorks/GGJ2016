using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GameManager : MonoBehaviour {
    
	public GameObject gameOutput;
    public GameObject historyOutput;
    public GameObject userInput;
	// Use this for initialization
	public void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	public void GetInput (string Input)
	{
        string tempLog = historyOutput.GetComponent<Text>().text;
        tempLog += Input + "\n";
        historyOutput.GetComponent<Text>().text = tempLog;

        InputField userInputField = userInput.GetComponent<InputField>();
        userInputField.text = "";
        EventSystem.current.SetSelectedGameObject(userInputField.gameObject, null);
        PointerEventData emptyData = new PointerEventData(EventSystem.current);
        userInputField.OnPointerClick(emptyData);



        gameOutput.GetComponent<Text>().text = Input + "\n";
	}
}
