using UnityEngine;
using System.Collections;

/**
 * @class Tetromino
 * @description A class that describes the game state of a tetromino
 */
public class Tetromino : MonoBehaviour {
	/**
	 * @member {struct} WallCollision
	 * @description A simple struct that stores two pieces of information: the all that was hit and the distance from that hit.
	 * This is used by the rayHitWall method to check if a tetromino has moved beyond a wall.
	 */
	public struct WallCollision
	{
		public Tetromino.Wall wall;
		public float distance;
		public float angleZ;
	}
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
	 * @member {float} _cubeHeight
	 * @description The height of an individual cube. Stored when it's instantiated
	 */
	protected float _cubeHeight = 0.0f;
	/**
	 * @member {float} magnitude
	 * @description The length of the ray from the tetromino origin to a corner. This is the same for all corners in the
	 * "I" tetromino.
	 */
	protected float magnitude = 0.0f;
	/**
	 * @member {enum} states
	 * @constant
	 * @description An enumeration containing the different states for a Tetromino
	 */
	public enum states {ACTIVE, INACTIVE, QUEUED, GRABBED};
	/**
	 * @member {enum} Wall
	 * @constant 
	 * @description An enumeration containing different wall definitions
	 */
	public enum Wall {LEFT, NONE, RIGHT};
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
		if (newState == Tetromino.states.GRABBED) {
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ | 
				RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationY | 
				RigidbodyConstraints.FreezeRotationZ;
		} else {
			gameObject.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ | 
				RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationY;
		}
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
		_cubeHeight = gameObject.GetComponentInChildren<Renderer> ().bounds.size.y;
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
		// Check if this tetromino is moving
		float force = this.gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
		if ( force > Tetromino.MIN_MOVE_FORCE || force < -Tetromino.MIN_MOVE_FORCE ) {
			moving = true;
		} else {
			moving = false;
		}
	}
	/**
	 * @member {Method} moveToWall
	 * @param {Tetromino.Wall} wall - The wall to which the tetromino should be moved
	 * @param {float} y - The new y-position of the tetrominon
	 * @description Using the tetromino's current rotation, this method calculates the new position of the tetromino
	 * based on the corner that is nearest the wall, as well as the x-distance between the wall and the corner.
	 */
	public virtual void moveToWall(Tetromino.Wall wall, float y){}
	/**
	 * @member {Method} rayHitWall
	 * @param {Vector3} rayVector
	 * @returns {Tetromino.Wall} The wall that was hit
	 */
	protected WallCollision rayHitWall(Vector3 rayVector){
		RaycastHit hit;
		float magnitude = (rayVector.magnitude);
		WallCollision colliderData = new WallCollision ();

		Debug.DrawRay (transform.position, rayVector, Color.black);

		if (Physics.Raycast (transform.position, rayVector, out hit, magnitude, 1 << 8)) {
			if (hit.transform.name == "wallRight") {
				colliderData.wall = Wall.RIGHT;
			} else if (hit.transform.name == "wallLeft") {
				colliderData.wall = Wall.LEFT;
			}

			colliderData.distance = hit.distance;
			colliderData.angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		} else {
			colliderData.wall = Wall.NONE;
			colliderData.distance = 0.0f;
			colliderData.angleZ = 0;
		}

		return colliderData;
	}
	/**
	 * @member {Method} rotateVector
	 * @param {Vector3} vector - The vector to rotate
	 * @param {float} radians - The number of radians to rotate the vector
	 * @returns {Vector3} A new, rotated vector
	 */
	protected Vector3 rotateVector(Vector3 vector, float radians){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		float unitX = vector.x * Mathf.Cos (angleZ) - vector.y * Mathf.Sin (angleZ);
		float unitY = vector.x * Mathf.Sin (angleZ) + vector.y * Mathf.Cos (angleZ);
		
		return new Vector3 (unitX, unitY, 0);
	}
	/**
	 * @member {Method} createUnitVector
	 * @param {float} x
	 * @param {float} y
	 * @param {float} z
	 * @returns {Vector3} The unit vector for the given x, y, and z values
	 */
	protected Vector3 createUnitVector(float x, float y, float z){
		float magnitude = Vector3.SqrMagnitude (new Vector3 (x, y, z));
		return new Vector3(x / magnitude, y / magnitude, z / magnitude);
	}
	/**
	 * @member {Method} willCollideWithWall
	 * @param {Vector3} curScreenPoint
	 * @param {Vector3} prevScreenPoint
	 * @returns {Tetromino.Wall} Returns the wall that is hit, Wall.NONE if no collision occurs 
	 */
	public virtual WallCollision willCollideWithWall(Vector3 curScreenPoint, Vector3 prevScreenPoint){
		return new WallCollision();
	}
}