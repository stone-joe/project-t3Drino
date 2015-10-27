using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    // box dimensions
    public float boxWidthPercentage;
    public float boxHeightPercentage;

    private float boxWidth;
    private float boxHeight;

    // button dimensions
    public float buttonWidthPercentage;

    private float buttonCount;
    private float buttonWidth;
    private float buttonHeight;

    // button positioning
    public float buttonTopPaddingPercentage;
    public float buttonBetweenPercentage;

    private float buttonSidePadding;
    private float buttonTopPadding;
    private float buttonBetweenPadding;

    public GameManager gameManager;

	void Awake () {

		gameManager = GameManager.Instance;
		gameManager.OnStateChange += HandleOnStateChange;
	}
	
    void Start () {
        boxWidthPercentage = 0.75f;
        boxHeightPercentage = 0.70f;
        buttonWidthPercentage = 0.90f;
        buttonTopPaddingPercentage = 0.07f;
        buttonBetweenPercentage = 0.05f;
        buttonCount = 2;
    }

	void HandleOnStateChange ()
	{
		Debug.Log("OnStateChange! This is the MainMenu scene script.");
	}

    public void OnGUI()
	{
        // setting box dimensions
        boxWidth = Screen.width * boxWidthPercentage;
        boxHeight = Screen.height * boxHeightPercentage;

        // setting button positions
        buttonSidePadding = boxWidth * ((1 - buttonWidthPercentage) / 2); //also used as bottom padding
        buttonTopPadding = boxHeight * buttonTopPaddingPercentage;

        // setting button dimensions
        buttonWidth = boxWidth * buttonWidthPercentage;
        buttonHeight = (boxHeight - (buttonTopPadding + buttonBetweenPadding * (buttonCount - 1) + buttonSidePadding)) / buttonCount;
       
        buttonBetweenPadding = buttonHeight * buttonBetweenPercentage;

        GUI.BeginGroup(new Rect(Screen.width * ((1 - boxWidthPercentage) / 2), Screen.height * ((1 - boxHeightPercentage)/2), boxWidth, boxHeight));

		GUI.Box (new Rect(0, 0, boxWidth, boxHeight), "Main Menu");
        
		if (GUI.Button(new Rect(buttonSidePadding, buttonTopPadding, buttonWidth, buttonHeight), "Classic Mode"))
			StartClassicMode();

        if (GUI.Button(new Rect(buttonSidePadding, buttonTopPadding + buttonHeight + buttonBetweenPadding, buttonWidth, buttonHeight), "Quit"))
			Quit();

		GUI.EndGroup();
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
