using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

	public static GameController instance;
	public PlayerController player;
	public PlayerController enemy;
	public Dictionary<Team, PlayerController> playerScript { get; private set; }

	void Awake()
	{
		instance = this;
		playerScript = new Dictionary<Team, PlayerController>();
		playerScript.Add(Team.PLAYER, player);
		playerScript.Add(Team.ENEMY, enemy);
	}

	// Use this for initialization
	void Start()
	{
		NetworkController.instance.Create();
		Camera.main.transform.position = new Vector3
		(
			NetworkController.instance.playerStart.transform.position.x,
			NetworkController.instance.playerStart.transform.position.y,
			-10
		);

		Node playerStart = NetworkController.instance.playerStart;
		Node enemyStart = NetworkController.instance.enemyStart;

		player.See(playerStart);
		player.Explore(playerStart);
		player.ownedNodes.Add(playerStart);
		playerStart.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 1);
		player.winCondition = PlayerTakeAll;

		enemy.See(enemyStart);
		enemy.Explore(enemyStart);
		enemy.ownedNodes.Add(enemyStart);
		enemyStart.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0.3f);
		enemy.winCondition = EnemyTakeSIL;

		UIController.instance.UpdateDisplay(null);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GameOver(Team team)
	{
		Debug.Log("GAME OVER, WINNER: " + team);
	}

	bool PlayerTakeAll()
	{
		HashSet<Node> playerNodes = new HashSet<Node>(player.ownedNodes);
		return playerNodes.SetEquals(NetworkController.instance.nodes);
	}

	bool EnemyTakeSIL()
	{
		return enemy.ownedNodes.Contains(NetworkController.instance.playerStart);
	}
}
