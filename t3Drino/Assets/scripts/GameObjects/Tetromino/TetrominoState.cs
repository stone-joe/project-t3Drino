using UnityEngine;
using System.Collections;

/**
 * @class TetrominoState
 * @description A class that describes the game state of a tetromino
 */
public class TetrominoState : MonoBehaviour {
	/**
	 * @member {enum} states
	 * @constant
	 * @description An enumeration containing the different states for a Tetromino
	 */
	public enum states {ACTIVE, INACTIVE, QUEUED, GRABBED};
	/**
	 * @member {int} MIN_MOVE_FORCE
	 * @description The minimum force acting on the tetromino for it to be considered "moving"
	 */
	public const float MIN_MOVE_FORCE = 0.05F;
	/**
	 * @member {TetrominState.states} state
	 * @description The current state of the tetromino
	 */
	private TetrominoState.states state = TetrominoState.states.QUEUED; // Default
	/**
	 * @member {bool} moving
	 * @description Whether or not the tetromino is moving. This is calculated in the Update method once per frame
	 */
	private bool moving = false;	
	/**
	 * @method setState
	 * @param {TetrominoState.states} newState - The state that the tetromino should be set to. Possible values are:
	 * 		TetrominoState.ACTIVE   
	 * 		TetrominoState.INACTIVE 
	 * 		TetrominoState.QUEUED   
	 * 		TetrominoState.GRABBED  
	 */
	public void setState(TetrominoState.states newState){
		state = newState;
	}
	/**
	 * @member {method} getState
	 * @returns {int} The current state of the tetromino
	 */
	public TetrominoState.states getState(){
		return state;
	}
	/**
	 * @member {method} isMoving
	 * @returns {bool} TRUE if the tetromino is moving
	 */
	public bool isMoving(){
		return moving;
	}
	/**
	 * @member {method} Setup
	 */
	void Start(){
		if (this.gameObject.GetComponent<Rigidbody> () == null) {
			this.gameObject.AddComponent<Rigidbody>();
			Rigidbody body = this.gameObject.GetComponent<Rigidbody>();
			body.mass = 1;
			body.angularDrag = 0.05f;
			body.drag = 0;
			body.useGravity = true;
			body.isKinematic = false;
			body.interpolation = RigidbodyInterpolation.None;
			body.collisionDetectionMode = CollisionDetectionMode.Discrete;
			body.constraints = RigidbodyConstraints.FreezePositionZ
					| RigidbodyConstraints.FreezeRotationX
					| RigidbodyConstraints.FreezeRotationY;
		}
	}
	/**
	 * @member {method} Update
	 * @description Internal method that's called once per frame. 
	 */
	void Update(){
		float force = this.gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		if ( force > TetrominoState.MIN_MOVE_FORCE || force < -TetrominoState.MIN_MOVE_FORCE ) {
			moving = true;
		} else {
			moving = false;
		}
	}
}