using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	// Note: Should attach this to main camera of the very first scene. This will load to main menu after x amount of seconds (see Invoke below)

	public GameManager gameManager;

	// Use this for initialization
	void Awake () {

		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}

	void HandleOnStateChange ()
	{
		gameManager.SetGameState(GameState.MAINMENU);
		Debug.Log("Handling state change to: " + gameManager.gameState);
		Invoke("LoadLevel", 3f);
	}

	public void LoadLevel()
	{
		Application.LoadLevel("MainMenu"); // TODO: Change this to name of the main menu scene (to be added)
	}
}
