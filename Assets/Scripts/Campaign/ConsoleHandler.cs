using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ConsoleHandler : MonoBehaviour {

	public List<ConsoleSequence> sequences;

	void Start()
	{
		RunConsoleSequence(0);
	}

	void RunConsoleSequence(int consoleSequence)
	{
		GameTime.pause = true;
		UIController.instance.DisplayConsole(true);
		StartCoroutine(Run(sequences[consoleSequence]));
	}

	void EndConsoleSequence()
	{
		UIController.instance.DisplayConsole(false);
		GameTime.pause = true;
	}

	IEnumerator Run(ConsoleSequence seq)
	{
		Text text = UIController.instance.GetConsoleTextElement();
		foreach(string s in seq.sequence)
		{
			foreach(char c in s)
			{
				text.text += c;
				yield return new WaitForSeconds(0.01f);
			}
			text.text += '\n';
			yield return new WaitForSeconds(1);
		}
	}
}
