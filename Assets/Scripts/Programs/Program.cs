using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Program : MonoBehaviour {

	public Node parent;
	public Node destination;
	public List<Node> path;

	public Team team;// { get; private set; }
	public ProgramType type { get; protected set; }

	public int compressionLevel = 0;
	public int learningLevel = 0;
	public int encryptionLevel = 0;

	public int speed;

	// Use this for initialization
	void Start () {
		//TEMPORARY
		Release();
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
		parent.Release(this);
		Destroy(gameObject);
	}

	public void IncreaseCompression()
	{

	}

	public void IncreaseLearning()
	{

	}

	public void IncreaseEncryption()
	{

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
			Run();
		else
			currentDestination.OnProgramEnter(this);
	}

	protected abstract IEnumerator Run();
}
