using UnityEngine;
using System.Collections;

/**
 * @class Tetromino_T
 * @extends Tetromino
 * 
 * 		(0)								(π/2)					(π)     				(3π/2)
 * 
 *										x						 -y						   -x
 *		 y								^						  ^							^
 *		 ^								|						  |							|
 *	     |								5---4				4---+---+---3				3---2
 *     1---0							|	|				|	|	|	|				|	|
 *	   |   |						0---+---+		  x <-- 5---+-o-+---2				+---+---1
 * 2---+-o-+---5 ----> x 	  y <---|   o	|					|	|					|	o	| --> y
 * |   |   |   |					1---+---+					0---1					+---+---0
 * 3---+---+---4						|   |					  						|	|
 * 										2---3					  						4---5
 * o = origin													  							
 */
public class Tetromino_T : Tetromino {
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
		
		if (angleZ >= 0 && angleZ < (Mathf.PI / 4)) {
			return new int[]{2, 5};	
		} else if (angleZ >= (Mathf.PI / 4) && angleZ < (Mathf.PI / 2)) {
			return new int[]{1, 4};
		} else if (angleZ >= (Mathf.PI / 2) && angleZ < (3 * Mathf.PI / 4)) {
			return new int[]{0, 3};
		} else if (angleZ >= (3 * Mathf.PI / 4) && angleZ < Mathf.PI) {
			return new int[]{5, 3};
		} else if (angleZ >= Mathf.PI && angleZ < (Mathf.PI * 5 / 4)) {
			return new int[]{4, 2};
		} else if (angleZ >= (5 * Mathf.PI / 4) && angleZ < (Mathf.PI * 3 / 2)) {
			return new int[]{4, 1};
		} else if (angleZ >= (3 * Mathf.PI / 2) && angleZ < (Mathf.PI * 7 / 4)) {
			return new int[]{3, 0};
		} else {
			return new int[]{3, 5};
		}
	}
	/**
	 * @override
	 */
	protected override float getAdjustedAngle(int corner){
		float angleZ = transform.eulerAngles.z * Mathf.Deg2Rad;
		
		if (corner == 0) {
			return angleZ + ((Mathf.PI / 2) - Mathf.Atan2(_cubeHeight, (0.5f * _cubeWidth)));
		} else if (corner == 1) {
			return angleZ + ((Mathf.PI / 2) + Mathf.Atan2(_cubeHeight, (0.5f * _cubeWidth)));
		} else if (corner == 2) {
			return angleZ + Mathf.PI;
		} else if ( corner == 3 ){
			return angleZ + (Mathf.PI + Mathf.Atan2(_cubeHeight, (1.75f * _cubeWidth)));
		} else if ( corner == 4 ){
			return angleZ - Mathf.Atan2(_cubeHeight, (1.75f * _cubeWidth));
		} else {
			return angleZ;
		}
	}
	/**
	 * @override
	 */
	public override float getHypotenuse(int corner){
		if (corner == 2 || corner == 5) {
			return new Vector3 (1.5f * _cubeWidth, 0, 0).magnitude;
		} else if (corner == 1 || corner == 0) {
			return new Vector3 (0.5f * _cubeWidth, _cubeHeight, 0).magnitude;
		} else {
			return new Vector3 (1.5f * _cubeWidth, _cubeHeight, 0).magnitude;
		}
	}
	/**
	 * @override
	 */
	protected override Vector3 getTestVector(int[] extremeCorners, float deltaX, float deltaY){
		Vector3 data = base.getTestVector (extremeCorners, deltaX, deltaY);
		// data.x -> corner number
		// data.y -> adjusted magnitude

		if (data.x == 0f) {
			return createUnitVector (0.5f * _cubeWidth, _cubeHeight, 0f) * data.y;
		} else if (data.x == 1f) {
			return createUnitVector (-0.5f * _cubeWidth, _cubeHeight, 0f) * data.y;
		} else if (data.x == 2f) {
			return createUnitVector (-1.5f * _cubeWidth, 0f, 0f) * data.y;
		} else if (data.x == 3f) {
			return createUnitVector (-1.5f * _cubeWidth, -_cubeHeight, 0f) * data.y;
		} else if (data.x == 4f) {
			return createUnitVector (1.75f * _cubeWidth, -_cubeHeight, 0f) * data.y;
		} else {
			return createUnitVector (1.5f * _cubeWidth, 0f, 0f) * data.y;
		}
	}
}
