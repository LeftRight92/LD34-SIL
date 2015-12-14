﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	public static UIController instance;

	private MouseController mouseController;

	public int ProcessesLines = 3;

	private GameObject[] infoPanelElements;
	private GameObject[] programTextElements;
	private GameObject[] scriptTextElements;
	private GameObject[] programListElements;

	void Awake()
	{
		instance = this;
		infoPanelElements = new GameObject[]
		{
			GetElement("Text Canvas", "CPU Amount").gameObject,
			GetElement("Text Canvas", "MEM Amount").gameObject,
			GetElement("Text Canvas", "Firewall Status").gameObject,
			GetElement("Text Canvas", "Cooldown Time").gameObject,
			GetElement("Text Canvas", "Processes").gameObject,
			GetElement("Window Canvas", "SystemInformation").gameObject
		};
		programTextElements = new GameObject[]
		{
			GetElement("Text Canvas", "Spider Text").gameObject,
			GetElement("Text Canvas", "Worm Text").gameObject,
			GetElement("Text Canvas", "Trojan Text").gameObject,
			GetElement("Text Canvas", "Firewall Text").gameObject,
			GetElement("Text Canvas", "ForkBomb Text").gameObject,
		};
		scriptTextElements = new GameObject[]
		{
			GetElement("Text Canvas", "Compression Text").gameObject,
			GetElement("Text Canvas", "Learning Text").gameObject,
			GetElement("Text Canvas", "AntiMalware Text").gameObject,
			GetElement("Text Canvas", "Encryption Text").gameObject,
		};
		programListElements = new GameObject[]
		{
			GetElement("Text Canvas", "Spider Text").gameObject,
			GetElement("Text Canvas", "Worm Text").gameObject,
			GetElement("Text Canvas", "Trojan Text").gameObject,
			GetElement("Text Canvas", "Firewall Text").gameObject,
			GetElement("Text Canvas", "ForkBomb Text").gameObject,
			GetElement("Text Canvas", "Compression Text").gameObject,
			GetElement("Text Canvas", "Learning Text").gameObject,
			GetElement("Text Canvas", "AntiMalware Text").gameObject,
			GetElement("Text Canvas", "Encryption Text").gameObject,
			GetElement("Window Canvas", "ProgramList").gameObject,
		};
	}

	void Start () {
		mouseController = MouseController.instance;
	}
	
	Transform GetElement(string canvas, string element)
	{
		return transform.FindChild(canvas).FindChild(element);
	}

	void Update()
	{
		UpdateDisplay(mouseController.selectedNode);
	}

	// Update is called once per frame
	void UpdateDisplay (Node node) {
		if (node != null)
		{
			foreach (GameObject g in infoPanelElements) g.SetActive(true);
			if(GameController.instance.player.ownedNodes.Contains(node))
			{
				if(NetworkController.instance.playerStart != node)
					foreach (GameObject g in scriptTextElements) g.SetActive(true);
				else
					foreach (GameObject g in scriptTextElements) g.SetActive(false);
				if (node.buildCooldown <= 0)
					DisplayPrograms(node);
				else
					foreach (GameObject g in programTextElements) g.SetActive(false);
				GetElement("Window Canvas", "ProgramList").gameObject.SetActive(true);
			}
			else
				foreach (GameObject g in programListElements) g.SetActive(false);
			GetElement("Text Canvas", "Cooldown Time").GetComponent<Text>().text =
				mouseController.selectedNode.buildCooldown.ToString();
			GetElement("Text Canvas", "Processes").GetComponent<Text>().text =
				CreateProcessesList(mouseController.selectedNode);
			GetElement("Text Canvas", "MEM Amount").GetComponent<Text>().text =
				mouseController.selectedNode.currentMEM.ToString() + "/" + mouseController.selectedNode.MEM.ToString();
			GetElement("Text Canvas", "Firewall Status").GetComponent<Text>().text =
				mouseController.selectedNode.hasFirewall ? "ENABLED" : "DISABLED";
			GetElement("Text Canvas", "CPU Amount").GetComponent<Text>().text =
				node.CPU.ToString();
		}
		else
		{
			foreach (GameObject g in infoPanelElements) g.SetActive(false);
			foreach (GameObject g in programListElements) g.SetActive(false);
		}
	}

	void DisplayPrograms(Node node)
	{
		if (node.MEM >= ProgramType.SPIDER.MemoryUsage()) GetElement("Text Canvas", "Spider Text").gameObject.SetActive(true);
		if (node.MEM >= ProgramType.WORM.MemoryUsage()) GetElement("Text Canvas", "Worm Text").gameObject.SetActive(true);
		if (node.MEM >= ProgramType.TROJAN.MemoryUsage()) GetElement("Text Canvas", "Trojan Text").gameObject.SetActive(true);
		if (node.MEM >= ProgramType.FORKBOMB.MemoryUsage()) GetElement("Text Canvas", "ForkBomb Text").gameObject.SetActive(true);
		if (node.MEM >= 3) GetElement("Text Canvas", "Firewall Text").gameObject.SetActive(true);
	}
	
	string CreateProcessesList(Node node)
	{
		string text = "";
		if(node.type == NodeType.BASE || node.type == NodeType.DEFAULT)
		{
			if (node.programs.Count == 0)
				text = "-NONE-";
			else if (node.programs.Count > 3)
			{
				text = node.programs[0].type.ToString() + "\n" +
					node.programs[1].type.ToString() + "\n" +
					"...and " + node.programs.Count + " more.";
            }
			else
				foreach (Program p in node.programs)
					text += p.type.ToString() + "\n";
		}else if(node.type == NodeType.ANTIMALWARE)
		{

		}
		else
		{

		}
		return text;
	}
}
