using UnityEngine;
using System.Collections;

// This class takes in a queue of tetrominos, and enables previewing up to a designated number of tetrominos in the queue

public class TetrinoPreviewer : MonoBehaviour {

	private TetrinoSelector _tetrinoSelector;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("TetrinoSelector");
		_tetrinoSelector = (TetrinoSelector) go.GetComponent(typeof(TetrinoSelector));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
