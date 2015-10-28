using UnityEngine;
using System.Collections;

/**
 * @class Tetromino_I
 * @extends Tetromino
 * 		   y
 * 		   ^
 * 		   |
 * 3---+---+---+---2
 * |   |   o   |   | -> x
 * 0---+---+---+---1
 * 
 * o = origin
 * 
 * World Coordinates Formulae:
 * 
 * l = corner position with respect to tetromino origin
 * o = the origin position with respect to the real world
 * ß = angle of corner with respect to tetromino origin
 * Ω = angle of tetromino with respect to the world
 * 
 * x_0 = o - l * cos(Ω + (π + ß))
 * x_1 = o - l * cos(Ω - ß)
 * x_2 = o - l * cos(Ω + ß)
 * x_3 = o - l * cos(Ω + (π - ß))
 */
public class Tetromino_I : Tetromino {
	/**
	 * @member {Tetromino} tetromino
	 * @description A reference to the gameObject's tetromino
	 */
	private Tetromino tetromino;
	/**
	 * @member {float} _phi
	 * @description The angle between the tetromino origin and each corner
	 */
	private float _phi = 0.0f;
	/**
	 * @member {float} localX
	 * @description The x position of corner with respect to the tetromino origin
	 */
	private float _hypotenuse = 0.0f;
	/**
	 * @member {float} magnitude
	 * @description The length of the ray from the tetromino origin to a corner. This is the same for all corners in the
	 * "I" tetromino.
	 */
	private float _magnitude = 0.0f;
	/**
	 * @override
	 */
	public override void Start(){
		base.Start();
		tetromino = transform.gameObject.GetComponent<Tetromino> ();
		// Calculate the arctangent using the _cubeWidth
		_phi = Mathf.Atan ((_cubeHeight/2)/(_cubeWidth * 2));

		// Calculate the localX
		_hypotenuse = (2 * _cubeWidth / Mathf.Cos (_phi));

		// Magnitude of vector from tetromino origin to corners
		_magnitude = Vector3.SqrMagnitude (new Vector3(2f * _cubeWidth, 0.5f * _cubeHeight, 0.0f));
	}
	/**
	 * @member {Method} getExtremeCorners
	 * @returns {int[]} Returns an array of two ints: one for the corner closest to the left wall and the second for
	 * the corner closest to the right wall. The ints represent the corner number in the tetromino. See the diagram at the
	 * top of the file.
	 */
	private int[] getExtremeCorners(){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;

		if (angleZ >= 0 && angleZ < (Mathf.PI / 2)) {
			return new int[]{3, 1};	
		} else if (angleZ >= (Mathf.PI / 2) && angleZ < Mathf.PI) {
			return new int[]{2, 0};
		} else if (angleZ >= Mathf.PI && angleZ < (3 * Mathf.PI / 2)) {
			return new int[]{1, 3};
		} else {
			return new int[]{0, 2};
		}
	}
	/**
	 * @override
	 */
	public override void moveToWall(Tetromino.Wall wall, float y){
		// Current angle of tetromino
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		// The position of wall to which the tetromino corner is moving
		float positionOfWall = 0.0f;
		// The angle of the tetromino plus the constant angle of the corner with respect to the tetromino origin
		float adjustedAngle = 0.0f;

		// The new position of the tetromino origin
		int newX = 0;
		// The extreme corners of the tetromino
		int[] corners = getExtremeCorners ();
		int extreme = -1;

		if (wall == Wall.RIGHT) {
			GameObject rightWall = GameObject.Find ("wallRight");
			positionOfWall = rightWall.transform.position.x - rightWall.GetComponent<Renderer> ().bounds.size.x / 2;
			extreme = 1;
		} else {
			GameObject leftWall = GameObject.Find ("wallLeft");
			positionOfWall = leftWall.transform.position.x + leftWall.GetComponent<Renderer> ().bounds.size.x / 2;
			extreme = 0;
		}

		if (corners [extreme] == 0) {
			adjustedAngle = angleZ + (Mathf.PI + _phi);
		} else if (corners [extreme] == 1) {
			adjustedAngle = angleZ - _phi;
		} else if (corners [extreme] == 2) {
			adjustedAngle = angleZ + _phi;
		} else {
			adjustedAngle = angleZ + (Mathf.PI - _phi);
		}

		transform.position = new Vector3 (positionOfWall - (_hypotenuse * Mathf.Cos(adjustedAngle)),
		                                  y, transform.position.z);
	}
	/**
	 * @override
	 */
	public override WallCollision willCollideWithWall(Vector3 curScreenPoint, Vector3 prevScreenPoint){
		int[] extremeCorners = getExtremeCorners ();
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		float deltaX = curScreenPoint.x - prevScreenPoint.x;
		float deltaY = curScreenPoint.y - prevScreenPoint.y;

		float moveMagnitude = new Vector3 (deltaX, deltaY, 0).magnitude;
		float adjustedMagnitude = _magnitude + moveMagnitude;

		Vector3 testVector;

		if (extremeCorners[0] == 3 && extremeCorners[1] == 1) {
			if ( deltaX < 0 ){
				// Left most corner is 3
				testVector = createUnitVector (-2 * _cubeWidth, _cubeHeight / 2, 0) * adjustedMagnitude;
			}
			else {
				// Right most corner is 1
				testVector = createUnitVector (2 * _cubeWidth, -_cubeHeight / 2, 0) * adjustedMagnitude;
			}
		} else if (extremeCorners[0] == 2 && extremeCorners[1] == 0){
			if ( deltaX < 0 ){
				// Left most corner is 2
				testVector = createUnitVector (2 * _cubeWidth, _cubeHeight / 2, 0) * adjustedMagnitude;
			}
			else {
				// Right most corner is 0
				testVector = createUnitVector (-2 * _cubeWidth, -_cubeHeight / 2, 0) * adjustedMagnitude;
			}
		}  else if (extremeCorners[0] == 1 && extremeCorners[1] == 3){
			if ( deltaX < 0 ){
				// Left most corner is 1
				testVector = createUnitVector (2 * _cubeWidth, -_cubeHeight / 2, 0) * adjustedMagnitude;
			}
			else {
				// Right most corner is 3
				testVector = createUnitVector (-2 * _cubeWidth, _cubeHeight / 2, 0) * adjustedMagnitude;
			}
		}
		else {
			if ( deltaX < 0 ){
				// Right most corner is 0
				testVector = createUnitVector (-2 * _cubeWidth, -_cubeHeight / 2, 0) * adjustedMagnitude;
			}
			else {
				// Left most corner is 2
				testVector = createUnitVector (2 * _cubeWidth, _cubeHeight / 2, 0) * adjustedMagnitude;
			}
		}
		
		return rayHitWall(rotateVector(testVector, angleZ));
	}
}
