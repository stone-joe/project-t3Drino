//Nabil raycaster and block destroyer
using UnityEngine;
using System.Collections;

public class ShootingRay : MonoBehaviour {
    private int neighborCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.right * 100); // this only draws a line. just for visuals


        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.right, 100);

        // hit.length also includes the 2 vertical walls 
        if (hits.Length > 8) {
            bool moving = false;

            for (int i = 0; i < hits.Length; i++) {
                RaycastHit hit = hits[i];
                // Get hit tet
                Transform TetHit = hit.transform;

                if (TetHit.gameObject.tag == "notMovableTag") {
                    if (TetHit.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.05F) {
                        moving = true;
                    }
                }
            }
            if (!moving) {
                for (int i = 0; i < hits.Length; i++) {
                    RaycastHit hit = hits[i];
                    // Get hit block
                    Transform TransformHitByRay = hit.collider.transform;

                    if (TransformHitByRay.parent != null && TransformHitByRay.parent.gameObject.tag == "notMovableTag") { // only handle blocks, not the side walls
                        TransformHitByRay.GetComponent<Renderer> ().material.color = Color.red;
                        
                        // Make deleting blocks freeze to prevent chain reaction line completions
                        if (TransformHitByRay.gameObject.GetComponent<Rigidbody>() == null) {
                            TransformHitByRay.gameObject.AddComponent<Rigidbody>();
                        }
                        TransformHitByRay.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        TransformHitByRay.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                       
                        Destroy(TransformHitByRay.gameObject, 1F);
                        TransformHitByRay.gameObject.tag = "blockDestroyingTag";

                        // iterate through all children blocks in tet/new parent...
                        foreach (Transform child in TransformHitByRay.parent) {
                            // ...except for the destroyed block or any destroying block
                            if (child.name != TransformHitByRay.name && child.gameObject.tag != "blockDestroyingTag") {
                                neighborCount = 0;
                                // count number of adjacent neighbors...
                                foreach (Transform potentialNeighbor in TransformHitByRay.parent) {
                                    // ...besides the destroyed block or any other blocks to be destroyed
                                    if (potentialNeighbor.name != child.name && potentialNeighbor.gameObject.tag != "blockDestroyingTag") {
                                        //get distance
                                        Vector3 difference = (child.localPosition - potentialNeighbor.localPosition);
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
                        TransformHitByRay.parent = null;
                    }
                }
            }
        }
    }
}