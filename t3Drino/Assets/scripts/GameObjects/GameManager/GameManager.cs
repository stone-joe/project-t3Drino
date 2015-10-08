using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private int _level = 1;
	//public BoardManager boardScript; // For referecing later

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		DontDestroyOnLoad(gameObject); // Allows persist between scenes

		//boardScript = GetComponent<BoardManager>();
		InitGame();
	}

	public void InitGame()
	{
		//boardScript.SetupScene(_level);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
