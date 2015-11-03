using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	private Player _player;
	private Score _score;
	private ScoreGUI _scoreGUI;
	private bool _scoreIsFrozen;
	private float _scoreToAdd;

	#region Public Properties
	public bool ScoreIsFrozen
	{
		get
		{
			return _scoreIsFrozen;
		}

		set
		{
			_scoreIsFrozen = value;
		}
	}

	public float ScoreToAdd
	{
		get
		{
			return _scoreToAdd;
		}
		
		set
		{
			_scoreToAdd = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		InitManager();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitManager()
	{
		_scoreIsFrozen = false;
		_scoreToAdd = 100f;

		GameObject go1 = GameObject.Find("Score");
		_score = (Score) go1.GetComponent(typeof(Score));

		GameObject go2 = GameObject.Find("Player");
		_player = (Player) go2.GetComponent(typeof(Player));

		GameObject go3 = GameObject.Find("ScoreGUI");
		_scoreGUI = (ScoreGUI) go3.GetComponent(typeof(ScoreGUI));
	}

	public void SetScoreToAdd(float score){
		_scoreToAdd = score;
	}

	public void AddToScore(float addValue)
	{
		if (!ScoreIsFrozen)
			_player.CurrentScore += addValue;
	}

	public void SubtractFromScore(float subtractValue)
	{
		if (!ScoreIsFrozen)
			_player.CurrentScore -= subtractValue;
	}
}
