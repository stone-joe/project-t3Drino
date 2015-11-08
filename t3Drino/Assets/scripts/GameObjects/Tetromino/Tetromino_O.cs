using UnityEngine;
using System.Collections;

/**
 * @class Tetromino_I		
 * @extends Tetromino		
 * 		
 * 	 y
 * 	 ^
 *   |
 * 0---+---3
 * |   |   |
 * +---+---+
 * | o |   | ---> x
 * 1---+---2
 * 
 * o = origin
 */
public class Tetromino_O : Tetromino {

	/**
	 * @member {Tetromino} tetromino
	 * @description A reference to the gameObject's tetromino
	 */
	private Tetromino tetromino;
	/**
	 * @override
	 */
	public override void Start(){
		base.Start();
		cubeIndex = 0;
		tetromino = transform.gameObject.GetComponent<Tetromino> ();
	}
	/**
	 * @member {Method} getExtremeCorners
	 * @returns {int[]} Returns an array of two ints: one for the corner closest to the left wall and the second for
	 * the corner closest to the right wall. The ints represent the corner number in the tetromino. See the diagram at the
	 * top of the file.
	 */
	protected override int[] getExtremeCorners(){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		
		if (angleZ >= 0 && angleZ < Mathf.PI / 2) {
			return new int[]{0, 2};
		} else if (angleZ >= Mathf.PI / 2 && angleZ < Mathf.PI) {
			return new int[]{3, 1};
		} else if (angleZ >= Mathf.PI && angleZ < 3 * Mathf.PI / 2) {
			return new int[]{2, 0};
		} else {
			return new int[]{1, 3};
		} 
	}
	protected override float getPositionFromWall(int corner){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		float adjustedAngle = 0.0f;
		
		if (corner == 0) {
			adjustedAngle = angleZ + ((Mathf.PI / 2) + Mathf.Atan2((1.5f * _cubeHeight), (0.5f * _cubeWidth)));
		} else if (corner == 1) {
			adjustedAngle = angleZ + (5 * Mathf.PI / 4);
		} else if (corner == 2) {
			adjustedAngle = angleZ - (Mathf.Atan2((0.5f * _cubeHeight), (1.5f * _cubeWidth)));
		} else {
			adjustedAngle = angleZ + (Mathf.PI / 4);
		}

		return (getHypotenuse(corner) * Mathf.Cos(adjustedAngle)) + ((_cubeWidth / 2) * Mathf.Cos (Mathf.PI / 4));
	}
	/**
	 * @member {Method} getUnitVector
	 * @param {int} corner
	 * @returns {Vector3} A vector pointing from the origin to the provided corner
	 */
	public Vector3 getVector(int corner){
		if (corner == 0) {
			return new Vector3 (-0.5f * _cubeWidth, 1.5f * _cubeHeight, 0);
		} else if (corner == 1) {
			return new Vector3 (-_cubeWidth / 2, -_cubeHeight / 2, 0);
		} else if (corner == 2) {
			return new Vector3 (1.5f * _cubeWidth, -0.5f * _cubeHeight, 0);
		} else {
			return new Vector3 (1.5f * _cubeWidth, 1.5f * _cubeHeight, 0);
		} 
	}
	/**
	 * @override
	 */
	public override float getHypotenuse(int corner){
		return getVector (corner).sqrMagnitude;
	}
	/**
	 * @override
	 */
	protected override Vector3 getTestVector(int[] extremeCorners, float deltaX, float deltaY){
		Vector3 data = base.getTestVector (extremeCorners, deltaX, deltaY);
		// data.x -> corner number
		// data.y -> adjusted magnitude

		Vector3 vec = getVector ((int)data.x);
		return  createUnitVector(vec.x, vec.y, vec.z) * data.y;
	}
}

