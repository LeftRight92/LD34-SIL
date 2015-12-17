using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class Level2Manage : MonoBehaviour {

	int state = 0;

	public CampaignUISwitch compression;
	public CampaignUISwitch learning;


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
		if(GameController.instance.player.ownedNodes.Count >= 7 && state == 2)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
			compression.SetActive();
			learning.SetActive();
		}
		if (GameController.instance.player.discoveredNodes.Where(x => GameController.instance.enemy.ownedNodes.Contains(x)).Any()
			&& state == 3)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if (GameController.instance.player.discoveredNodes.Where(x => x == NetworkController.instance.enemyStart).Any()
			&& state == 4)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if (GameController.instance.player.ownedNodes.Count == (NetworkController.instance.nodes.Count) && state == 5)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if(state == 6 && GameTime.pause == false)
			SceneManager.LoadScene("Level3");
	}
}
