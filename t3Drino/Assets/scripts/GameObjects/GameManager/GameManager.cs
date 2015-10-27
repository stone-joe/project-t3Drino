using UnityEngine;
using System.Collections;

public enum GameState { INTRO, MAINMENU, CLASSIC }
public delegate void OnStateChangeHandler();

public class GameManager : MonoBehaviour {

	private static GameManager _instance = null;
	public event OnStateChangeHandler OnStateChange;
	public GameState gameState { get; private set; }

	// Singleton pattern
	public static GameManager Instance
	{
		get {

			if (GameManager._instance == null)
			{
				GameObject manager= new GameObject("GameManager");
				GameManager._instance = manager.AddComponent<GameManager>();
				DontDestroyOnLoad(GameManager._instance);
			}

			return GameManager._instance;
		}
	}

	public void SetGameState(GameState gameState)
	{
		this.gameState = gameState;
		OnStateChange();
	}

	public void OnApplicationQuit()
	{
		GameManager._instance = null;
	}
}
