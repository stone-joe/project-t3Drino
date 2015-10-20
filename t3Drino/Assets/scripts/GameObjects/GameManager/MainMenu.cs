using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameManager gameManager;

	void Awake () {

		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}
	
	void HandleOnStateChange ()
	{
		Debug.Log("OnStateChange! This is the MainMenu scene script.");
	}

	public void OnGUI()
	{
		GUI.BeginGroup(new Rect(Screen.width / 2 - Screen.width * 0.25f, Screen.height / 2 - 200, Screen.width * 0.50f, Screen.height * 0.75f));

		GUI.Box (new Rect(0, 0, Screen.width * 0.75f, Screen.height * 0.70f), "Main Menu");

		if (GUI.Button(new Rect(10, 40, 150, 150), "Classic Mode"))
			StartClassicMode();

		if (GUI.Button(new Rect(10, 160, 150, 150), "Quit"))
			Quit();

		GUI.EndGroup();
	}

	// Event for start button
	public void StartClassicMode()
	{
		gameManager.SetGameState(GameState.CLASSIC);
		Invoke("LoadLevel", 1f);
	}

	public void LoadLevel()
	{
		Application.LoadLevel("basic_test_scene"); // TODO: Change this to name of the main menu scene (to be added)
	}

	public void Quit()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
}
