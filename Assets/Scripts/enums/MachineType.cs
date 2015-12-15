	using UnityEngine;
using System.Collections;

public enum MachineType {
	SMARTPHONE,
	DESKTOPPC,
	HIGHENDPC,
	FILESERVER,
	MININGARRAY,
	SUPERCOMPUTER
}

public static class MachineTypeExtensions
{
	public static MachineType GetRandom()
	{
		float r = Random.value;
		if (r < 0.10f)
			return MachineType.SMARTPHONE;
		else if(r < 0.45f)
            return MachineType.DESKTOPPC;
		else if (r < 0.65f)
			return MachineType.HIGHENDPC;
		else if(r < 0.8f)
            return MachineType.FILESERVER;
		else if (r < 0.95f)
			return MachineType.MININGARRAY;
		else
            return MachineType.SUPERCOMPUTER;
    }

	public static Sprite GetSprite(this MachineType type)
	{
		switch (type)
		{
			case MachineType.SMARTPHONE:
				return Resources.Load<Sprite>("Smartphone");
			case MachineType.DESKTOPPC:
				return Resources.Load<Sprite>("Desktop PC");
			case MachineType.HIGHENDPC:
				return Resources.Load<Sprite>("High-end PC");
			case MachineType.FILESERVER:
				return Resources.Load<Sprite>("File Server");
			case MachineType.MININGARRAY:
				return Resources.Load<Sprite>("Mining Array");
			case MachineType.SUPERCOMPUTER:
				return Resources.Load<Sprite>("Research Supercomputer");
			default:
				Debug.LogError("Invalid enum type");
				return null;
		}
	}
}


