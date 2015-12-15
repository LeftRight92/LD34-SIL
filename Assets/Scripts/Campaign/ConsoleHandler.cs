using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConsoleHandler : MonoBehaviour {

	public static ConsoleHandler instance;
	public bool debug;
	public int debugNum;
	public List<ConsoleSequence> sequences;
	public bool done = true;

	void Start()
	{
		instance = this;
		if(debug)RunConsoleSequence(debugNum);
	}

	public void RunConsoleSequence(int consoleSequence)
	{
		GameTime.pause = true;
		UIController.instance.DisplayConsole(true);
		StartCoroutine(Run(sequences[consoleSequence]));
	}

	public void EndConsoleSequence()
	{
		Debug.Log("Hello");
		if (!done) return;
		UIController.instance.DisplayConsole(false);
		GameTime.pause = false;
	}

	IEnumerator Run(ConsoleSequence seq)
	{
		done = false;
		Text text = UIController.instance.GetConsoleTextElement();
		text.text = "";
		foreach(string s in seq.sequence)
		{
			foreach(char c in s)
			{
				text.text += c;
				//yield return new WaitForSeconds(0.001f);
			}
			text.text += '\n';
			yield return new WaitForSeconds(0.1f);
		}
		done = true;
	}
}
