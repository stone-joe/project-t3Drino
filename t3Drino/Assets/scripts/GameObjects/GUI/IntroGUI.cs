using UnityEngine;
using System.Collections;

public class IntroGUI : MonoBehaviour {

	public GameObject SplashScreen;

	public Texture2D _splashScreenTexture;
	public string _splashScreenPath;

   

	// Use this for initialization
	void Start () {
		LoadTextures();
		InitializeSplashScreenGO();
		SetSplashScreenLocation(0, 0, 0);
		ApplySplashScreenTexture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadTextures()
	{
		_splashScreenPath = "GUI/TEMP_tetris_block_splash_screen";
        _splashScreenTexture = Resources.Load(_splashScreenPath) as Texture2D;


	}

	public void InitializeSplashScreenGO()
	{
		SplashScreen = GameObject.CreatePrimitive(PrimitiveType.Quad);							// Create base game object of type Quad
		SplashScreen.name = "SplashScreen";														// Name base game object
		Destroy(SplashScreen.GetComponent("MeshCollider"));										// Remove collider
		SplashScreen.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Diffuse");	// Set transparency
	}

	private void SetSplashScreenLocation(float x, float y, float z)
	{
		SplashScreen.transform.position = new Vector3(x, y, z);
	}

	private void ApplySplashScreenTexture()
	{
		// TODO: Find better way to scale, aspect ratio to screen
		int _scaleWidth = 150;
		int _scaleHeight = 150;

		float scale = 150;

		SplashScreen.GetComponent<Renderer>().material.mainTexture = _splashScreenTexture;
		SplashScreen.GetComponent<Transform>().localScale = new Vector3(_splashScreenTexture.width/scale, 
																		_splashScreenTexture.height/scale, 
																		1);
	}
}
