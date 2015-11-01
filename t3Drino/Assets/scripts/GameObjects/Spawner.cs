//Nabil spawner
//this is just a test
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    private float _period = 15.0f; // Every 3 seconds
	public float nextActionTime = 0.0f;
    public int _tetrinoCount = 0;
	private TetrinoSelector _tetrinoSelector;

    public int numberOfTetrinosToSpawn=1;

    // Use this for initialization
    void Start () {

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
			nextActionTime += _period;

			_tetrinoCount++;
			if (_tetrinoCount < numberOfTetrinosToSpawn)
            {
				GameObject go = _tetrinoSelector.Pop();
                Instantiate(go, transform.position, transform.rotation);
            }
        }
    }
}
