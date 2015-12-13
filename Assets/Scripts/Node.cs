using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Node : MonoBehaviour
{

	public int CPU;// { get; private set; }
	public int MEM;// { get; private set; }
	public Team team { get; private set; }
	public string name;// { get; private set; }
	public List<Node> neighbours;
	public List<Edge> edges;
	public List<Program> programs;
	public List<Program> queuedPrograms;
	public Seen discovered = Seen.UNDISCOVERED;
	public NodeType type = NodeType.DEFAULT;

	// Use this for initialization
	void Start()
	{
		CPU = 3;
		MEM = 3;
		if (NetworkController.instance.playerStart == this)
		{
			team = Team.PLAYER;
			type = NodeType.BASE;
			name = "SIL";
		}
		else if (NetworkController.instance.enemyStart == this)
		{
			team = Team.ENEMY;
			type = NodeType.BASE;
			name = "Threat Origin";
		}
		else
			GenerateNode();
		GetComponentInChildren<TextMesh>().text = name;
	}

	void GenerateNode()
	{
		team = Team.NONE;
		float type = Random.value;
		type = Mathf.Pow(type, 2);
		if (type < 0.25f)
		{
			CPU = Random.Range(1, 3);
			MEM = Random.Range(1, 3);
			name = "Smartphone";
		}
		else if (type < 0.45f)
		{
			CPU = Random.Range(2, 4);
			MEM = Random.Range(2, 4);
			name = "Desktop";
		}
		else if (type < 0.65f)
		{
			CPU = Random.Range(3, 6);
			MEM = Random.Range(3, 6);
			name = "Hi-End";
		}
		else if (type < 0.8f)
		{
			CPU = Random.Range(2, 5);
			MEM = Random.Range(5, 9);
			name = "File Server";
		}
		else if (type < 0.95f)
		{
			CPU = Random.Range(5, 9);
			MEM = Random.Range(2, 5);
			name = "Mining Array";
		}
		else
		{
			CPU = Random.Range(8, 11);
			MEM = Random.Range(8, 11);
			name = "Research Supercomputer";
		}
	}

	// Update is called once per frame
	void Update()
	{
		foreach (Node n in neighbours) Debug.DrawLine(transform.position, n.transform.position);
	}

	void OnProgramEnter(Program prg)
	{
		if (type == NodeType.DEFAULT || type == NodeType.BASE)
			prg.Release();
		else
			queuedPrograms.Add(prg);
	}

	public void See()
	{
		transform.FindChild("Sphere").GetComponent<MeshRenderer>().enabled = true;
		GetComponent<Collider2D>().enabled = true;
		discovered = Seen.SEEN;
	}

	public void Explore()
	{
		discovered = Seen.EXPLORED;
		transform.FindChild("Text").GetComponent<MeshRenderer>().enabled = true;
		foreach (Node n in neighbours.Where(n => n.discovered != Seen.EXPLORED))
			n.See();
		foreach (Edge e in edges) e.GetComponent<LineRenderer>().enabled = true;
	}

	public void Disable()
	{
		foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
			r.enabled = false;
		GetComponent<Collider2D>().enabled = false;
	}

	public void Create(ProgramType type)
	{

	}

	IEnumerator ProcessQueue(NodeType asType)
	{
		int time = (asType == NodeType.COMPRESSION) ? 5 :
			((asType == NodeType.ENCRYPTION) ? 10 : 15);
		while (type == asType)
		{
			if (queuedPrograms.Count != 0)
			{
				yield return new WaitForSeconds(time);
				if (asType == NodeType.COMPRESSION)
					queuedPrograms[0].compressionLevel += CPU + MEM;
				else if (asType == NodeType.ENCRYPTION)
					queuedPrograms[0].encryptionLevel += CPU + MEM;
				else
					queuedPrograms[0].learningLevel += CPU + MEM;
				queuedPrograms[0].Release();
				queuedPrograms.Remove(queuedPrograms[0]);
			}
		}
	}

	IEnumerator ProcessQueueOther(NodeType asType)
	{
		int time = (asType == NodeType.COMPRESSION) ? 5 :
			((asType == NodeType.ENCRYPTION) ? 10 : 15);
		while (type == asType)
		{
			if (queuedPrograms.Count != 0)
			{
				yield return new WaitForSeconds(time / (CPU + MEM));
				if (asType == NodeType.COMPRESSION)
					queuedPrograms[0].compressionLevel++;
				else if (asType == NodeType.ENCRYPTION)
					queuedPrograms[0].encryptionLevel++;
				else
					queuedPrograms[0].learningLevel++;
				queuedPrograms[0].Release();
				queuedPrograms.Remove(queuedPrograms[0]);
			}
		}
	}
}