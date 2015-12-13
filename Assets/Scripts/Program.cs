using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public Node parent;
	public Node destination;
	public List<Node> path;
	public Team team { get; private set; }
	public ProgramType type { get; private set; }
	public int compressionLevel;
	public int learningLevel;
	public int encryptionLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Release()
	{

	}
}
