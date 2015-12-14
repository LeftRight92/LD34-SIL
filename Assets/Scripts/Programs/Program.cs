using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Program : MonoBehaviour {

	public Node parent;
	public Node destination;
	public List<Node> path;

	public Team team;// { get; private set; }
	public ProgramType type { get; protected set; }

	public int compressionLevel { get; private set; }
	public int learningLevel { get; private set; }
	public int encryptionLevel { get; private set; }

	public int speed;

	// Use this for initialization
	void Start () {
		compressionLevel = 0;
		learningLevel = 0;
		encryptionLevel = 0;
		//TEMPORARY
		Release();
	}

	void Appear()
	{
		GetComponent<SpriteRenderer>().enabled = true;
	}

	void Disappear()
	{
		GetComponent<SpriteRenderer>().enabled = false;
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
		compressionLevel++;
	}

	public void IncreaseLearning()
	{
		learningLevel++;
	}

	public void IncreaseEncryption()
	{
		encryptionLevel++;
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
		Debug.Log("Arrived");
		path.Remove(currentDestination);
		Disappear();
		Debug.Log(path.Count);
		//if (currentDestination == destination)
		if (path.Count == 0)
		{
			StartCoroutine(RunProgram());
		}
		else
			currentDestination.OnProgramEnter(this);
	}

	protected abstract IEnumerator RunProgram();

}
