//Nabil raycaster and block destroyer
using UnityEngine;
using System.Collections;

public class ShootingRay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.right * 100); // this only draws a line. just for visuals


        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.right, 100);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Transform TransformHitByRay = hit.collider.transform;

            //fyi:the hit.length also includes the 2 vertical walls 
                if (hits.Length > 8) {
                    if (TransformHitByRay.parent.gameObject.tag == "notMovableTag") { //the side walls should not be tagged, only the blocks are
                        TransformHitByRay.GetComponent<Renderer> ().material.color = Color.red;
                       
                        Destroy(TransformHitByRay.gameObject, 1F);

                        foreach (Transform child in TransformHitByRay.parent) {
                            GameObject newParent = new GameObject("newParent");
                            newParent.tag = "notMovableTag";
                            newParent.AddComponent<SelfCleanUp>();
                            //get siblings only
                            if (child.name != TransformHitByRay.name) {
                                //get distance
                                Vector3 difference = (child.localPosition - TransformHitByRay.localPosition);
                                if (difference.magnitude <= 2) {
                                    child.GetComponent<Renderer> ().material.color = Color.grey;
                                    // create new parent object for unparented block so it can be hit with shooting ray again
                                    child.parent = newParent.transform;
                                    child.gameObject.AddComponent<Rigidbody>();
                                    child.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ
                                                                                             | RigidbodyConstraints.FreezeRotationX
                                                                                             | RigidbodyConstraints.FreezeRotationY; 
                                }
                            }
                        }
                    }
                }
        }


    }
}