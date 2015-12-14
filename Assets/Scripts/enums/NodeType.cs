using UnityEngine;
using System.Collections;

public enum NodeType {
	DEFAULT,
	COMPRESSION,
	LEARNING_ALGORITHM,
	ENCRYPTION,
	BASE,
	ANTIMALWARE
}

public static class NodeTypeExtension
{
	public static int Time(this NodeType type)
	{
		switch (type)
		{
			case NodeType.DEFAULT:
			case NodeType.BASE:
				Debug.LogWarning("Default or Base attempted to request runtime.");
				return 0;
			case NodeType.COMPRESSION:
				return 5;
            case NodeType.ENCRYPTION:
				return 10;
			case NodeType.LEARNING_ALGORITHM:
				return 15;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}

	public static float Bonus(this NodeType type)
	{
		switch (type)
		{
			case NodeType.DEFAULT:
			case NodeType.BASE:
				Debug.LogWarning("Default or Base attempted to request bonus multiplier.");
				return 0;
			case NodeType.ENCRYPTION:
				return 0.10f;
			case NodeType.LEARNING_ALGORITHM:
				return 0.15f;
			case NodeType.COMPRESSION:
				return 0.25f;
			default:
				Debug.LogError("Invalid enum type");
				return 0;
		}
	}
}
