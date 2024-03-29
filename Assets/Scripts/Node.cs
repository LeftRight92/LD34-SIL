﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Node : MonoBehaviour
{
	public string nodeName { get; set; }
	public Team team;// { get; private set; }
	public NodeType type = NodeType.DEFAULT;
	public MachineType machineType;

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

	float processQueueWaitTime = 0;
	
	// Use this for initialization
	void Start()
	{
		if (NetworkController.instance.AutoMode)
			GenerateNode();
		else
			ManualNode();
		GetComponentInChildren<TextMesh>().text = nodeName;
		currentMEM = MEM;
	}

	void ManualNode()
	{
		NodeStats node;
		if (NetworkController.instance.playerStart == this)
			node = NodeGenerator.GetPlayerNode();
		else if (NetworkController.instance.enemyStart == this)
			node = NodeGenerator.GetEnemyNode();
		else
			node = NodeGenerator.GenerateNodeStats(machineType);
		type = node.type;
		CPU = node.CPU;
		MEM = node.MEM;
		nodeName = node.name;
		if (hasFirewall)
		{
			currentMEM = Mathf.Max(0, currentMEM - 3);
			transform.FindChild("Firewall").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Firewall");
		}
		GetComponent<SpriteRenderer>().sprite = machineType.GetSprite();
	}

	void GenerateNode()
	{
		NodeStats node;
		if (NetworkController.instance.playerStart == this)
			node = NodeGenerator.GetPlayerNode();
		else if (NetworkController.instance.enemyStart == this)
			node = NodeGenerator.GetEnemyNode();
		else 
			node = NodeGenerator.GenerateNodeStats(MachineTypeExtensions.GetRandom());
		team = node.team;
		type = node.type;
		CPU = node.CPU;
		MEM = node.MEM;
		nodeName = name = node.name;
		machineType = node.machineType;
		GetComponent<SpriteRenderer>().sprite = machineType.GetSprite();
    }

	// Update is called once per frame
	void Update()
	{
		if (processQueueWaitTime > 0) processQueueWaitTime -= GameTime.deltaTime;
		foreach (Node n in neighbours) Debug.DrawLine(transform.position, n.transform.position);
		if ((type == NodeType.DEFAULT || type == NodeType.BASE) 
			&& !canBuild)
		{
			buildCooldown -= GameTime.deltaTime;
			if (buildCooldown < 0)
			{
				canBuild = true;
				buildCooldown = 0;
			}
		}
	}

	public void OnProgramEnter(Program prg)
	{
		if (type == NodeType.DEFAULT ||
			type == NodeType.ANTIMALWARE || 
			type == NodeType.BASE)
			prg.Release();
		else
		{
			if (type == NodeType.ENCRYPTION && (prg.type == ProgramType.TROJAN || prg.type == ProgramType.SPIDER))
				prg.Release();
			else
				queuedPrograms.Add(prg);
		}
	}

	public void OnProgramEnterDestination(Program prg)
	{
		//SOME STUFF
	}

	public void HideAtStart()
	{
		//PLAYER SPECIFIC (BUT NO MATTER, BECAUSE ONLY CALLED AT START)
		GetComponent<Collider2D>().enabled = false;
		GetComponent<SpriteRenderer>().enabled = false;
		transform.FindChild("Firewall").GetComponent<SpriteRenderer>().enabled = false;
		transform.FindChild("Algorithm").GetComponent<SpriteRenderer>().enabled = false;
		transform.FindChild("Text").GetComponent<MeshRenderer>().enabled = false;
		//END PLAYER SPECIFIC
	}

	public void CreateFirewall()
	{
		if ((type == NodeType.DEFAULT || type == NodeType.BASE) && canBuild&& currentMEM >= 3)
		{
			currentMEM -= 3;
			hasFirewall = true;
			transform.FindChild("Firewall").GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Firewall");
		}
	}

	public void BreakFirewall()
	{
		hasFirewall = false;
		currentMEM = Mathf.Min(currentMEM + 3, MEM);
		buildCooldown = Mathf.Max(
			buildCooldown,
			15 - (1.5f * (CPU - 1))
			);
		canBuild = false;
		transform.FindChild("Firewall").GetComponent<Animator>().runtimeAnimatorController = null;
		transform.FindChild("Firewall").GetComponent<SpriteRenderer>().sprite = null;
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
			programs = new List<Program>();
            hasFirewall = false;
			currentMEM = 0;
			canBuild = false;
			this.type = type;
			if(type != NodeType.ANTIMALWARE) StartCoroutine(ProcessQueue(type));
		}
		transform.FindChild("Algorithm").GetComponent<Animator>().runtimeAnimatorController = type.GetNodeAnimation();
	}

	public void Create(ProgramType type, Node[] path)
	{
		if ((this.type == NodeType.DEFAULT || this.type == NodeType.BASE)
				&& canBuild&& (MEM - type.MemoryUsage()) >= 0)
		{
			GameObject progObj = Instantiate(type.GetPrefab(), transform.position, Quaternion.identity) as GameObject;
			Program prg = progObj.GetComponent<Program>();
			prg.parent = this;
			prg.destination = path[path.Length - 1];
			prg.path = path.ToList();
			prg.team = team;
			prg.GetComponent<SpriteRenderer>().color = team == Team.PLAYER ? new Color(0.6f, 0.6f, 1) : new Color(1, 0.3f, 0.3f);
			prg.Release();
			programs.Add(prg);
			currentMEM -= type.MemoryUsage();
			canBuild = false;
			buildCooldown = Mathf.Max(buildCooldown, type.BuildCooldown(CPU));
		}
	}

	public void Release(Program prg)
	{
		programs.Remove(prg);
		currentMEM += prg.type.MemoryUsage();
	}

	IEnumerator ProcessQueue(NodeType asType)
	{
		int time = asType.Time();
		while (type == asType)
		{
			if (queuedPrograms.Count != 0)
			{
				processQueueWaitTime = time / (CPU + MEM);
				while (processQueueWaitTime > 0) yield return null;
				if (asType == NodeType.COMPRESSION)
					queuedPrograms[0].IncreaseCompression();
				else if (asType == NodeType.ENCRYPTION)
					queuedPrograms[0].IncreaseEncryption();
				else
					queuedPrograms[0].IncreaseLearning();
				queuedPrograms[0].Release();
				queuedPrograms.Remove(queuedPrograms[0]);
			}
			yield return null;
		}
	}
}