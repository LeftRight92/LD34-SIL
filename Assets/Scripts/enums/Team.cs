using UnityEngine;
using System.Collections;

public enum Team {
	PLAYER,
	ENEMY,
	NONE
}

public static class TeamExtensions
{
	public static Color TeamColour(this Team team)
	{
		switch (team)
		{
			case Team.PLAYER:
				return new Color(0.6f, 0.6f, 1);
            case Team.ENEMY:
				return new Color(1, 0.3f, 0.3f);
            case Team.NONE:
				return new Color(1, 1, 1);
			default:
				Debug.LogError("Bad Enum Value");
				return Color.white;
		}
	}

	public static string AsString(this Team team)
	{
		switch(team){
			case Team.PLAYER:
				return "Owned";
			case Team.ENEMY:
				return "Hostile";
			case Team.NONE:
				return "Neutral";
			default:
				return "";
		}
	}
}
