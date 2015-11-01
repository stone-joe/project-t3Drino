using UnityEngine;
using System.Collections;

/**
 * @class CubeScript
 * @description The main script for a Cube. It will include state characteristics and other Cube related methods/properties
 */
public class Cube : MonoBehaviour {
	/**
	 * @member {enum} states
	 * @description The different states that the cube can have
	 */
	public enum states {DO_NOT_DESTROY, DESTROYING}
	/**
	 * @member {Cube.states} state
	 * @description The current state of the cube
	 */
	private Cube.states state = Cube.states.DO_NOT_DESTROY;
	/**
	 * @member {method} OnTriggerEnter
	 * @param {Collider} other
	 * @description Fires when another object collides with this object
	 */
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
	/**
	 * @member {method} setState
	 * @param {Cube.states} newState - The new state of the cube
	 */
	public void setState(Cube.states newState){
		state = newState;
	}
	/**
	 * @member {method} getState
	 * @returns {Cube.states} The current state of the cube
	 */
	public Cube.states getState(){
		return state;
	}
}
