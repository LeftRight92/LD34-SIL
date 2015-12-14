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
				return 2;
            case NodeType.ENCRYPTION:
				return 5;
			case NodeType.LEARNING_ALGORITHM:
				return 7;
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

	public static RuntimeAnimatorController GetNodeAnimation(this NodeType type)
	{
		switch (type)
		{
			case NodeType.DEFAULT:
			case NodeType.BASE:
				return null;
			case NodeType.COMPRESSION:
				return Resources.Load<RuntimeAnimatorController>("Compression");
			case NodeType.LEARNING_ALGORITHM:
				return Resources.Load<RuntimeAnimatorController>("Learning");
			case NodeType.ENCRYPTION:
				return Resources.Load<RuntimeAnimatorController>("Encryption");
			case NodeType.ANTIMALWARE:
				return Resources.Load<RuntimeAnimatorController>("AntiMalware");
			default:
				Debug.LogError("Invalid enum value");
				return null;
		}
	}

	public static NodeType FromString(string type)
	{
		type = type.ToUpper();
		if (type == "DEFAULT") return NodeType.DEFAULT;
		if (type == "COMPRESSION") return NodeType.COMPRESSION;
		if (type == "LEARNINGALGORITHM"|| type == "LEARNING ALGORITHM" || type == "LEARNING_ALGORITHM") return NodeType.LEARNING_ALGORITHM;
		if (type == "ENCRYPTION") return NodeType.ENCRYPTION;
		if (type == "BASE") return NodeType.BASE;
		if (type == "ANTIMALWARE") return NodeType.ANTIMALWARE;
		throw new System.Exception("NodeType.FromString " + type);

	}
}
