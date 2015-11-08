using UnityEngine;
using System.Collections;

public class Classic : MonoBehaviour {

	public GameManager gameManager;

	public GameObject _tetrinoSelector;
	public GameObject _tetrinoPreviewer;
	public GameObject _tetrinoSpawner;
	public GameObject _tetrinoSpawnTimer;
	public GameObject _tetrinoSpawnRateModifier;

	public GameObject _score;
	public GameObject _player;
	public GameObject _scoreManager;
	public GameObject _scoreGUI;

	public GameObject _lightPrefab;
	//public GameObject _elapsedTimer;

	//public ClassicLoseGUI _loseGUIComponents;
	public ClassicLoseLeapGUI _loseLeapGUIComponents; // Lose screen GUI for leap version
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
		Instantiate(_lightPrefab);
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

		// Tetromino controls
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
	

		// Player score and score manager
		_score = new GameObject("Score");
		_score.AddComponent<Score>();

		_player = new GameObject("Player");
		_player.AddComponent<Player>();

		_scoreGUI = new GameObject("ScoreGUI");
		_scoreGUI.AddComponent<ScoreGUI>();

		_scoreManager = new GameObject("ScoreManager");
		_scoreManager.AddComponent<ScoreManager>();

		// Ray casters
		Instantiate(Resources.Load("RayCasters/RayCasters"));


		//_elapsedTimer = new GameObject("ElapsedTimer");
		//_elapsedTimer.AddComponent<ElapsedTimer>();
	}

	public void InitLoseScreen()
	{
		// Only enter lose state once
		if (!_inLoseState)
		{
			_inLoseState = true;
			_scoreManager.GetComponent<ScoreManager>().ScoreIsFrozen = true; // Freeze score

			// Enable these only for normal gameplay
			//GameObject obj = new GameObject("ClassicLoseGUI");
			//_loseGUIComponents = obj.AddComponent<ClassicLoseGUI>();

			// Adjust lose gui - leap version
			GameObject obj = new GameObject("ClassicLoseLeapGUI");
			_loseLeapGUIComponents = obj.AddComponent<ClassicLoseLeapGUI>();

			// Redirect to leap menu after 5 seconds
			//_loseGUIComponents.StartLeapMenu(5.0F);
			_loseLeapGUIComponents.StartLeapMenu(5.0F);
		}
	}
}
