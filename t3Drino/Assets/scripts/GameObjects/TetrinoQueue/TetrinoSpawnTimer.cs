using UnityEngine;
using System.Collections;

public class TetrinoSpawnTimer : Timer {

	private float _intervalBetweenSpawn; // In seconds
	private float _nextSpawnTime; // In seconds
	private bool _shouldPop;

	#region Public Properties
	public float IntervalBetweenSpawn
	{
		get 
		{
			return _intervalBetweenSpawn;
		}

		set
		{
			_intervalBetweenSpawn = value;
		}
	}

	public float NextSpawnTime
	{
		get
		{
			return Mathf.FloorToInt(_nextSpawnTime);
		}

		set
		{
			_nextSpawnTime = value;
		}
	}

	public bool ShouldPop
	{
		get
		{
			return _shouldPop;
		}

		set
		{
			_shouldPop = value;
		}
	}
	#endregion

	public override void Start ()
	{
		base.Start();
		_shouldPop = false;
	}
	
	void FixedUpdate () 
	{
		UpdateTime();
		if (CurrentTimetoSeconds() > NextSpawnTime)
		{
			// Notify that a tetrino should be popped
			_shouldPop = true;
			NextSpawnTime += IntervalBetweenSpawn;
			NextSpawnTime = 10000f;
		}
	}

	// Example usage: SetTimer(0f, float.PositiveInfinity, 0f);
	public void SetTimer(float startTime, float endTime, float currentTime, float intervalBetweenSpawn)
	{
		StartTime = startTime;
		EndTime = endTime;
		CurrentTime = currentTime;
		IntervalBetweenSpawn = intervalBetweenSpawn;
		NextSpawnTime = CurrentTime + IntervalBetweenSpawn;
	}

	public void Pause()
	{
		base.Pause();
	}
	
	public void Resume()
	{
		base.Resume();
	}
}
