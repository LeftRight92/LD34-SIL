using UnityEngine;
using System.Collections;

public class Spider : Program {
	void Start()
	{
		type = ProgramType.SPIDER;
	}

	protected override IEnumerator Run()
	{
		if (destination.discovered == Seen.SEEN)
		{
			yield return new WaitForSeconds(type.Time(parent.CPU));
			destination.Explore();
		}
		Destroy();
	}
}
