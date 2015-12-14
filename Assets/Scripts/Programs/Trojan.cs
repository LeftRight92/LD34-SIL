using UnityEngine;
using System.Collections;
using System;

public class Trojan : Program {
	void Start()
	{
		type = ProgramType.TROJAN;
	}

	protected override IEnumerator RunProgram()
	{
		if (destination.hasFirewall)
		{
			yield return new WaitForSeconds(type.Time(parent.CPU, learningLevel));
			destination.BreakFirewall();
		}
		Destroy();
	}
}
