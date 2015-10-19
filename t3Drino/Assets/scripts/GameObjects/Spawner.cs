//Nabil spawner
//this is just a test
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private float _period; // Every 3 seconds
	public float nextActionTime;
	public int _tetrinoCount;
	private TetrinoSelector _tetrinoSelector;

    public int numberOfTetrinosToSpawn=20;
	public List<GameObject> list;

    // Use this for initialization
    void Start () {

		ResetValues();

		// Set spawn location
		transform.position = new Vector3(0f, 21.80f, 0f);

		// Grab reference of the tetromino queue
		GameObject go = GameObject.Find("TetrinoSelector");
		_tetrinoSelector = (TetrinoSelector) go.GetComponent(typeof(TetrinoSelector));
    }

    void FixedUpdate()
    {
        if (Time.time > nextActionTime)
        {
			nextActionTime = Time.time;
			nextActionTime += _period;

			_tetrinoCount++;
			if (_tetrinoCount < numberOfTetrinosToSpawn)
            {
				GameObject go = _tetrinoSelector.Pop();
				list.Add((GameObject)Instantiate(go, transform.position, transform.rotation));
            }
        }
    }

	public void ResetValues()
	{
		list = new List<GameObject>();
		_period = 3f;
		nextActionTime = 0.0f;;
		_tetrinoCount = 0;
	}
}
