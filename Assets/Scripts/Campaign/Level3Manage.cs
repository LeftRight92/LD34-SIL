using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class Level3Manage : MonoBehaviour {

	int state = 0;
	int networkSize = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if (ConsoleHandler.instance.done && state == 1 && Input.GetMouseButtonDown(0))
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if(GameController.instance.player.discoveredNodes.Where(x => x == NetworkController.instance.enemyStart).Any()
			&& state == 2)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
			networkSize = GameController.instance.player.ownedNodes.Count();
		}

		if(GameController.instance.player.ownedNodes.Count() > networkSize + 10 && state == 3)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if (GameController.instance.player.ownedNodes.Count == (NetworkController.instance.nodes.Count) && state == 5)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if (state == 6 && GameTime.pause == false)
			SceneManager.LoadScene("Freeplay");
	}
}
