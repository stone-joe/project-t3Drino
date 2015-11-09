using UnityEngine;
using System.Collections;

public class ClassicLoseLeapGUI : MonoBehaviour {

	private GameManager gameManager;

	private TetrinoSpawnTimer _tetrinoSpawnTimer;
	private ElapsedTimer _elapsedTimer;
	private ScoreGUI _scoreGUI;

	public GameObject Background;
	
	void Awake()
	{
		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}
	
	// Use this for initialization
	void Start () {

		InitializeBackground();

		SetElapsedTimerGUIToLoseSettings(-8.5f, 20f, -2f);
		StopTetrinoSpawnTimer();
		StopElaspedTimer();

		SetScoreGUIToLoseSettings(-8.5f, 12f, -2f);
	}
	
	#region Switch Scenes and State
	void HandleOnStateChange ()
	{
		Debug.Log("Handling state change to: " + gameManager.gameState);
	}
	
	public void StartMainMenu(float delay = 1F)
	{
		gameManager.SetGameState(GameState.MAINMENU);
		Invoke("LoadMainMenu", delay);
	}
	
	public void StartLeapMenu(float delay = 1F)
	{
		gameManager.SetGameState(GameState.MAINMENU);
		Invoke("LoadLeapMenu", delay);
	}
	#endregion
	
	#region Scene Loaders
	public void LoadMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
	
	public void LoadLeapMenu()
	{
		Application.LoadLevel("LeapMenu");
	}
	#endregion
	
	#region Initialization Methods
	/// <summary>
	/// Initializes the background object
	/// </summary>
	public void InitializeBackground()
	{
		Background = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Background.name = "ClassicLoseLeapGUIBackground";
		Background.transform.position = new Vector3(-0.69f, 10f, -1.75f);
		Background.transform.localScale = new Vector3(25f, 25f, 1f);
		Destroy(Background.GetComponent("MeshCollider"));
	}

	public void SetScoreGUIToLoseSettings(float x, float y, float z)
	{
		GameObject go = GameObject.Find("ScoreGUI");
		_scoreGUI = (ScoreGUI) go.GetComponent(typeof(ScoreGUI));

		// Set location of gui text
		_scoreGUI.transform.position = new Vector3(x, y, z);

		// Apply other properties
		_scoreGUI.FreezeScoreUpdating();
		_scoreGUI.SetFontSize(30);
		_scoreGUI.SetFontStyleToBold();
		_scoreGUI.AlignToCenter();
		_scoreGUI.SetTextColor(Color.blue);
		_scoreGUI.SetText("Your Score:\n" + _scoreGUI.GetCurrentText());
	}

	public void SetElapsedTimerGUIToLoseSettings(float x, float y, float z)
	{
		GameObject go = GameObject.Find("ElapsedTimer");
		_elapsedTimer = (ElapsedTimer) go.GetComponent(typeof(ElapsedTimer));
		
		// Set location of gui text
		_elapsedTimer.transform.position = new Vector3(x, y, z);
		
		// Apply other properties
		_elapsedTimer.FreezeTimeUpdating();
		_elapsedTimer.SetFontSize(30);
		_elapsedTimer.SetFontStyleToBold();
		_elapsedTimer.AlignToCenter();
		_elapsedTimer.SetTextColor(Color.magenta);
		_elapsedTimer.SetText("Total Time:\n" + _elapsedTimer.GetCurrentText());
	}
	
	private void StopTetrinoSpawnTimer()
	{
		GameObject go = GameObject.Find("TetrinoSpawnTimer");
		_tetrinoSpawnTimer = (TetrinoSpawnTimer) go.GetComponent(typeof(TetrinoSpawnTimer));
		_tetrinoSpawnTimer.Stop();
	}
	
	private void StopElaspedTimer()
	{
		GameObject go = GameObject.Find("ElapsedTimer");
		_elapsedTimer = (ElapsedTimer) go.GetComponent(typeof(ElapsedTimer));
		_elapsedTimer.Stop();
	}
	#endregion
}
