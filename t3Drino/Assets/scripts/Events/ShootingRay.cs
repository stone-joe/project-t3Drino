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
            GameObject ObjectHitByRay = hit.transform.gameObject;

            //fyi:the hit.length also includes the 2 vertical walls 
                if (hits.Length > 8) {
                    if (ObjectHitByRay.tag == "blockTag")  //the side walls should not be tagged, only the blocks are
                    Destroy(ObjectHitByRay);
                }
        }

    }
}
