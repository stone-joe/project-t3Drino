using UnityEngine;
using System.Collections;

/**
 * @class Tetromino
 * @description A class that describes the game state of a tetromino
 */
public class Tetromino : MonoBehaviour {
	/**
	 * @member {bool} moving
	 * @description Whether or not the tetromino is moving. This is calculated in the Update method once per frame
	 */
	protected bool moving = false;	
	/**
	 * @member {float} _cubeWidth
	 * @description The width of an individual cube. Stored when it's instantiated
	 */
	protected float _cubeWidth = 0.0f;
	/**
	 * @member {float} minMouseX
	 * @description The minimum mouse position that the piece can move given the tetromino angle
	 */
	protected float minMouseX = 0.0f;
	/**
	 * @member {float} maxMouseX
	 * @description The maximum mouse position that the piece can move given the tetromino angle
	 */
	protected float maxMouseX = 0.0f;
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
	public Tetromino.states state = Tetromino.states.QUEUED; // Default
	/**
	 * @member {method} isMoving
	 * @returns {bool} TRUE if the tetromino is moving
	 */
	public bool isMoving(){
		return moving;
	}
	/**
	 * @method setState
	 * @param {Tetromino.states} newState - The state that the tetromino should be set to. Possible values are:
	 * 		Tetromino.ACTIVE   
	 * 		Tetromino.INACTIVE 
	 * 		Tetromino.QUEUED   
	 * 		Tetromino.GRABBED  
	 */
	public void setState(Tetromino.states newState){
		state = newState;
	}
	/**
	 * @member {method} getState
	 * @returns {int} The current state of the tetromino
	 */
	public Tetromino.states getState(){
		return state;
	}
	public virtual void Start(){
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
		_cubeWidth = gameObject.GetComponentInChildren<Renderer> ().bounds.size.x;
	}
	/**
	 * @member {Method} getCubeWidth
	 * @returns {float} 
	 */
	public float getCubeWidth(){
		return _cubeWidth;
	}
	/**
	 * @member {method} calculateCorners
	 * @param {float} newX - The would-be x-position of the tetromino origin once it's moved
	 * @description Calculates the position of each corner in the tetromino.
	 */
	public virtual float[] calculateCorners(float newX){
		float[] corners = {0.0f, 0.0f};
		return corners;
	}
	/**
	 * @member {float} calculateOriginPosition
	 * @param {int} corner - The index of the corner
	 * @param {float} desiredXPosition - The x position of the 'desired' corner position. E.G. where on the x-axis 
	 * should the provided corner be?
	 * @description Calculates the position of the tetromino origin given a specific corner
	 */
	public virtual float calculateOriginPosition(int corner, float desiredXPosition){
		return 0.0f;
	}
	/**
	 * @member {method} Update
	 * @description Internal method that's called once per frame. 
	 */
	public virtual void Update(){
		float force = this.gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		if ( force > Tetromino.MIN_MOVE_FORCE || force < -Tetromino.MIN_MOVE_FORCE ) {
			moving = true;
		} else {
			moving = false;
		}
	}
	public virtual float[] calculateBounds(){
		float[] bounds = {0.0f, 0.0f};
		return bounds;
	}
}