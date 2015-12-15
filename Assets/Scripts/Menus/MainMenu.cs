using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void LoadCampaign()
	{
		Debug.Log("Boop");
		SceneManager.LoadScene("Level1");
	}

	public void LoadFreeplay()
	{
		SceneManager.LoadScene("Freeplay");
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}
}
