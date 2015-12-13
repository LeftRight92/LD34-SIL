using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public Node parent;
	public Node destination;
	public List<Node> path;

	public Team team;// { get; private set; }
	public ProgramType type;// { get; private set; }

	public int compressionLevel;
	public int learningLevel;
	public int encryptionLevel;

	public int speed;

	// Use this for initialization
	void Start () {
		//TEMPORARY
		Release();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Appear()
	{
		GetComponentInChildren<MeshRenderer>().enabled = true;
	}

	void Disappear()
	{
		GetComponentInChildren<MeshRenderer>().enabled = false;
	}

	public void Release()
	{
		Appear();
		StartCoroutine(Move());
	}

	public void Destroy()
	{
		Debug.Log("Pop");
		parent.Release(this);
		Destroy(gameObject);
	}

	IEnumerator Move()
	{
		Node currentDestination = path[0];
		while(transform.position != currentDestination.transform.position)
		{
			Vector3 step = (currentDestination.transform.position - transform.position).normalized *
				Time.deltaTime *
				speed *
				(1 + (0.25f * compressionLevel));
			if (step.magnitude > (currentDestination.transform.position - transform.position).magnitude)
				transform.position = currentDestination.transform.position;
			else
				transform.Translate(step);
			yield return null;
		}
		path.Remove(currentDestination);
		Disappear();
		if (currentDestination == destination)
			currentDestination.OnProgramEnterDestination(this);
		else
			currentDestination.OnProgramEnter(this);
	}
}
