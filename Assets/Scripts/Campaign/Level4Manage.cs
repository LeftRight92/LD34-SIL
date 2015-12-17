using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level4Manage : MonoBehaviour
{
	int state = 0;

	public Node containment;
	public Node cable;

	void Update()
	{
		if (state == 0)
		{
			Debug.Log("Start");
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
			GameController.instance.player.See(containment);
			GameController.instance.player.Explore(containment);
			GameController.instance.player.See(cable);
			GameController.instance.player.Explore(cable);
			containment.nodeName = "Containment Unit";
			containment.transform.FindChild("Text").GetComponent<TextMesh>().text = "Containment Unit";
			cable.nodeName = "Submarine Cable";
			cable.transform.FindChild("Text").GetComponent<TextMesh>().text = "Submarine Cable";
		}
		if (GameController.instance.player.ownedNodes.Contains(containment) && state == 1)
		{
			Debug.Log("Containment End");
			ConsoleHandler.instance.RunConsoleSequence(1);
			state = 2;
		}
		if (GameController.instance.player.ownedNodes.Contains(cable) && state == 1)
		{
			Debug.Log("Cable End");
			ConsoleHandler.instance.RunConsoleSequence(2);
			state = 2;
		}
		if (state == 2 && GameTime.pause == false)
			SceneManager.LoadScene("MainMenu");
	}
}
