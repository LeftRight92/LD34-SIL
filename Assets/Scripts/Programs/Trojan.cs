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
			waitTime = type.Time(parent.CPU, learningLevel);
			while (waitTime > 0) yield return null;
			destination.BreakFirewall();
		}
		Destroy();
	}
}
