﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1Manage : MonoBehaviour {

	private int state = 0;
	private int magicNodes = 0;

	public GameObject spiderText;
	public GameObject wormText;
	public GameObject trojanText;
	public GameObject firewallText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(state == 0)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if(ConsoleHandler.instance.done && state <= 4 && Input.GetMouseButtonDown(0))
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
            state++;
        }
		if(state == 4)
		{
			spiderText.GetComponent<CampaignUISwitch>().SetActive();
		}
		if (state == 5 && GameController.instance.player.discoveredNodes.Count > 1)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
			GameController.instance.player.winCondition = () => { return false; };
			GameController.instance.enemy.winCondition = () => { return false; };
			wormText.GetComponent<CampaignUISwitch>().SetActive();
		}
		if(state == 6 && GameController.instance.player.ownedNodes.Count > 1)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
		}
		if(state == 7 && GameController.instance.player.discoveredNodes.Where(x => x.hasFirewall).Any())
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
			magicNodes = GameController.instance.player.ownedNodes.Count;
			trojanText.GetComponent<CampaignUISwitch>().SetActive();
		}
		if(state == 8 && GameController.instance.player.ownedNodes.Count > magicNodes)
		{
			ConsoleHandler.instance.RunConsoleSequence(state);
			state++;
			firewallText.GetComponent<CampaignUISwitch>().SetActive();
		}
		if(state == 9)
		{
			if(GameController.instance.player.ownedNodes.Count == (NetworkController.instance.nodes.Count - 1))
			{
				ConsoleHandler.instance.RunConsoleSequence(state);
				state++;
			}
		}
		if (state == 10 && GameTime.pause == false)
			SceneManager.LoadScene("Level2");

	}
}
