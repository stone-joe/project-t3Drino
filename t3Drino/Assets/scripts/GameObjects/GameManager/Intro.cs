using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	
	public GameManager gameManager;

	private IntroGUI _guiComponents;

	void Awake () {
		
		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;

		Debug.Log("Current game state when Awakes: " + gameManager.gameState);

		InitSplashScreen();
	}

	void Start () {
		Debug.Log("Current game state when Starts: " + gameManager.gameState);
		gameManager.SetGameState(GameState.MAINMENU);
		Invoke("LoadLevel", 3f);
	}
	
	void HandleOnStateChange ()
	{
		Debug.Log("Handling state change to: " + gameManager.gameState);
	}
	
	public void LoadLevel()
	{
		Application.LoadLevel("MainMenu");
	}

	public void InitSplashScreen()
	{
		GameObject obj = new GameObject("IntroGUI");
		_guiComponents = obj.AddComponent<IntroGUI>();
	}
}
