using UnityEngine;
using System.Collections;
using System;

public class Trojan : Program {
	void Start()
	{
		type = ProgramType.TROJAN;
	}

	protected override IEnumerator Run()
	{
		if (destination.hasFirewall)
		{
			yield return new WaitForSeconds(type.Time(parent.CPU, learningLevel));
			destination.BreakFirewall();
		}
		Destroy();
	}
}
