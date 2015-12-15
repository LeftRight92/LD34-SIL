using UnityEngine;
using System.Collections;

public class ForkBomb : Program {
	void Start()
	{
		type = ProgramType.FORKBOMB;
	}

	protected override IEnumerator RunProgram()
	{
		if (!destination.hasFirewall)
		{
			waitTime = type.Time(parent.CPU, learningLevel);
			while (waitTime > 0) yield return null;
			if (destination.team != Team.NONE &&
				Random.value > (
				GameController.instance.playerScript[destination.team].ResistanceChance -
				(encryptionLevel * 0.1f )
				))
			{
				destination.buildCooldown = (40 - (4 * (destination.CPU - 1)));
				destination.canBuild = false;
			}
		}
		Destroy();
    }
}
