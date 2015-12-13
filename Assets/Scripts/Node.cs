using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Node : MonoBehaviour
{
	public string nodeName { get; private set; }
	public Team team;// { get; private set; }
	public Seen discovered = Seen.UNDISCOVERED;
	public NodeType type = NodeType.DEFAULT;

	public int CPU { get; private set; }
	public int MEM { get; private set; }
	public int currentMEM { get; private set; }
	public bool canBuild = true;
	public float buildCooldown = 0;
	public bool hasFirewall = false;
	
	public List<Node> neighbours;
	public List<Edge> edges;
	public List<Program> programs;
	public List<Program> queuedPrograms;



	// Use this for initialization
	void Start()
	{
		GenerateNode();
		GetComponentInChildren<TextMesh>().text = nodeName;
		currentMEM = MEM;
	}

	void GenerateNode()
	{
		NodeStats node;
		if (NetworkController.instance.playerStart == this)
			node = NodeGenerator.GetPlayerNode();
		else if (NetworkController.instance.enemyStart == this)
			node = NodeGenerator.GetEnemyNode();
		else
			node = NodeGenerator.GenerateNodeStats();
		team = node.team;
		type = node.type;
		CPU = node.CPU;
		MEM = node.MEM;
		name = node.name;
    }

	// Update is called once per frame
	void Update()
	{
		foreach (Node n in neighbours) Debug.DrawLine(transform.position, n.transform.position);
		if ((type == NodeType.DEFAULT || type == NodeType.BASE) 
			&& !canBuild)
		{
			buildCooldown -= Time.deltaTime;
			if (buildCooldown < 0) canBuild = true;
		}
	}

	public void OnProgramEnter(Program prg)
	{
		if (type == NodeType.DEFAULT || type == NodeType.BASE)
			prg.Release();
		else
			queuedPrograms.Add(prg);
	}

	public void OnProgramEnterDestination(Program prg)
	{
		//SOME STUFF
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

	public void CreateFirewall()
	{
		if ((type == NodeType.DEFAULT || type == NodeType.BASE) && canBuild&& currentMEM > 3)
		{
			currentMEM -= 3;
			hasFirewall = true;
		}
	}

	public void BreakFirewall()
	{
		hasFirewall = false;
		currentMEM += 3;
		buildCooldown = Mathf.Max(
			buildCooldown,
			15 - (1.5f * (CPU - 1))
			);
		canBuild = false;
	}

	public void RunAlgorithm(NodeType type)
	{
		if (this.type == NodeType.BASE || type == NodeType.BASE) return;
		//Consider expression...may not be ideal.
		if (type == NodeType.DEFAULT && this.type != NodeType.DEFAULT)
		{
			currentMEM = MEM;
			canBuild = true;
			this.type = type;
		}
		else
		{
			foreach (Program p in programs)
				p.Destroy();
			currentMEM = 0;
			canBuild = false;
			this.type = type;
		}
	}

	public void Create(ProgramType type, Node[] path)
	{
		if (type == ProgramType.ANTIMALWARE)
			CreateAntiMalware();
		else if ((this.type == NodeType.DEFAULT || this.type == NodeType.BASE)
				&& canBuild&& (MEM - type.MemoryUsage()) > 0)
		{
			GameObject progObj = Instantiate(type.GetPrefab(), transform.position, Quaternion.identity) as GameObject;
			Program prg = progObj.GetComponent<Program>();
			prg.parent = this;
			prg.destination = path[path.Length - 1];
			prg.path = path.ToList();
			prg.team = team;
			prg.Release();
			currentMEM -= type.MemoryUsage();
			canBuild = false;
			buildCooldown = Mathf.Max(buildCooldown, type.BuildCooldown(CPU));
		}
	}

	public void CreateAntiMalware()
	{
		if ((type == NodeType.DEFAULT || this.type == NodeType.BASE)
				   && canBuild && (MEM - ProgramType.ANTIMALWARE.MemoryUsage()) > 0)
		{
			GameObject progObj = Instantiate(ProgramType.ANTIMALWARE.GetPrefab(), transform.position, Quaternion.identity) as GameObject;
			Program prg = progObj.GetComponent<Program>();
			prg.parent = this;
			prg.team = team;
			prg.Release();
			currentMEM -= ProgramType.ANTIMALWARE.MemoryUsage(); 
		}
	}

	public void Release(Program prg)
	{
		programs.Remove(prg);
		currentMEM += prg.type.MemoryUsage();
		if(prg.type == ProgramType.ANTIMALWARE)
		{
			canBuild = false;
			buildCooldown = Mathf.Max(buildCooldown, prg.type.BuildCooldown(CPU));
		}
	}

	IEnumerator ProcessQueueOther(NodeType asType)
	{
		int time = asType.Time();
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

	IEnumerator ProcessQueue(NodeType asType)
	{
		int time = asType.Time();
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

	IEnumerator BeginCooldown(float time)
	{
		canBuild = false;
		yield return new WaitForSeconds(time);
		canBuild = true;
	}
}