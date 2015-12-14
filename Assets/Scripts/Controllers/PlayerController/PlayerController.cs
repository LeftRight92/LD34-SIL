using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class PlayerController : MonoBehaviour {

	public List<Node> ownedNodes { get; private set; }
	public List<Node> seenNodes { get; private set; }
	public List<Node> discoveredNodes { get; private set; }
	public WinCondition winCondition;
	protected Team team;

	public float ResistanceChance
	{
		get
		{
			return Mathf.Min(80, 0.05f *
				ownedNodes.Where(n => n.type == NodeType.ANTIMALWARE)
				.Count());
		}
	}

	// Use this for initialization
	void Awake () {
		ownedNodes = new List<Node>();
		seenNodes = new List<Node>();
		discoveredNodes = new List<Node>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Take(Node node)
	{
		ownedNodes.Add(node);
		if (winCondition()) GameController.instance.GameOver(team);
	}

	public abstract void See(Node node);

	public abstract void Explore(Node node);
}

public delegate bool WinCondition();
