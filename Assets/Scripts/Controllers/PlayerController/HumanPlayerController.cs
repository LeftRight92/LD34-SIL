using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class HumanPlayerController : PlayerController
{
	void Awake()
	{
		team = Team.PLAYER;
	}

	public override void Explore(Node node)
	{
		discoveredNodes.Add(node);
		foreach (Node n in node.neighbours.Where(n => !discoveredNodes.Contains(n)))
			See(n);
		//PLAYER SPECIFIC
		foreach (Edge e in node.edges) e.GetComponent<LineRenderer>().enabled = true;
		node.transform.FindChild("Text").GetComponent<MeshRenderer>().enabled = true;
		//END PLAYER SPECIFIC
	}

	public override void See(Node node)
	{
		node.GetComponent<SpriteRenderer>().enabled = true;
		node.GetComponent<Collider2D>().enabled = true;
		seenNodes.Add(node);
	}
}
