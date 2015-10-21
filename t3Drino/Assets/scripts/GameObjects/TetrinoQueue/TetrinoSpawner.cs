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
	private TetrinoSelector _tetrinoSelector;
	private TetrinoSpawnTimer _tetrinoSpawnTimer;
	private TetrinoSpawnRateModifier _tetrinoSpawnRateModifier;

    // Use this for initialization
    void Start () {

		// Set to spawn every 3 seconds
		_period = 3f;
		_decreaseAmount = 0.20f;		// Decrease by this many seconds (for time between spawns)
		_intervalPerDecrease = 30f;		// Second interval to up the difficulty (a.k.a. decrease the spawn rate by _decreaseAmont time)
		_nextIntervalDecreaseTime = 0f + _intervalPerDecrease;

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
    }

    void FixedUpdate()
    {
		// Should we pop the next tetromino from the queue?
		if (_tetrinoSpawnTimer.ShouldPop)
		{
			GameObject go = _tetrinoSelector.Pop();	// I think we should :)
			Instantiate(go, transform.position, transform.rotation);
			_tetrinoSpawnTimer.ShouldPop = false; 	// Stop da pop
		}

		if (_tetrinoSpawnTimer.CurrentTime > _nextIntervalDecreaseTime)
		{
			ApplyDifficultyModifierToTimer();
			//Debug.Log("Spawn timer current time: " + _tetrinoSpawnTimer.CurrentTime);
			//Debug.Log ("Spawn timer interval between spawn: " + _tetrinoSpawnTimer.IntervalBetweenSpawn);
		}
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
