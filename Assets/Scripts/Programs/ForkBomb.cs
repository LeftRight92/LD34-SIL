using UnityEngine;
using System.Collections;
using System;

public class ForkBomb : Program {
	void Start()
	{
		type = ProgramType.FORKBOMB;
	}

	protected override IEnumerator Run()
	{
		if (!destination.hasFirewall)
		{
			yield return new WaitForSeconds(type.Time(parent.CPU));
			destination.buildCooldown = (40 - (4 * (destination.CPU - 1)));
			destination.canBuild = false;
		}
		Destroy();
    }
}
