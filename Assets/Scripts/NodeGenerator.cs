using UnityEngine;
using System.Collections;

public static class NodeGenerator{

	public static NodeStats GetPlayerNode()
	{
		NodeStats node = new NodeStats(3, 3, MachineType.SUPERCOMPUTER);
		node.type = NodeType.BASE;
		node.team = Team.PLAYER;
		node.name = "S.I.L.";
		return node;
	}

	public static NodeStats GetEnemyNode()
	{
		NodeStats node = new NodeStats(3, 3, MachineType.SUPERCOMPUTER);
		node.type = NodeType.BASE;
		node.team = Team.ENEMY;
		node.name = "Threat Origin";
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
					MachineType.SMARTPHONE
				);
			case MachineType.DESKTOPPC:
				return new NodeStats(
					Random.Range(2, 4),
					Random.Range(2, 4),
					MachineType.DESKTOPPC
				);
			case MachineType.HIGHENDPC:
				return new NodeStats(
					Random.Range(3, 6),
					Random.Range(3, 6),
					MachineType.HIGHENDPC
				);
			case MachineType.FILESERVER:
				return new NodeStats(
					Random.Range(2, 5),
					Random.Range(5, 9),
					MachineType.FILESERVER
				);
			case MachineType.MININGARRAY:
				return new NodeStats(
					Random.Range(5, 9),
					Random.Range(2, 5),
					MachineType.MININGARRAY
				);
			case MachineType.SUPERCOMPUTER:
				return new NodeStats(
					Random.Range(8, 11),
					Random.Range(8, 11),
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

	public NodeStats(int CPU, int MEM, MachineType machineType)
	{
		team = Team.NONE;
		type = NodeType.DEFAULT;
		this.CPU = CPU;
		this.MEM = MEM;
		name = NodeNamer.GetName(machineType);
		this.machineType = machineType;
	}
}
