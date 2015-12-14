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
			yield return new WaitForSeconds(type.Time(parent.CPU, learningLevel));
			GameController.instance.playerScript[team].Explore(destination);
		}
		Destroy();
	}
}
