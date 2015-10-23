using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private Player _player;
	private Score _score;
	private ScoreGUI _scoreGUI;

	// Use this for initialization
	void Start () {
		InitManager();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitManager()
	{
		GameObject go1 = GameObject.Find("Score");
		_score = (Score) go1.GetComponent(typeof(Score));

		GameObject go2 = GameObject.Find("Player");
		_player = (Player) go2.GetComponent(typeof(Player));

		GameObject go3 = GameObject.Find("ScoreGUI");
		_scoreGUI = (ScoreGUI) go3.GetComponent(typeof(ScoreGUI));
	}

	public void AddToScore(float addValue)
	{
		_player.CurrentScore += addValue;
	}

	public void SubtractFromScore(float subtractValue)
	{
		_player.CurrentScore -= subtractValue;
	}
}
