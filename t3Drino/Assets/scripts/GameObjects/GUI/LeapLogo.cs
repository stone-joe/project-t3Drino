using UnityEngine;
using System.Collections;

public class LeapLogo : MonoBehaviour {

	public GameObject SplashScreen;

	public Texture2D _splashScreenTexture;
	public string _splashScreenPath;

	// Use this for initialization
	void Start () {
		LoadTextures();
		InitializeSplashScreenGO();
		SetSplashScreenLocation(0, 8, 14);
		ApplySplashScreenTexture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadTextures()
	{
		_splashScreenPath = "GUI/logo"; // TODO: Change this for updating image
		_splashScreenTexture = Resources.Load<Texture2D>(_splashScreenPath);
	}

	public void InitializeSplashScreenGO()
	{
		SplashScreen = GameObject.CreatePrimitive(PrimitiveType.Quad);							// Create base game object of type Quad
		SplashScreen.name = "SplashScreen";														// Name base game object
		Destroy(SplashScreen.GetComponent("MeshCollider"));										// Remove collider
		SplashScreen.GetComponent<Renderer>().material = Resources.Load<Material>("Shaders/SpriteTransparentMaterial");	// Set transparency
	}

	private void SetSplashScreenLocation(float x, float y, float z)
	{
		SplashScreen.transform.position = new Vector3(x, y, z);
	}

	private void ApplySplashScreenTexture()
	{
		// TODO: Find better way to scale, aspect ratio to screen
		int _scaleWidth = 35;
		int _scaleHeight = 35;

		float scale = 35;

		SplashScreen.GetComponent<Renderer>().material.mainTexture = _splashScreenTexture;
		SplashScreen.GetComponent<Transform>().localScale = new Vector3(_splashScreenTexture.width/scale, 
		                                                                _splashScreenTexture.height/scale, 
		                                                                1);
	}
}
