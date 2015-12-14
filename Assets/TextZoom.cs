using UnityEngine;
using System.Collections;

public class TextZoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.main.orthographicSize > 3)
		{
			GetComponent<MeshRenderer>().enabled = false;
			Debug.Log("Off");
		}
		//else if (!(GetComponentInParent<Node>().discovered == Seen.UNDISCOVERED))
			GetComponent<MeshRenderer>().enabled = true;

	}
}
