using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Score _score;

	#region Public Properties
	public float CurrentScore
	{
		get
		{
			return _score.CurrentScore;
		}

		set
		{
			_score.CurrentScore = value;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		InitPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitPlayer()
	{
		GameObject go = GameObject.Find("Score");
		_score = (Score) go.GetComponent(typeof(Score));

		CurrentScore = 0;
	}
}
