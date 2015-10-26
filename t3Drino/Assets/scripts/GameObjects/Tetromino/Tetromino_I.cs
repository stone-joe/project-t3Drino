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
 * x_0 = o + l * cos(Ω + ßπ)
 * x_1 = o + l * cos(Ω - ß)
 * x_2 = o + l * cos(Ω + ß)
 * x_3 = o + l * cos(Ω + (π - ß))
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
		_phi = Mathf.Atan (0.5f/2.0f);

		// Calculate the localX
		_hypotenuse = (2 * _cubeWidth / Mathf.Cos (_phi));

		// Magnitude of vector from tetromino origin to corners
		_magnitude = Vector3.SqrMagnitude (new Vector3(2f * _cubeWidth, 0.5f * _cubeHeight, 0.0f));
	}
	/**
	 * @override
	 */
	public override WallCollision willCollideWithWall(Vector3 curScreenPoint, Vector3 prevScreenPoint){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		float deltaX = curScreenPoint.x - prevScreenPoint.x;
		float deltaY = curScreenPoint.y - prevScreenPoint.y;

		float moveMagnitude = new Vector3 (deltaX, deltaY, 0).magnitude;
		float adjustedMagnitude = _magnitude + moveMagnitude;

		Vector3 testVector;

		if (angleZ >= 0 && angleZ < (Mathf.PI / 2)) {
			if ( deltaX < 0 ){
				// Left most corner is 3
				testVector = createUnitVector (-2 * _cubeWidth, _cubeHeight / 2, 0) * adjustedMagnitude;
			}
			else {
				// Right most corner is 1
				testVector = createUnitVector (2 * _cubeWidth, -_cubeHeight / 2, 0) * adjustedMagnitude;
			}
		} else if (angleZ >= (Mathf.PI / 2) && angleZ < Mathf.PI){
			if ( deltaX < 0 ){
				// Left most corner is 2
				testVector = createUnitVector (2 * _cubeWidth, _cubeHeight / 2, 0) * adjustedMagnitude;
			}
			else {
				// Right most corner is 0
				testVector = createUnitVector (-2 * _cubeWidth, -_cubeHeight / 2, 0) * adjustedMagnitude;
			}
		}  else if (angleZ >= Mathf.PI && angleZ < (3 * Mathf.PI / 2)){
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
