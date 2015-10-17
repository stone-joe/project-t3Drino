//Nabil raycaster and block destroyer
using UnityEngine;
using System.Collections;

public class ShootingRay : MonoBehaviour {

    private int neighborCount = 0;
    private bool moving = false;
    private RaycastHit[] hits;
    private int layerMask = 1 << 9; // Only Raycast 'blocks' layer (9)
    private Transform hitBlock; 
    private Vector3 difference;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.right * 100); // this only draws a line. just for visuals

        hits = Physics.RaycastAll(transform.position, transform.right, 100, layerMask);

        if (hits.Length >= 7) {
            // Only clear line if all objects are stationary
            moving = false;
            foreach (RaycastHit hit in hits) {
                if (hit.transform.gameObject.tag == "notMovableTag" && hit.transform.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.05F) {
                    moving = true;
                }
            }
            if (!moving) {
                foreach (RaycastHit hit in hits) {
                    // Get hit block
                    hitBlock = hit.collider.transform;

                    if (hitBlock.parent != null && hitBlock.parent.gameObject.tag == "notMovableTag") { // only handle blocks, not the side walls
                        hitBlock.GetComponent<Renderer> ().material.color = Color.red;
                        
                        // Make deleting blocks freeze to prevent chain reaction line completions
                        if (hitBlock.gameObject.GetComponent<Rigidbody>() == null) {
                            hitBlock.gameObject.AddComponent<Rigidbody>();
                        }
                        hitBlock.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hitBlock.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                       
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
                                        //child.GetComponent<Renderer> ().material.color = Color.grey;
                                        // create new parent object for unparented block so it can be hit with shooting ray again
                                        GameObject newParent = new GameObject("newParent");
                                        newParent.layer = 9;
                                        newParent.AddComponent<SelfCleanUp>();
                                        newParent.tag = "notMovableTag";
                                        child.parent = newParent.transform;
                                        child.gameObject.AddComponent<Rigidbody>();
                                        child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ
                                                                                                 | RigidbodyConstraints.FreezeRotationX
                                                                                                 | RigidbodyConstraints.FreezeRotationY; 
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