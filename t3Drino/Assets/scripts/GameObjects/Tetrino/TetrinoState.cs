using UnityEngine;
using System.Collections;

/**
 * @class TetrinoState
 * @description A class that describes the game state of a tetrino
 */
public class TetrinoState : MonoBehaviour {
	/**
	 * @member {int} ACTIVE
	 * @constant
	 * @description The tetrino is in the playable area and can be grabbed by the user
	 */
	public const int ACTIVE = 0;
	/**
	 * @member {int} INACTIVE
	 * @constant
	 * @description The tetrino is in the playable area but CANNOT be grabbed by the user. This state should only occur
	 * when the tetrino has collided with the bottom of the playable area, another inactive tetrino, or the time limit
	 * for holding a tetrino has run out(not the side walls)
	 */
	public const int INACTIVE = 1;
	/**
	 * @member {int} QUEUED
	 * @constant
	 * @description A queued tetrino is NOT rendered in the playable field, but is "in line" to be rendered. It should
	 * be displayed to the player so that (s)he knows what the next tetrino will be
	 */
	public const int QUEUED = 2;
	/**
	 * @member {int} GRABBED
	 * @constant
	 * @description The player has grabbed the tetrino and can move/rotate the tetrino at will (until either the timer runs out
	 * OR the tetrino has touched an inactive tetrino)
	 */
	public const int GRABBED = 3;

	/**
	 * @member {int} state
	 * @description The current state of the tetrino
	 */
	private int state = 2; // Default

	/**
	 * @method setState
	 * @param {int} newState - The state that the tetrino should be set to. Possible values are:
	 * 		TetrinoState.ACTIVE   (0)
	 * 		TetrinoState.INACTIVE (1)
	 * 		TetrinoState.QUEUED   (2)
	 * 		TetrinoState.GRABBED  (3)
	 */
	public void setState(int newState){
		if ( newState > -1 && newState < 4 ){
			state = newState;
		}
		else {
			Debug.Log(newState);
			Debug.Log("'newState' must be an integer that is 1 through 3");
		}
	}

	/**
	 * @method getState
	 * @returns {int} The current state of the tetrino
	 */
	public int getState(){
		return state;
	}
}