using UnityEngine;
using System.Collections;

public static class NodeGenerator{

	public static NodeStats GetPlayerNode()
	{
		NodeStats node = new NodeStats(3, 3, "SIL");
		node.type = NodeType.BASE;
		node.team = Team.PLAYER;
		return node;
	}

	public static NodeStats GetEnemyNode()
	{
		NodeStats node = new NodeStats(3, 3, "Threat Origin");
		node.type = NodeType.BASE;
		node.team = Team.ENEMY;
		return node;
	}

	public static NodeStats GenerateNodeStats()
	{
		float kind = Random.value;
		kind = Mathf.Pow(kind, 2);
		if (kind < 0.25f)
			return new NodeStats(
				Random.Range(1, 3),
				Random.Range(1, 3),
				"Smartphone"
			);
		else if (kind < 0.45f)
			return new NodeStats(
				Random.Range(2, 4),
				Random.Range(2, 4),
				"Desktop"
			);
		else if (kind < 0.65f)
			return new NodeStats(
				Random.Range(3, 6),
				Random.Range(3, 6),
				"Hi-End"
			);
		else if (kind < 0.8f)
			return new NodeStats(
				Random.Range(2, 5),
				Random.Range(5, 9),
				"File Server"
			);
		else if (kind < 0.95f)
			return new NodeStats(
				Random.Range(5, 9),
				Random.Range(2, 5),
				"Mining Array"
			);
		else
			return new NodeStats(
				Random.Range(8, 11),
				Random.Range(8, 11),
				"Research Supercomputer"
			);
	}
}

public struct NodeStats
{
	public Team team;
	public NodeType type;
	public int CPU, MEM;
	public string name;

	public NodeStats(int CPU, int MEM, string name)
	{
		team = Team.NONE;
		type = NodeType.DEFAULT;
		this.CPU = CPU;
		this.MEM = MEM;
		this.name = name;
	}
}
