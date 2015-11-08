using UnityEngine;
using System.Collections;

/**
 * @class Tetromino_I		
 * @extends Tetromino		
 * 		
 * 		 y
 * 		 ^
 * 		 |
 *     5---+---0
 *     |   |   |
 * 4---+---+---1
 * |   | o | ---> x
 * 3---+---2
 * 
 * o = origin
 */
public class Tetromino_Z : Tetromino {

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
		cubeIndex = 1;
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
		
		if (angleZ >= 0 && angleZ < (Mathf.PI / 6)) {
			return new int[]{4, 1};
		} else if (angleZ >= (Mathf.PI / 6) && angleZ < (Mathf.PI / 2)) {
			return new int[]{5, 2};
		} else if (angleZ >= (Mathf.PI / 2) && angleZ < (Mathf.PI)) {
			return new int[]{0, 3};
		} else if (angleZ >= Mathf.PI && angleZ < (7 * Mathf.PI / 6)) {
			return new int[]{1, 4};
		} else if (angleZ >= (7 * Mathf.PI / 6) && angleZ < (Mathf.PI * 3 / 2)) {
			return new int[]{2, 5};
		} else {
			return new int[]{3, 0};
		}
	}
	/**
	 * @override
	 */
	protected override float getAdjustedAngle(int corner){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		
		if (corner == 0) {
			return angleZ + (Mathf.PI / 4);
		} else if (corner == 1) {
			return angleZ + (Mathf.Atan2(0.5f, 1.5f));
		} else if (corner == 2) {
			return angleZ - (Mathf.PI / 4);
		} else if (corner == 3) {
			return angleZ + (Mathf.PI + Mathf.Atan2(0.5f, 1.5f));
		} else if (corner == 4) {
			return angleZ + (Mathf.PI * Mathf.Atan2(0.5f, 1.5f));
		} else {
			return angleZ + (Mathf.PI / 2) + Mathf.Atan2(0.5f, 1.5f);
		}
	}
	/**
	 * @member {Method} getUnitVector
	 * @param {int} corner
	 * @returns {Vector3} A vector pointing from the origin to the provided corner
	 */
	public Vector3 getVector(int corner){
		if (corner == 0) {
			return new Vector3 (1.5f * _cubeWidth, 1.5f * _cubeHeight, 0);
		} else if (corner == 1) {
			return new Vector3 (1.5f * _cubeWidth, 0.5f * _cubeHeight, 0);
		} else if (corner == 2) {
			return new Vector3 (0.5f * _cubeWidth, -0.5f * _cubeHeight, 0);
		} else if (corner == 3) {
			return new Vector3 (-1.5f * _cubeWidth, -0.5f * _cubeHeight, 0);
		} else if (corner == 4) {
			return new Vector3 (-1.5f * _cubeWidth, 0.5f * _cubeHeight, 0);
		} else {
			return new Vector3 (-0.5f * _cubeWidth, 1.5f * _cubeHeight, 0);
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

