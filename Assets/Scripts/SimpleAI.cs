﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleAI : MonoBehaviour {

	PlayerController controller;
	bool alive = true;
	bool go = false;
	float waitTime;

	// Use this for initialization
	void Start () {
		controller = GameController.instance.enemy;
	}

	public void StartAI()
	{
		go = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (waitTime > 0) waitTime -= GameTime.deltaTime;
		if(waitTime <= 0 && go && alive)
		{
			List<Node> chosenNodes = new List<Node>();
			if (controller.ownedNodes.Count == 0) alive = false;
			int desiredNodes = Mathf.CeilToInt(controller.ownedNodes.Count * 0.66f);
			for (int x = 0; x < controller.ownedNodes.Count; x++)
				if (Random.value < (desiredNodes / (controller.ownedNodes.Count - x)))
				{
					desiredNodes--;
					chosenNodes.Add(controller.ownedNodes[x]);
				}
			foreach (Node n in chosenNodes)
			{
				if (Random.value < 0.01f) n.CreateFirewall();
				List<Node> o = n.neighbours.Where(
						p => p.team != Team.ENEMY &&
						p.hasFirewall &&
						controller.discoveredNodes.Contains(p)
					).ToList();
				if (o.Count != 0)
				{
					Node p = o.First();
					if (n.type == NodeType.ANTIMALWARE) n.RunAlgorithm(NodeType.DEFAULT);
					controller.RunProgram(n, ProgramType.TROJAN, new Node[] { p });
					break;
				}
				o = n.neighbours.Where(
						p => p.team != Team.ENEMY &&
						controller.discoveredNodes.Contains(p)
					).ToList();
				if (o.Count != 0)
				{
					Node p = o.First();
					controller.RunProgram(n, ProgramType.WORM, new Node[] { p });
					break;
				}
				o = n.neighbours.Where(
						p => !controller.discoveredNodes.Contains(p)
					).ToList();
				if (o.Count != 0)
				{
					Node p = o.First();
					controller.RunProgram(n, ProgramType.SPIDER, new Node[] { p });
					break;
				}
				controller.RunAlgorithm(n, NodeType.ANTIMALWARE);
			}
			waitTime = Random.Range(7, 13);
		}
	}
}
