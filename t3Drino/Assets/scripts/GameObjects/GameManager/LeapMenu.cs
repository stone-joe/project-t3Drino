using UnityEngine;
using System.Collections;

public class LeapMenu : MonoBehaviour {

    public GameManager gameManager;
    private Animation anim;

	void Awake () {
		//anim = GetComponent<Animation>();
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
		//anim.Play("LeapMenuCam");
		Invoke("LoadLevel", 1.0F);
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
