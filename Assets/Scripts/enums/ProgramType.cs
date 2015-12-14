using UnityEngine;
using System.Collections;

public enum ProgramType {
	SPIDER,
	TROJAN,
	WORM,
	FORKBOMB
}

public static class ProgramTypeExtension
{
	public static GameObject forkBombPrefab = Resources.Load("ForkBomb") as GameObject;
	public static GameObject spiderPrefab = Resources.Load("Spider") as GameObject;
	public static GameObject trojanPrefab = Resources.Load("Trojan") as GameObject;
	public static GameObject wormPrefab = Resources.Load("Worm") as GameObject;

	public static GameObject GetPrefab(this ProgramType type)
	{
		switch (type)
		{
			case ProgramType.FORKBOMB:
				return forkBombPrefab;
			case ProgramType.SPIDER:
				return spiderPrefab;
			case ProgramType.TROJAN:
				return trojanPrefab;
			case ProgramType.WORM:
				return wormPrefab;
			default:
				Debug.LogError("Invalid enum type");
				return null;
		}
	}

	public static int MemoryUsage(this ProgramType type)
	{
		switch (type)
		{
			case ProgramType.SPIDER:
				return 1;
			case ProgramType.WORM:
				return 2;
			case ProgramType.TROJAN:
				return 3;
			case ProgramType.FORKBOMB:
				return 4;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}
	

	public static float Time(this ProgramType type)
	{
		switch (type)
		{
			case ProgramType.SPIDER:
			case ProgramType.FORKBOMB:
				return 5;
			case ProgramType.WORM:
				return 10;
			case ProgramType.TROJAN:
				return 15;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}

	public static float Time(this ProgramType type, int parentCPU)
	{
		float t = Time(type);
		return t - ((t / 10) * (parentCPU - 1));
    }

	public static float Time(this ProgramType type, int parentCPU, int learningLevel)
	{
		float t = Time(type, parentCPU);
		return (1 - (learningLevel * 0.1f)) * t;
	}

	public static int BuildCooldown(this ProgramType type)
	{
		switch (type)
		{
			case ProgramType.SPIDER:
				return 10;
			case ProgramType.WORM:
				return 15;
			case ProgramType.TROJAN:
				return 20;
			case ProgramType.FORKBOMB:
				return 30;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}

	public static int BuildCooldown(this ProgramType type, int CPU)
	{
		int t = BuildCooldown(type);
		return t - ((t / 10) * (CPU - 1));
	}

}
