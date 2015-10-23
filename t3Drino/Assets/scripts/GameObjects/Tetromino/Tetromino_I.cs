using UnityEngine;
using System.Collections;

/**
 * @class Tetromino_I
 * @extends Tetromino
 * 3---+---+---+---2
 * |   |   o   |   |
 * 0---+---+---+---1
 * 
 * o = origin
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
	private float _localX = 0.0f;
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
	 * @override
	 */
	public override void Start(){
		base.Start();
		tetromino = transform.gameObject.GetComponent<Tetromino> ();
		// Calculate the arctangent using the _cubeWidth
		_phi = Mathf.Atan2 (0.5f * _cubeWidth, 2.0f * _cubeWidth);

		// Calculate the localX
		_localX = (2 * _cubeWidth / Mathf.Cos (_phi));
	}
	/**
	 * @member {method} calculateCorners
	 * @param {float} newX - The would-be x-position of the tetromino origin if we allow it to move
	 * @description Calculates the position of each corner in the tetromino
	 */
	public override float[] calculateCorners(float newX){
		float angleZ = transform.eulerAngles.z;
		float[] corners = {0.0f, 0.0f, 0.0f, 0.0f};

		corners [0] = newX - _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + (_phi + Mathf.PI));
		corners [1] = newX + _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + (2 * Mathf.PI - _phi ));
		corners [2] = newX + _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + _phi);
		corners [3] = newX - _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + (Mathf.PI - _phi));

		Debug.Log (Mathf.Cos ((angleZ * Mathf.Deg2Rad) + _phi));
		return corners;
	}
	/**
	 * @member {float} calculateOriginPosition
	 * @param {int} corner - The index of the corner
	 * @param {float} desiredXPosition - The x position of the 'desired' corner position. E.G. where on the x-axis 
	 * should the provided corner be?
	 * @description Calculates the position of the tetromino origin given a specific corner
	 */
	public override float calculateOriginPosition(int corner, float desiredXPosition){
		float angleZ = transform.eulerAngles.z;
		if (corner == 0) {
			return desiredXPosition - _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + (_phi + Mathf.PI));
		} else if (corner == 1) {
			return desiredXPosition + _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + (2 * Mathf.PI - _phi ));
		} else if (corner == 2) {
			return desiredXPosition + _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + _phi);
		} else if (corner == 3) {
			return desiredXPosition - _localX * Mathf.Cos ((angleZ * Mathf.Deg2Rad) + (Mathf.PI - _phi));
		} else {
			Debug.LogError ("Tetromino_I#calculateOriginPosition: provided corner is out of range. Must be 0 through 3. Provided: " + corner);
			return 0.0f;
		}
	}
	public override float[] calculateBounds(){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		float turn1 = (Mathf.PI / 2 - _phi);
		float turn2 = (Mathf.PI * 2 - _phi);
		float turn3 = (Mathf.PI * 3 / 2 - _phi);
		float[] bounds = {0.0f, 0.0f};
		
		if (angleZ >= 0 && angleZ < turn1 ) {
			// Left most corner is 3
			bounds[0] = -7.5f - _localX * Mathf.Cos (angleZ + (Mathf.PI - _phi));
			// Right most corner is 1
			bounds[1] = 6.5f - _localX * Mathf.Cos (angleZ + (2 * Mathf.PI - _phi));
		} else if (angleZ >= turn1 && angleZ < turn2) {
			// Left most corner is 2
			bounds[0] = -7.5f - _localX * Mathf.Cos (angleZ + _phi);
			// Right most corner is 0
			bounds[1] = 6.5f - _localX * Mathf.Cos (angleZ + (_phi + Mathf.PI));
		} else if (angleZ >= turn2 && angleZ < turn3) {
			// Left most corner is 1
			bounds[0] = -7.5f + _localX * Mathf.Cos (angleZ + (2 * Mathf.PI - _phi));
			// Right most corner is 3
			bounds[1] = 6.5f + _localX * Mathf.Cos (angleZ + (Mathf.PI - _phi));
		} else {
			// Left most corner is 0
			bounds[0] = -7.5f - _localX * Mathf.Cos (angleZ + (_phi + Mathf.PI));
			// Right most corner is 2
			bounds[1] = 6.5f - _localX * Mathf.Cos (angleZ + _phi);
		}
		return bounds;
	}
}
