using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    

	// Use this for initialization
	public void Start () {
	
	}
	
	// Update is called once per frame
	public void Update () {
	
	}

	public void GetInput (string Input)
	{
        GameObject historyLog = GameObject.FindWithTag("HistoryLog");
        string tempLog = historyLog.GetComponent<Text>().text;
        tempLog += Input + "\n";
        historyLog.GetComponent<Text>().text = tempLog;

        GameObject userInput = GameObject.FindWithTag("UserInput");
        userInput.GetComponent<Text>().text = "";
	}
}
