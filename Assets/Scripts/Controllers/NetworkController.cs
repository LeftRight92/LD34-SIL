using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NetworkController : MonoBehaviour {

	public static NetworkController instance;

	public bool AutoMode = true;
	public int maxNodes;
	public GameObject nodePrefab;
	public GameObject edgePrefab;
	public Vector2 worldDimensions;

	public List<Node> nodes { get; private set; }
	public Node playerStart;// { get; private set; }
	public Node enemyStart;// { get; private set; }

	

	private Dictionary<Vector3, Vector3> connectedAssuranceLines = new Dictionary<Vector3, Vector3>();

	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	public void Create() {
		nodes = new List<Node>();
		if(AutoMode)
		{
			CreateNodes();
			CreateConnections();
			ChooseStarts();
			DrawConnections();
			foreach (Node n in nodes.Where(n => !(n == playerStart)))
				n.HideAtStart();
			Debug.Log("Ready");
		}
		else
		{
			nodes = GameObject.FindGameObjectsWithTag("Node").Select(n => n.GetComponent<Node>()).ToList();
			DrawConnections();
			foreach (Node n in nodes.Where(n => !(n == playerStart)))
				n.HideAtStart();
			if (playerStart == null) Debug.LogError("Player Start node set to null in manual mode");
			if (enemyStart == null) Debug.LogError("Enemy Start node set to null in manual mode");
		}
		
	}

	private void CreateNodes()
	{
		for (int x = 0; x < maxNodes; x++)
			nodes.Add(((GameObject)
				Instantiate(nodePrefab,
					new Vector3(
						Random.Range(-(worldDimensions.x / 2), worldDimensions.x / 2),
						Random.Range(-(worldDimensions.y / 2), worldDimensions.y / 2),
						0
					),
					Quaternion.identity)
				).
			GetComponent<Node>());
		foreach (Node n in nodes)
			n.transform.parent = transform;
	}

	private void CreateConnections()
	{
		//Initial Connections
		foreach (Node n in nodes)
		{
			List<Node> currentNeighbours = n.neighbours;
			List<Node> newNeighbours = Physics2D.OverlapCircleAll(n.transform.position, 100f)
				.Select(x => x.GetComponent<Node>())
				.Where(x => x != n && !currentNeighbours.Contains(x))
				.OrderBy(x => Vector3.Magnitude(n.transform.position - x.transform.position))
				.Take(Random.Range(1, 3)).ToList();
			foreach (Node o in newNeighbours) o.neighbours.Add(n);
			n.neighbours = currentNeighbours.Concat(newNeighbours).ToList();
		}

		//Ensure connectedness
		HashSet<Node> connected = new HashSet<Node>();
		HashSet<Node> front = new HashSet<Node>();
		connected.Add(nodes[0]);
		front.Add(nodes[0]);
		while (!connected.SetEquals(nodes))
		{
			if (front.Count == 0)
			{
				//Our front is empty but we are not fully connected, there must be islands
				//Collect all nodes not connected to the main graph
				HashSet<Node> notConnected = new HashSet<Node>(nodes);
				notConnected.ExceptWith(connected);
				Debug.Log("Running Not Connected branch, size: " + notConnected.Count);
				//Take any node
				Node anyNode = notConnected.ToArray()[0];
				//Add a new neighbour from the connected graph
				Node newNeighbour = connected
					.OrderBy(x => Vector3.Magnitude(anyNode.transform.position - x.transform.position))
					.Take(1).ToArray()[0];
				anyNode.neighbours.Add(newNeighbour);
				//Add the any node to the connected nodes neighbours
				newNeighbour.neighbours.Add(anyNode);
				//Add the any node to the front
				connected.Add(anyNode);
				front.Add(anyNode);
				//DEBUG
				connectedAssuranceLines.Add(anyNode.transform.position, newNeighbour.transform.position);
			}
			HashSet<Node> newFront = new HashSet<Node>();
			foreach (Node n in front)
			{
				foreach (Node o in n.neighbours)
					if (!connected.Contains(o))
					{
						newFront.Add(o);
						connected.Add(o);
					}
			}
			front = newFront;
			Debug.Log("New front: " + front.Count);
		}
	}

	private void DrawConnections()
	{
		Dictionary<LinkRef, Edge> edgeMap = new Dictionary<LinkRef, Edge>();
		foreach(Node n in nodes)
		{
			foreach (Node o in n.neighbours)
			{
				Edge e;
				if(edgeMap.TryGetValue(new LinkRef(n, o), out e))
				{
					o.edges.Add(e);
				}
				else
				{
					GameObject g = Instantiate(edgePrefab, o.transform.position, Quaternion.identity) as GameObject;
					g.transform.parent = transform;
					LineRenderer line = g.GetComponent<LineRenderer>();
					line.SetPositions(new Vector3[]
					{
						o.transform.position,
						n.transform.position
					});
					line.enabled = false;
					edgeMap.Add(new LinkRef(o, n), g.GetComponent<Edge>());
					o.edges.Add(g.GetComponent<Edge>());
				}
			}
		}
	}

	private void ChooseStarts()
	{
		Vector3 topLeft = new Vector3(-worldDimensions.x / 2, worldDimensions.y / 2, 0);
		Vector3 bottomRight = new Vector3(worldDimensions.x / 2, -worldDimensions.y / 2, 0);
		playerStart = Physics2D.OverlapCircleAll(
			bottomRight, worldDimensions.x)
				.Select(x => x.GetComponent<Node>())
				.OrderBy(x => Vector3.Magnitude(bottomRight - x.transform.position))
				.Take(1).ToArray()[0];
		enemyStart = Physics2D.OverlapCircleAll(
			topLeft, worldDimensions.x)
				.Select(x => x.GetComponent<Node>())
				.OrderBy(x => Vector3.Magnitude(topLeft - x.transform.position))
				.Take(1).ToArray()[0];
	}
	
	// Update is called once per frame
	void Update () {
		foreach (KeyValuePair<Vector3, Vector3> p in connectedAssuranceLines)
			Debug.DrawLine(p.Key + Vector3.one * 0.1f, p.Value + Vector3.one * 0.1f, Color.red);
		Debug.DrawLine(enemyStart.transform.position, new Vector3(-worldDimensions.x / 2, worldDimensions.y / 2, 0), Color.green);
		Debug.DrawLine(playerStart.transform.position, new Vector3(worldDimensions.x / 2, -worldDimensions.y / 2, 0), Color.yellow);
	}
}

struct LinkRef
{
	public Node n1, n2;

	public LinkRef(Node n1, Node n2)
	{
		this.n1 = n1;
		this.n2 = n2;
	}
}
