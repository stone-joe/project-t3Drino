//Nabil spawner
//this is just a test
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetrinoSpawner : MonoBehaviour {

    private float _period;
	private float _decreaseAmount;
	private float _intervalPerDecrease;
	private float _nextIntervalDecreaseTime;
	private float _minTimeBetweenSpawns;
	private TetrinoSelector _tetrinoSelector;
	private TetrominoState tetrominoState;
	private TetrinoSpawnTimer _tetrinoSpawnTimer;
	private TetrinoSpawnRateModifier _tetrinoSpawnRateModifier;
	private Classic _classicModeState;
	private ScoreManager _scoreManager;


    // Use this for initialization
    void Start () {

		// Set to spawn every 4 seconds
		_period = 5f;
		_decreaseAmount = 0.10f;		// Decrease by this many seconds (for time between spawns)
		_intervalPerDecrease = 30f;		// Second interval to up the difficulty (a.k.a. decrease the spawn rate by _decreaseAmont time)
		_nextIntervalDecreaseTime = 0f + _intervalPerDecrease;
		_minTimeBetweenSpawns = 1.5f;

		// Set spawn location
		transform.position = new Vector3(0f, 21.80f, 0f);

		// Grab reference of the tetromino queue
		GameObject go = GameObject.Find("TetrinoSelector");
		_tetrinoSelector = (TetrinoSelector) go.GetComponent(typeof(TetrinoSelector));

		// Grab reference of the tetromino spawn timer
		GameObject go2 = GameObject.Find("TetrinoSpawnTimer");
		_tetrinoSpawnTimer = (TetrinoSpawnTimer) go2.GetComponent(typeof(TetrinoSpawnTimer));
		_tetrinoSpawnTimer.SetTimer(0f, float.PositiveInfinity, 0f, _period);
		_tetrinoSpawnTimer.BeginCountUp();

		// Grab reference of the tetromino spawn rate modifier (for adjusting difficulty on the spawn timer)
		GameObject go3 = GameObject.Find("TetrinoSpawnRateModifier");
		_tetrinoSpawnRateModifier = (TetrinoSpawnRateModifier) go3.GetComponent(typeof(TetrinoSpawnRateModifier));
		_tetrinoSpawnRateModifier.SetDecreaseAmount(_decreaseAmount);				// Decrease by _decreaseAmount seconds at a set interval
		_tetrinoSpawnRateModifier.SetIntervalPerDecrease(_intervalPerDecrease);		// Every _intervalPerDecrease seconds, decrease the interval by _decreaseAmount seconds

		// Get Classic mode state to check if game is over
		GameObject go4 = GameObject.Find("Main Camera");
		_classicModeState = (Classic)go4.GetComponent(typeof(Classic));

		// Get score manager reference
		GameObject go5 = GameObject.Find("ScoreManager");
		_scoreManager = (ScoreManager) go5.GetComponent(typeof(ScoreManager));
    }

    void FixedUpdate()
    {    
		// Checking current spawn interval time
		//Debug.Log("Current spawn time: " + _tetrinoSpawnTimer.IntervalBetweenSpawn);

		// Should we pop the next tetromino from the queue?
		if (_tetrinoSpawnTimer.ShouldPop)
		{
			GameObject go = _tetrinoSelector.Pop();	// I think we should :)
			tetrominoState = go.GetComponent<TetrominoState>();
			tetrominoState.setState(TetrominoState.states.ACTIVE);
			Instantiate(go, transform.position, transform.rotation); // Pop goes the weasle!
			_tetrinoSpawnTimer.ShouldPop = false; 	// Stop da pop
		}

		// Apply modifier to rate if the interval between spawn is longer than 1 second, we have not lost yet, and if its time to update...
		// 1 second is the minimum amount of time between spawns
		// Afterwhich, we no longer decrease time
		if (_tetrinoSpawnTimer.CurrentTime > _nextIntervalDecreaseTime
		    && _tetrinoSpawnTimer.IntervalBetweenSpawn > _minTimeBetweenSpawns
		    && !_classicModeState.InLoseState)
		{
			ApplyDifficultyModifierToTimer();
			_scoreManager.SetScoreToAdd(_scoreManager.ScoreToAdd + 100f);
			//Debug.Log("Spawn timer current time: " + _tetrinoSpawnTimer.CurrentTime);
			//Debug.Log ("Spawn timer interval between spawn: " + _tetrinoSpawnTimer.IntervalBetweenSpawn);
		}

		// We can do something similar here for applying a faster falling speed for the tetrominoes
    }

	public void SetSpawnerLocation(Vector3 vector)
	{
		transform.position = vector;
	}

	private void ApplyDifficultyModifierToTimer()
	{
		_nextIntervalDecreaseTime += _tetrinoSpawnRateModifier.IntervalPerDecrease;				// Update for the next time we speed up the spawn rate
		_tetrinoSpawnTimer.IntervalBetweenSpawn -= _tetrinoSpawnRateModifier.DecreaseAmount;	// Decrease the time interval between spawns
	}
}
