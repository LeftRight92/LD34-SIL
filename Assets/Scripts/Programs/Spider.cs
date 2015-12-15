using UnityEngine;
using System.Collections;

public class Spider : Program {
	void Start()
	{
		type = ProgramType.SPIDER;
	}

	protected override IEnumerator RunProgram()
	{
		Debug.Log("Spider Exploring...");
		if(GameController.instance.playerScript[team].seenNodes.Contains(destination))
		{
			waitTime = type.Time(parent.CPU, learningLevel);
			while (waitTime > 0) yield return null;
			GameController.instance.playerScript[team].Explore(destination);
		}
		Destroy();
	}
}
