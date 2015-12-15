using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Worm : Program {
	void Start()
	{
		type = ProgramType.WORM;
	}

	protected override IEnumerator RunProgram()
	{
		if (!destination.hasFirewall)
		{
			Debug.Log("Worm Attacking...");
			waitTime = type.Time(parent.CPU, learningLevel);
			while (waitTime > 0) yield return null;
			Debug.Log("Caught by stuff");
			if (destination.team == Team.NONE ||
				Random.value > (
				GameController.instance.playerScript[destination.team].ResistanceChance -
				(encryptionLevel * 0.1f)
				))
			{
				Debug.Log("Not caught by stuff");
				if (team == Team.PLAYER)
				{
					if(destination.team == Team.ENEMY) GameController.instance.enemy.ownedNodes.Remove(destination);
					GameController.instance.player.Take(destination);
				}
				else if (team == Team.ENEMY)
				{
					if(destination.team == Team.PLAYER) GameController.instance.player.ownedNodes.Remove(destination);
					GameController.instance.enemy.Take(destination);
				}
					
				destination.team = team;
				destination.GetComponent<SpriteRenderer>().color = team.TeamColour();
				destination.programs = new List<Program>();
				destination.queuedPrograms = new List<Program>();
				//foreach (Program p in destination.programs)
				//	p.Destroy();
				//foreach (Program p in destination.queuedPrograms)
				//	p.Destroy();
				if (destination.type != NodeType.BASE)
					destination.type = NodeType.DEFAULT;
			}
		}
		Destroy();
	}
}
