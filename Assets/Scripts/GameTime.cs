using UnityEngine;
using System.Collections;

public static class GameTime{

	public static bool pause = false;
	public static float deltaTime
	{
		get
		{
			if (pause)
				return 0;
			else
				return Time.deltaTime;
		}
	}
}
