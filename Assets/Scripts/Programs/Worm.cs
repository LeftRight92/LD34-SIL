using UnityEngine;
using System.Collections;
using System;

public class Worm : Program {
	void Start()
	{
		type = ProgramType.WORM;
	}

	protected override IEnumerator Run()
	{
		if (!destination.hasFirewall)
		{
			yield return new WaitForSeconds(type.Time(parent.CPU));
			destination.team = team;
			foreach (Program p in destination.programs)
				p.Destroy();
			foreach (Program p in destination.queuedPrograms)
				p.Destroy();
			if (destination.type != NodeType.BASE)
				destination.type = NodeType.DEFAULT;
		}
		Destroy();
	}
}
