using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public TetrinoSelector _tetrinoSelector;
	public TetrinoPreviewer _tetrinoPreviewer;

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
		GameObject obj = new GameObject("TetrinoSelector");
		_tetrinoSelector =  obj.AddComponent<TetrinoSelector>();
		obj = new GameObject("TetrinoPreviewer");
		_tetrinoPreviewer =  obj.AddComponent<TetrinoPreviewer>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
