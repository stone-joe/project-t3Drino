using UnityEngine;
using System.Collections;

public class ClassicLoseGUIComponents : MonoBehaviour {
	
	public GameObject LoseImage;
	private GameManager gameManager;
	
	public Texture2D _loseImageTexture;
	public string _loseImagePath;

	void Awake()
	{
		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}

	// Use this for initialization
	void Start () {
		LoadTextures();
		InitializeSplashScreenGO();
		SetSplashScreenLocation(0, 0, 0);
		ApplySplashScreenTexture();
	}

	public void OnGUI()
	{
		// Menu Layout
		GUI.BeginGroup(new Rect(Screen.width / 2 - Screen.width * 0.25f, Screen.height / 2 - 200, Screen.width * 0.50f, Screen.height * 0.75f));
		GUI.Box (new Rect(0, 0, Screen.width * 0.75f, Screen.height * 0.70f), "GAME OVER!");

		if (GUI.Button(new Rect(10, 40, 150, 150), "Main Menu"))
		{
			StartMainMenu();
		}

		if (GUI.Button(new Rect(10, 160, 150, 150), "Quit"))
		{
			Application.Quit();
		}
		
		GUI.EndGroup();
	}

	void OnDestroy()
	{

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
	
	public void LoadTextures()
	{
		_loseImagePath = "Assets/Resources/GUI/TEMP_tetris_block_splash_screen.png"; // TODO: Change this for updating image
		_loseImageTexture = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(_loseImagePath, typeof(Texture2D));
	}
	
	public void InitializeSplashScreenGO()
	{
		LoseImage = GameObject.CreatePrimitive(PrimitiveType.Quad);							// Create base game object of type Quad
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
}
