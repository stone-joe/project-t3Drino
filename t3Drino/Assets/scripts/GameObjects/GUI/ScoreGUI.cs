using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

	//private GameObject _scoreGO;
	private Score _score;
	private TextMesh _txtMesh;

	// Use this for initialization
	void Start () {
		InitializeScoreGO();
		FindScoreGO();
	}
	
	// Update is called once per frame
	void Update () {
		_txtMesh.text = _score.CurrentScore.ToString();
	}

	public void InitializeScoreGO()
	{
		gameObject.transform.position = new Vector3(13.55f, 16f, 0f);			// Set position of text/object

		Destroy(gameObject.GetComponent("MeshCollider"));						// Remove collider

		gameObject.AddComponent<TextMesh>();									// Add a TextMesh
		_txtMesh = gameObject.GetComponent<TextMesh>();							// Get reference to the TextMesh of the Score GUI GameObject
		_txtMesh.fontSize = 20;													// Set font size
		_txtMesh.font = Resources.GetBuiltinResource<Font>("Arial.ttf");		// Apply font
		_txtMesh.color = Color.yellow;											// Set text color

		gameObject.GetComponent<Renderer>().material = _txtMesh.font.material;	// Apply material to renderer
	}

	public void FindScoreGO()
	{
		GameObject go = GameObject.Find("Score");
		_score = (Score) go.GetComponent(typeof(Score));
	}
}
