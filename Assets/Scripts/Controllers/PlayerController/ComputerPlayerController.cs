using UnityEngine;
using System.Collections;
using System;

public class ComputerPlayerController : PlayerController
{
	void Awake()
	{
		team = Team.PLAYER;
	}

	public override void Explore(Node node)
	{
		//throw new NotImplementedException();
	}

	public override void See(Node node)
	{
		//throw new NotImplementedException();
	}
}
