//Nabil raycaster and block destroyer
using UnityEngine;
using System.Collections;

/**
 * @class ShoortingRay
 * @description This class creates a ray that travels from the left wall to the right wall at a certain height. If that ray
 * hits a predetermined number of cubes, then the row is considered full and all cubes are destroyed.
 */
public class ShootingRay : MonoBehaviour {
	/**
	 * @member {int} neighborCount
	 * @description Used to count the number of cubes that are connected to a cube. If neighborCount is 0, then the
	 * cube must be added to a new parent
	 */
    private int neighborCount = 0;
	/**
	 * @member {RaycastHit[]} hits
	 * @description An array of raycasthit objects containing references to the cubes that were hit
	 */
    private RaycastHit[] hits;
	/**
	 * @member {Transform} hitBlock
	 * @description The transform object of a cube that in the above 'hits' array
	 */
    private Transform hitBlock; 
	/**
	 * @member {Vector3} difference
	 */
    private Vector3 difference;
	/**
	 * @member {int} cubesPerRow
	 * @description The number of cubes that can fill a row
	 */
	private int cubesPerRow = 7;
	/**
	 * @member {bool} moving
	 * @description Used to define whether or not cubes within a row are moving
	 */
	private bool moving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.right * 100); // this only draws a line. just for visuals

        hits = Physics.RaycastAll(transform.position, transform.right, 100);

        // hit.length also includes the 2 vertical walls 
        if (hits.Length == cubesPerRow+2) {
            // Only clear line if all objects are stationary
            moving = false;
            foreach (RaycastHit hit in hits) {
				TetrominoState tetrominoState = hit.transform.GetComponent<TetrominoState>();
                if (tetrominoState != null && (tetrominoState.getState () == TetrominoState.states.INACTIVE && tetrominoState.isMoving())) {
                    moving = true;
                }
            }
            if (!moving) {
                foreach (RaycastHit hit in hits) {
                    // Get hit block
                    hitBlock = hit.collider.transform;

                    if ( hitBlock.parent != null && hitBlock.parent.GetComponent<TetrominoState>() != null ) { // only handle blocks, not the side walls
                        hitBlock.GetComponent<Renderer> ().material.color = Color.red;
                        
                        // Make deleting blocks freeze to prevent chain reaction line completions
                        if (hitBlock.gameObject.GetComponent<Rigidbody>() == null) {
                            hitBlock.gameObject.AddComponent<Rigidbody>();
                        }
                        hitBlock.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hitBlock.gameObject.GetComponent<Rigidbody>().isKinematic = true;


                        //geting the position of explosion, the starting the explosion.
                        //each explosion has a ExplosionSelfDestruct.cs which takes care of destroying the eplosion particle system
                        Vector3 pos = hitBlock.transform.position;         
                        GameObject explo = (Resources.Load("Explosions/Explosion1", typeof(GameObject))) as GameObject;
                        Instantiate(explo, pos, Quaternion.identity);
                       
                        Destroy(hitBlock.gameObject, 1F);

                        hitBlock.gameObject.tag = "blockDestroyingTag";

                        // iterate through all children blocks in tet/new parent...
                        foreach (Transform child in hitBlock.parent) {
                            // ...except for the destroyed block or any destroying block
                            if (child.name != hitBlock.name && child.gameObject.tag != "blockDestroyingTag") {
                                neighborCount = 0;
                                // count number of adjacent neighbors...
                                foreach (Transform potentialNeighbor in hitBlock.parent) {
                                    // ...besides the destroyed block or any other blocks to be destroyed
                                    if (potentialNeighbor.name != child.name && potentialNeighbor.gameObject.tag != "blockDestroyingTag") {
                                        //get distance
                                        difference = (child.localPosition - potentialNeighbor.localPosition);
                                        if (difference.magnitude <= 2) {
                                            //potentialNeighbor.GetComponent<Renderer> ().material.color = Color.yellow;
                                            neighborCount++;
                                        }
                                    }
                                }
                                // Re-parent block if no neighbors found
                                if (neighborCount == 0) {
                                    // create new parent object for unparented block so it can be hit with shooting ray again
                                    GameObject newParent = new GameObject("newParent");
                                    newParent.AddComponent<SelfCleanUp>();
									newParent.AddComponent<TetrominoState>();
                                    child.parent = newParent.transform; 
                                }
                            }
                        }
                        hitBlock.parent = null;
                    }
                }
            }
        }
    }
}