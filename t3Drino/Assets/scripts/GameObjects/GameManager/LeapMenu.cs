using UnityEngine;
using System.Collections;

public class LeapMenu : MonoBehaviour {

    public GameManager gameManager;

	void Awake () {

		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}
	
    void Start () {

    }

	void HandleOnStateChange ()
	{
		Debug.Log("OnStateChange! This is the LeapMenu scene script.");
	}

    public void OnGUI()
	{

	}

	// Event for start button
	public void StartClassicMode()
	{
		gameManager.SetGameState(GameState.CLASSIC);
		Invoke("LoadLevel", 1f);
	}

	public void LoadLevel()
	{
		Application.LoadLevel("basic_test_scene"); // TODO: Change this to name of the main menu scene (to be added)
	}

	public void Quit()
	{
		Debug.Log("Quit!");
		Application.Quit();
	}
}
