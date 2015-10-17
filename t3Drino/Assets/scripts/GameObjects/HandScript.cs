using UnityEngine;
using System.Collections;
using Leap;

public class HandScript : MonoBehaviour {

    private Hand hand;
    private bool grabbing = false;
    private GameObject grabbedObject;
    private Vector3 handCenterPos = new Vector3(0, 0, 0);
    private Vector3 offsetPos;
    private Vector3 pivotPoint;
    private Vector3 dropVelocity;
    private float palmRotationZ;
    private float offsetRot;

    public float grabStrength;
    public static float grabRadius = 2.0F;
    public static float maxDropVelocity = 20F;

    void Start () {
        // Disable collisions between hand and inactive blocks
        Physics.IgnoreLayerCollision(8, 9, true);

        //Assign hand rigidbodies to layer 8 to disable collision with blocks
        Transform [] childTransforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform transform in childTransforms) {
            transform.gameObject.layer = 8;
        }
    }

    void OnDrawGizmos() {
        // Visualize OverlapSphere
        Gizmos.DrawWireSphere(handCenterPos, grabRadius);
    }

    void Update() {
        hand = GetComponent<HandModel>().GetLeapHand();
        grabStrength = hand.GrabStrength;
        handCenterPos = (new Vector(hand.PalmPosition.x, hand.PalmPosition.y-100, hand.PalmPosition.z+80)+hand.PalmNormal*50).ToUnityScaled()*40;
        palmRotationZ = hand.PalmNormal.Roll * Mathf.Rad2Deg;

        if (grabStrength > 0.3F) {
            if (!grabbing) {
                grabbedObject = null;
                // Look for closest object within sphere to grab
                Collider[] close_things = Physics.OverlapSphere(handCenterPos, grabRadius);
                Vector3 dist = new Vector3(grabRadius, 0.0F, 0.0F);
                foreach (Collider close_thing in close_things) {
                    Vector3 new_dist = handCenterPos - close_thing.transform.position;
                    if (close_thing.gameObject.transform.parent != null
                        && close_thing.gameObject.transform.parent.transform.tag== "movableTag"
                        && new_dist.magnitude < dist.magnitude
                    ) {
                        // Get the block's parent shape
                        grabbedObject = close_thing.gameObject.transform.parent.gameObject;
                        dist = new_dist;
                    }
                }
                if (grabbedObject != null) {
                    // Found object to grab
                    print(string.Format("GRABBED {0}", grabbedObject));
                    grabbing = true;
                    // Record position and rotation relative to hand
                    offsetPos = handCenterPos - grabbedObject.transform.position;
                    offsetRot = palmRotationZ - grabbedObject.transform.localEulerAngles.z;
                    foreach (Transform block in grabbedObject.transform) {
                        block.GetComponent<Renderer> ().material.color = Color.yellow;
                    }
                }
            }
        } else {
            if (grabbing) {
                print(string.Format("DROPPED {0}", grabbedObject));
                dropVelocity = grabbedObject.GetComponent<Rigidbody>().velocity;
                LimitVelocity(dropVelocity);
                grabbedObject.GetComponent<Rigidbody>().velocity = new Vector3(dropVelocity.x, dropVelocity.y, dropVelocity.z);
                grabbedObject = null;
            }
            grabbing = false;
        }

        // Update position and rotation if grabbing shape
        if (grabbing && grabbedObject != null) {
            pivotPoint = handCenterPos - offsetPos;
            grabbedObject.transform.position = new Vector3(pivotPoint.x, pivotPoint.y, 0);
            grabbedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, palmRotationZ - offsetRot));

            // Force drop if deactivated for some reason
            if (grabbedObject.transform.tag == "notMovableTag") {
                dropVelocity = grabbedObject.GetComponent<Rigidbody>().velocity;
                LimitVelocity(dropVelocity);
                grabbedObject.GetComponent<Rigidbody>().velocity = new Vector3(dropVelocity.x, dropVelocity.y, dropVelocity.z);
                grabbedObject = null;
                grabbing = false;
            }
        }
    }

    Vector3 LimitVelocity(Vector3 velocity) {
        if (velocity.magnitude >= maxDropVelocity) {
            velocity.Normalize();
            velocity *= maxDropVelocity;
        }
        return velocity;
    }
}