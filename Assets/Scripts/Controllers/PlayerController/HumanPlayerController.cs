﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class HumanPlayerController : PlayerController
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
		foreach (Edge e in node.edges) e.GetComponent<LineRenderer>().enabled = true;
		node.transform.FindChild("Text").GetComponent<MeshRenderer>().enabled = true;
		node.transform.FindChild("Firewall").GetComponent<SpriteRenderer>().enabled = true;
		node.transform.FindChild("Algorithm").GetComponent<SpriteRenderer>().enabled = true;
		
	}

	public override void See(Node node)
	{
		node.GetComponent<SpriteRenderer>().enabled = true;
		node.GetComponent<Collider2D>().enabled = true;
		seenNodes.Add(node);
	}
}
