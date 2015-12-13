using UnityEngine;
using System.Collections;

public enum ProgramType {
	SPIDER,
	TROJAN,
	WORM,
	FORKBOMB,
	ANTIMALWARE
}

public static class ProgramTypeExtension
{
	public static int MemoryUsage(this ProgramType type)
	{
		switch (type)
		{
			case ProgramType.SPIDER:
				return 1;
			case ProgramType.WORM:
				return 2;
			case ProgramType.TROJAN:
			case ProgramType.ANTIMALWARE:
				return 3;
			case ProgramType.FORKBOMB:
				return 4;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}

	public static int Time(this ProgramType type)
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
			case ProgramType.ANTIMALWARE:
				Debug.LogError("Anti-Malware has no time value");
				return 0;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
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
			case ProgramType.ANTIMALWARE:
				return 25;
			case ProgramType.FORKBOMB:
				return 30;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}

}
