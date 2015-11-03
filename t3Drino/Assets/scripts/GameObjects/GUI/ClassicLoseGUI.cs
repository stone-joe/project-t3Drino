using UnityEngine;
using System.Collections;

public class ClassicLoseGUI : MonoBehaviour {
	
	private GameObject LoseImage;
	private GameManager gameManager;
	private Texture2D _loseImageTexture;
	private string _loseImagePath;
	private float _buttonWidth;
	private float _buttonHeight;
	private float _numberOfButtons;
	private float _boxWidth;
	private float _boxHeight;
	private TetrinoSpawnTimer _tetrinoSpawnTimer;
	private ElapsedTimer _elapsedTimer;

	void Awake()
	{
		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}

	// Use this for initialization
	void Start () {
		//LoadTextures();
		//InitializeSplashScreenGO();
		//SetSplashScreenLocation(0, 0, 0);
		//ApplySplashScreenTexture();
		StopTetrinoSpawnTimer();
		StopElaspedTimer();

		_buttonWidth = 150;
		_buttonHeight = 30;
		_numberOfButtons = 3;

		_boxWidth = _buttonWidth * 2;
		_boxHeight =  _buttonHeight * (_numberOfButtons + 3);
	}

	#region GUI
	public void OnGUI()
	{
		GUI.BeginGroup(new Rect(Screen.width / 2 - Screen.width * 0.25f, Screen.height / 2 - 200, _boxWidth, _boxHeight));

		GUI.Box (new Rect(0, 0, _boxWidth, _boxHeight), "Game Over!");

		if (GUI.Button(new Rect(10, 40, _buttonWidth, _buttonHeight), "Main Menu"))
			StartMainMenu();

		if (GUI.Button(new Rect(10, 80, _buttonWidth, _buttonHeight), "Replay"))
			ReplayClassicMode();

		if (GUI.Button(new Rect(10, 120, _buttonWidth, _buttonHeight), "Quit Game"))
			Application.Quit();
		
		GUI.EndGroup();
	}
	#endregion

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

	public void ReplayClassicMode(float delay = 1F)
	{
		gameManager.SetGameState(GameState.CLASSIC);
		Invoke("LoadClassicMode", delay);
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

	public void LoadClassicMode()
	{
		Application.LoadLevel("basic_test_scene"); // TODO change scene name
	}

	public void LoadLeapMenu()
	{
		Application.LoadLevel("LeapMenu");
	}
	#endregion

	#region Initialization Methods
	public void LoadTextures()
	{
		_loseImagePath = "Assets/Resources/GUI/TEMP_tetris_block_splash_screen.png"; // TODO: Change this for updating image
		_loseImageTexture = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(_loseImagePath, typeof(Texture2D));
	}
	
	public void InitializeSplashScreenGO()
	{
		LoseImage = GameObject.CreatePrimitive(PrimitiveType.Quad);								// Create base game object of type Quad
		LoseImage.name = "SplashScreen";														// Name base game object
		Destroy(LoseImage.GetComponent("MeshCollider"));										// Remove collider
		LoseImage.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Diffuse");	// Set transparency
	}
	
	private void SetSplashScreenLocation(float x, float y, float z)
	{
		LoseImage.transform.position = new Vector3(x, y, z);
	}
	
	private void ApplySplashScreenTexture()
	{
		// TODO: Find better way to scale, aspect ratio to screen
		int _scaleWidth = 150;
		int _scaleHeight = 150;
		
		float scale = 150;

		LoseImage.GetComponent<Renderer>().material.mainTexture = _loseImageTexture;
		LoseImage.GetComponent<Transform>().localScale = new Vector3(_loseImageTexture.width/scale, 
		                                                             _loseImageTexture.height/scale, 
		                                                             1);
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
