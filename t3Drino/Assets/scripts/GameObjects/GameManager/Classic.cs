using UnityEngine;
using System.Collections;

public class Classic : MonoBehaviour {

	public GameManager gameManager;

	public TetrinoSelector _tetrinoSelector;
	public TetrinoPreviewer _tetrinoPreviewer;
	public Spawner _tetrinoSpawner;

	// Use this for initialization
	void Awake () {
		
		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}

	void Start()
	{
		InitGame();
	}
	
	void HandleOnStateChange ()
	{
		gameManager.SetGameState(GameState.MAINMENU);
		Debug.Log("Handling state change to: " + gameManager.gameState);
		Invoke("LoadLevel", 3f);
	}
	
	public void LoadLevel()
	{
		Application.LoadLevel("MainMenu");
	}

	public void InitGame()
	{
		GameObject obj = new GameObject("TetrinoSelector");
		_tetrinoSelector = obj.AddComponent<TetrinoSelector>();
		
		obj = new GameObject("TetrinoPreviewer");
		_tetrinoPreviewer = obj.AddComponent<TetrinoPreviewer>();

		obj = new GameObject("Spawner");
		_tetrinoSpawner = obj.AddComponent<Spawner>();
	}
}
