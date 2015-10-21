using UnityEngine;
using System.Collections;

public class Classic : MonoBehaviour {

	public GameManager gameManager;

	public GameObject _tetrinoSelector;
	public GameObject _tetrinoPreviewer;
	public GameObject _tetrinoSpawner;
	public GameObject _tetrinoSpawnTimer;
	public GameObject _tetrinoSpawnRateModifier;
	//public GameObject _elapsedTimer;

	public ClassicLoseGUI _loseGUIComponents;
	private bool _inLoseState;

	#region Public Properties
	public bool InLoseState
	{
		get
		{
			return _inLoseState;
		}
		set
		{
			_inLoseState = value;
		}
	}
	#endregion

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
		Debug.Log("Handling state change to: " + gameManager.gameState);
	}
	
	public void LoadMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void StartMainMenu()
	{
		gameManager.SetGameState(GameState.MAINMENU);
		Invoke("LoadMainMenu", 1f);
	}

	public void InitGame()
	{
		_inLoseState = false;

		_tetrinoSelector = new GameObject("TetrinoSelector");
		_tetrinoSelector.AddComponent<TetrinoSelector>();

		_tetrinoPreviewer = new GameObject("TetrinoPreviewer");
		_tetrinoPreviewer.AddComponent<TetrinoPreviewer>();

		_tetrinoSpawnTimer = new GameObject("TetrinoSpawnTimer");
		_tetrinoSpawnTimer.AddComponent<TetrinoSpawnTimer>();

		_tetrinoSpawnRateModifier = new GameObject("TetrinoSpawnRateModifier");
		_tetrinoSpawnRateModifier.AddComponent<TetrinoSpawnRateModifier>();

		_tetrinoSpawner = new GameObject("TetrinoSpawner");
		_tetrinoSpawner.AddComponent<TetrinoSpawner>();
	



		//_elapsedTimer = new GameObject("ElapsedTimer");
		//_elapsedTimer.AddComponent<ElapsedTimer>();
	}

	public void InitLoseScreen()
	{
		// Only enter lose state once
		if (!_inLoseState)
		{
			GameObject obj = new GameObject("ClassicLoseGUI");
			_loseGUIComponents = obj.AddComponent<ClassicLoseGUI>();
			_inLoseState = true;
		}
	}
}
