using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class ComputerPlayerController : PlayerController
{
	void Start()
	{
		team = Team.PLAYER;
	}

	public override void Explore(Node node)
	{
		discoveredNodes.Add(node);
		foreach (Node n in node.neighbours.Where(n => !discoveredNodes.Contains(n)))
			See(n);
	}

	public override void See(Node node)
	{
		seenNodes.Add(node);
	}
}
