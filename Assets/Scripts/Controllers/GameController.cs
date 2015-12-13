using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NetworkController.instance.Create();
		Camera.main.transform.position = new Vector3
		(
			NetworkController.instance.playerStart.transform.position.x,
			NetworkController.instance.playerStart.transform.position.y,
			-10
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
