using UnityEngine;
using System.Collections;

public static class NodeGenerator{

	public static NodeStats GetPlayerNode()
	{
		NodeStats node = new NodeStats(3, 3, "S.I.L.", MachineType.SUPERCOMPUTER);
		node.type = NodeType.BASE;
		node.team = Team.PLAYER;
		return node;
	}

	public static NodeStats GetEnemyNode()
	{
		NodeStats node = new NodeStats(3, 3, "Threat Origin", MachineType.SUPERCOMPUTER);
		node.type = NodeType.BASE;
		node.team = Team.ENEMY;
		return node;
	}

	public static NodeStats GenerateNodeStats(MachineType type)
	{
		switch (type)
		{
			case MachineType.SMARTPHONE:
				return new NodeStats(
					Random.Range(1, 3),
					Random.Range(1, 3),
					"Smartphone",
					MachineType.SMARTPHONE
				);
			case MachineType.DESKTOPPC:
				return new NodeStats(
					Random.Range(2, 4),
					Random.Range(2, 4),
					"Desktop",
					MachineType.DESKTOPPC
				);
			case MachineType.HIGHENDPC:
				return new NodeStats(
					Random.Range(3, 6),
					Random.Range(3, 6),
					"Hi-End",
					MachineType.HIGHENDPC
				);
			case MachineType.FILESERVER:
				return new NodeStats(
					Random.Range(2, 5),
					Random.Range(5, 9),
					"File Server",
					MachineType.FILESERVER
				);
			case MachineType.MININGARRAY:
				return new NodeStats(
					Random.Range(5, 9),
					Random.Range(2, 5),
					"Mining Array",
					MachineType.MININGARRAY
				);
			case MachineType.SUPERCOMPUTER:
				return new NodeStats(
					Random.Range(8, 11),
					Random.Range(8, 11),
					"Research Supercomputer",
					MachineType.SUPERCOMPUTER
				);
			default:
				Debug.LogError("Invalid enum type");
				return new NodeStats();
		}
	}
}

public struct NodeStats
{
	public Team team;
	public NodeType type;
	public int CPU, MEM;
	public string name;
	public MachineType machineType;

	public NodeStats(int CPU, int MEM, string name, MachineType machineType)
	{
		team = Team.NONE;
		type = NodeType.DEFAULT;
		this.CPU = CPU;
		this.MEM = MEM;
		this.name = name;
		this.machineType = machineType;
	}
}
