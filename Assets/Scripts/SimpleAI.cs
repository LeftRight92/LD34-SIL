using UnityEngine;
using System.Collections;

public class SimpleAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(AIScript());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator AIScript()
	{
		yield return new WaitForSeconds(Random.Range(1, 3));
	}
}
