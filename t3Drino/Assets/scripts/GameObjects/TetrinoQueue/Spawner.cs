//Nabil spawner
//this is just a test
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private float _period;
	private TetrinoSelector _tetrinoSelector;
	private TetrinoSpawnTimer _tetrinoSpawnTimer;

    // Use this for initialization
    void Start () {

		_period = 3f;

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
    }

    void FixedUpdate()
    {
		if (_tetrinoSpawnTimer.ShouldPop)
		{
			GameObject go = _tetrinoSelector.Pop();
			Instantiate(go, transform.position, transform.rotation);
			_tetrinoSpawnTimer.ShouldPop = false;
		}
    }
}
