using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{

	// Use this for initialization

	public int minZoom = 1;
	public int maxZoom = 10;
	public int zoomStep = 1;
	public int cameraStartSize = 3;

	public Node selectedNode;

	private Vector3 lastMousePosition;

	void Start()
	{
		Camera.main.orthographicSize = cameraStartSize;
	}

	// Update is called once per frame
	void Update()
	{
		CameraControls();
		SelectNode();
	}

	void SelectNode()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(
				Camera.main.ScreenToWorldPoint(Input.mousePosition),
				Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Node"));
			if (hit)
			{
				selectedNode = hit.transform.GetComponent<Node>();
				GameController.instance.player.Explore(selectedNode);
			}
		}
	}

	public void RunProgram(ProgramType type)
	{

	}

	public void RunAlgorithm(NodeType type)
	{

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
