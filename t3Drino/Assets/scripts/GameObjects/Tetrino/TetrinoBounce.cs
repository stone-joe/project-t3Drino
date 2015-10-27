using UnityEngine;
using System.Collections;

public class TetrinoBounce : MonoBehaviour {
	
	private bool _bounceOn;
	
	// Use this for initialization
	void Start () {
		//_bounceOn = true;
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;						// Stop bounce effect
		rigidBody.drag = 1;									// Drag can be used to slow down an object. The higher the drag the more the object slows down.
		rigidBody.mass = 10;
		rigidBody.useGravity = true;
		//_bounceOn = false;									// Stop bounce forever
	}
	
	// Update is called once per frame
	void Update () {
		
		// If no longer moving/in inactive state, stop tetromino from bouncing forever
		// NOTE: Must apply Tetromino State detection for this later once fully tested and implemented
		// The if-statement should wenter when 
		/*if (this.transform.tag == "notMovableTag" && _bounceOn)
		{
			Rigidbody rigidBody = GetComponent<Rigidbody>();
			rigidBody.isKinematic = false;						// Stop bounce effect
			rigidBody.drag = 1;									// Drag can be used to slow down an object. The higher the drag the more the object slows down.
			rigidBody.mass = 5;
			rigidBody.useGravity = true;
			_bounceOn = false;									// Stop bounce forever
		}		*/
	}
}
