using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	private float _currentScore;
	private float _highScore;

	#region Public Properties
	public float CurrentScore
	{
		get
		{
			return _currentScore;
		}

		set
		{
			_currentScore = value;
		}
	}

	public float HighScore
	{
		get
		{
			return _highScore;
		}
		
		set
		{
			_highScore = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		_currentScore = 0;
		_highScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
