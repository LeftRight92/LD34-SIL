using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CampaignUISwitch : MonoBehaviour {

	public bool active = true;
	private string words;

	// Use this for initialization
	void Start () {
		if (!active)
		{
			gameObject.SetActive(false);
		}
	}
	
	public void SetEnable(bool val)
	{
		if (active)
		{
			gameObject.SetActive(val);
		}
	}

	public void SetActive()
	{
		gameObject.SetActive(true);
		active = true;
	}

}
