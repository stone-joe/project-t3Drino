using UnityEngine;
using System.Collections;

public class HandGrab : MonoBehaviour {

    private bool thumbCollide = false;
    private bool fingerCollide = false;
    private Transform HandGrabbedTransform;
    private bool grabbed = false;
    private Vector3 offset = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {
        // Disable collisions between hand and inactive blocks
        Physics.IgnoreLayerCollision(8, 9, true);
    }
    
    // Update is called once per frame
    void Update () {
        if (grabbed && HandGrabbedTransform != null) {
            // If grabStrength lower than threshold, release object
            if (HandGrabbedTransform.parent.transform.parent.transform.GetComponent<HandScript>().grabStrength < 0.2F) {
                grabbed = false;
                HandGrabbedTransform = null;
            } else {
                // If grabbed, follow hand position and rotation
                gameObject.transform.position = new Vector3(HandGrabbedTransform.position.x - offset.x, HandGrabbedTransform.position.y - offset.y, 0);
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, HandGrabbedTransform.localEulerAngles.z));
            }
        }
        if (grabbed && gameObject.transform.tag == "notMovableTag") {
            grabbed = false;
            HandGrabbedTransform = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!grabbed && collision.gameObject.transform.parent && gameObject.transform.tag == "movableTag") {
            // Check if parent of hit bone is a finger
            if (collision.gameObject.transform.parent.GetComponent<RigidFinger>()) {
                if (collision.gameObject.transform.parent.name == "thumb") {
                    thumbCollide = true;
                } else if (collision.gameObject.transform.parent.name != null) {
                    fingerCollide = true;
                } else {
                    thumbCollide = false;
                    fingerCollide = false;
                }
                //if (collision.gameObject.transform.parent.transform.parent.transform.GetComponent<LM_Gesture>().grabStrength > 0.7F) {
                // Count as grabbed if thumb and any finger(s) collided with shape
                if (thumbCollide && fingerCollide) {
                    print(string.Format("Grabbing {0}", gameObject.transform));
                    foreach (Transform block in gameObject.transform) {
                        block.GetComponent<Renderer> ().material.color = Color.yellow;
                    }
                    // Get hand and distance between hit bone and shape
                    HandGrabbedTransform = collision.gameObject.transform;
                    offset = HandGrabbedTransform.position - gameObject.transform.position;
                    grabbed = true;
                }
            }
        }
    }
}
