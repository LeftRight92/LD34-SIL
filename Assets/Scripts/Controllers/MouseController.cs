using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{

	public static MouseController instance;

	public int minZoom = 1;
	public int maxZoom = 10;
	public int zoomStep = 1;
	public int cameraStartSize = 3;

	public Node selectedNode;

	public bool pathingMode;
	public ProgramType prgType;
	public List<Node> path;

	private Vector3 lastMousePosition;

	void Awake()
	{
		Camera.main.orthographicSize = cameraStartSize;
		instance = this;
	}

	void Update()
	{
		CameraControls();

		if (Input.GetMouseButtonDown(0) && !UIHit())
			if (pathingMode)
				TryAddNode();
			else
				SelectNode();
	}

	void TryAddNode()
	{
		RaycastHit2D hit = Physics2D.Raycast(
			Camera.main.ScreenToWorldPoint(Input.mousePosition),
			Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Node"));
		if (hit)
		{
			Node hitNode = hit.transform.GetComponent<Node>();
			if (path[path.Count - 1].neighbours.Contains(hitNode))
			{
				path.Add(hitNode);
				PlayerController player = GameController.instance.player;
				if (prgType == ProgramType.SPIDER)
				{
					if (!player.discoveredNodes.Contains(hitNode) && player.seenNodes.Contains(hitNode))
					{
						pathingMode = false;
						player.RunProgram(selectedNode, prgType, path.ToArray());
					}
				}
				else
				{
					if (!player.ownedNodes.Contains(hitNode))
					{
						pathingMode = false;
						player.RunProgram(selectedNode, prgType, path.ToArray());
					}
				}
			}
		}
	}

	bool UIHit()
	{
		PointerEventData ped = new PointerEventData(null);
		ped.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();
		GraphicRaycaster windowCast = UIController.instance.transform.FindChild("Window Canvas").GetComponent<GraphicRaycaster>();
		windowCast.Raycast(ped, results);
		if (results.Count != 0) return true;
		return false;
	}

	void SelectNode()
	{
		RaycastHit2D hit = Physics2D.Raycast(
			Camera.main.ScreenToWorldPoint(Input.mousePosition),
			Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Node"));
		if (hit)
		{
			selectedNode = hit.transform.GetComponent<Node>();
			UIController.instance.UpdateDisplay(selectedNode);
			//GameController.instance.player.Explore(selectedNode);
		}
		else
		{
			selectedNode = null;
			UIController.instance.UpdateDisplay(null);
		}
	}

	public void RunProgram(ProgramType type)
	{
		pathingMode = true;
		path = new List<Node>();
		path.Add(selectedNode);
		prgType = type;
	}

	public void RunAlgorithm(NodeType type)
	{
		GameController.instance.player.RunAlgorithm(selectedNode, type);
	}

	public void RunProgram(string type)
	{
		Debug.Log("Runing Program: " + type.ToString());
		RunProgram(ProgramTypeExtension.FromString(type));
	}

	public void RunAlgorithm(string type)
	{
		if (type.ToUpper() == "FIREWALL") GameController.instance.player.RunFirewall(selectedNode);
		RunAlgorithm(NodeTypeExtension.FromString(type));
	}

	void CameraControls()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			Camera.main.orthographicSize -= zoomStep;
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Camera.main.orthographicSize += zoomStep;
		}

		Camera.main.orthographicSize = Mathf.Clamp(
			Camera.main.orthographicSize,
			minZoom,
			maxZoom
		);

		if (Input.GetMouseButton(2))
		{
			Camera.main.transform.Translate(lastMousePosition -
				Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}

		lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
