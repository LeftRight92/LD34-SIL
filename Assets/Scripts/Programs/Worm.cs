using UnityEngine;
using System.Collections;

public class Worm : Program {
	void Start()
	{
		type = ProgramType.WORM;
	}

	protected override IEnumerator Run()
	{
		if (!destination.hasFirewall)
		{
			yield return new WaitForSeconds(type.Time(parent.CPU, learningLevel));
			if (destination.team != Team.NONE &&
				Random.value > (
				GameController.instance.playerScript[destination.team].ResistanceChance -
				(encryptionLevel * 0.1f)
				))
			{ 
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
				destination.GetComponent<SpriteRenderer>().color = team == Team.PLAYER ? new Color(0.6f, 0.6f, 1) : new Color(1, 0.3f, 0.3f);
				foreach (Program p in destination.programs)
					p.Destroy();
				foreach (Program p in destination.queuedPrograms)
					p.Destroy();
				if (destination.type != NodeType.BASE)
					destination.type = NodeType.DEFAULT;
			}
		}
		Destroy();
	}
}
