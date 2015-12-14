using UnityEngine;
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
		if(mouseController.selectedNode != null)
		{
			GetElement("Text Canvas", "Cooldown Time").GetComponent<Text>().text =
				mouseController.selectedNode.buildCooldown.ToString();
			GetElement("Text Canvas", "Processes").GetComponent<Text>().text =
				CreateProcessesList(mouseController.selectedNode);
			GetElement("Text Canvas", "MEM Amount").GetComponent<Text>().text =
				mouseController.selectedNode.currentMEM.ToString() + "/" + mouseController.selectedNode.MEM.ToString();
			GetElement("Text Canvas", "Firewall Status").GetComponent<Text>().text =
				mouseController.selectedNode.hasFirewall ? "ENABLED" : "DISABLED";
			if (mouseController.selectedNode.buildCooldown > 0)
				foreach (GameObject g in programTextElements) g.SetActive(false);
			else
				foreach (GameObject g in programTextElements) g.SetActive(true);
		}
	}

	// Update is called once per frame
	public void UpdateDisplay (Node node) {
		if (node != null)
		{
			foreach (GameObject g in infoPanelElements) g.SetActive(true);
			foreach (GameObject g in programListElements) g.SetActive(true);
			GetElement("Text Canvas", "CPU Amount").GetComponent<Text>().text =
				node.CPU.ToString();
		}
		else
		{
			foreach (GameObject g in infoPanelElements) g.SetActive(false);
			foreach (GameObject g in programListElements) g.SetActive(false);
		}
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
