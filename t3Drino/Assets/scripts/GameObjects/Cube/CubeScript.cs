using UnityEngine;
using System.Collections;

/**
 * @class CubeScript
 * @description The main script for a Cube. It will include state characteristics and other Cube related methods/properties
 */
public class CubeScript : MonoBehaviour {
	/**
	 * @member {string} type
	 * @constant
	 */
	public const string type = "cube";

	/**
	 * @member {method} OnTriggerEnter
	 * @param {Collider} other
	 * @description Fires when another object collides with this object
	 */
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
