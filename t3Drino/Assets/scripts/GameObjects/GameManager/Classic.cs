using UnityEngine;
using System.Collections;

public class Classic : MonoBehaviour {

	public GameManager gameManager;

	public GameObject _tetrinoSelector;
	public GameObject _tetrinoPreviewer;
	public GameObject _tetrinoSpawner;

	public ClassicLoseGUIComponents _loseGUIComponents;

	// Use this for initialization
	void Awake () {
		
		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}

	void Start()
	{
		InitGame();
		InitLoseScreen(); // Should only be called when we enter lose state
	}
	
	void HandleOnStateChange ()
	{
		Debug.Log("Handling state change to: " + gameManager.gameState);
	}
	
	public void LoadMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void StartMainMenu()
	{
		gameManager.SetGameState(GameState.MAINMENU);
		Debug.Log(gameManager.gameState);
		Invoke("LoadMainMenu", 1f);
	}

	public void InitGame()
	{
		_tetrinoSelector = new GameObject("TetrinoSelector");
		_tetrinoSelector.AddComponent<TetrinoSelector>();

		_tetrinoPreviewer = new GameObject("TetrinoPreviewer");
		_tetrinoPreviewer.AddComponent<TetrinoPreviewer>();

		_tetrinoSpawner = new GameObject("TetrinoSpawner");
		_tetrinoSpawner.AddComponent<Spawner>();
	}

	public void InitLoseScreen()
	{
		GameObject obj = new GameObject("ClassicLoseGUIComponents");
		_loseGUIComponents = obj.AddComponent<ClassicLoseGUIComponents>();
	}
}
