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
    private float palmRotationX;
    private float palmRotationY;
    private float palmRotationZ;
    private float offsetRotX;
    private float offsetRotY;
    private float offsetRotZ;
    private float handCenterPosOffsetX;
    private float handCenterPosOffsetY;
    private float handCenterPosOffsetZ;
    private bool headMounted = false;
    private bool constrainTet = true;

    // configurable
    private float grabRadius = 2.0F;
    private float maxDropVelocity = 20F;

    public float grabStrength;
    public GameObject handObject;

    void Start () {
        // Disable collisions between hand and inactive blocks
        Physics.IgnoreLayerCollision(8, 9, true);

        // If camera is root parent, assume head-mounted
        if (gameObject.transform.root.tag == "MainCamera") {
            headMounted = true;
        }

        // If scene is Leap Menu, don't constrain tetrominos
        if (Application.loadedLevelName == "LeapMenu") {
            constrainTet = false;
        }

        // Different offsets if facing up or facing out
        if (headMounted) {
            handCenterPosOffsetX = 0F;
            handCenterPosOffsetY = -230F; // actually Z
            handCenterPosOffsetZ = -310F; // actually Y
        } else {
            handCenterPosOffsetX = 0F;
            handCenterPosOffsetY = -130F;
            handCenterPosOffsetZ = 120F;
        }

        //Assign hand rigidbodies to layer 8 to disable collision with blocks
        Transform [] childTransforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform transform in childTransforms) {
            transform.gameObject.layer = 8;
        }

        handObject = GameObject.Find("HandControllerCycler");
        if (handObject)
        {

        }
    }

    void OnDrawGizmos() {
        // Visualize OverlapSphere
        Gizmos.DrawWireSphere(handCenterPos, grabRadius);
    }

    void Update() {
        hand = GetComponent<HandModel>().GetLeapHand();
        grabStrength = hand.GrabStrength;
        if (headMounted) {
            handCenterPos = (new Vector(-(hand.PalmPosition.x + handCenterPosOffsetX + hand.PalmNormal.x * 50),
                                        -(hand.PalmPosition.z + handCenterPosOffsetY + hand.PalmNormal.z * 50),
                                        -(hand.PalmPosition.y + handCenterPosOffsetZ + hand.PalmNormal.y * 50)
                                        )
                                        ).ToUnityScaled() * 40;
        } else {
            handCenterPos = (new Vector((hand.PalmPosition.x + handCenterPosOffsetX + hand.PalmNormal.x * 50),
                                        1.5F*(hand.PalmPosition.y + handCenterPosOffsetY + hand.PalmNormal.y * 50 / 1.5F),
                                        (hand.PalmPosition.z + handCenterPosOffsetZ + hand.PalmNormal.z * 50)
                                        )
                                        ).ToUnityScaled() * 40;
        }
        palmRotationX = -hand.Direction.Pitch * Mathf.Rad2Deg;
        palmRotationY = hand.Direction.Yaw * Mathf.Rad2Deg;
        palmRotationZ = hand.PalmNormal.Roll * Mathf.Rad2Deg;

        if (grabStrength > 0.3F) {
            if (!grabbing) {
                grabbedObject = null;
                // Look for closest object within sphere to grab
                Collider[] close_things = Physics.OverlapSphere(handCenterPos, grabRadius);
                Vector3 dist = new Vector3(grabRadius, 0.0F, 0.0F);
                foreach (Collider close_thing in close_things) {
                    if (close_thing.transform.parent != null) {
                        TetrominoState tetrominoState = close_thing.transform.parent.gameObject.GetComponent<TetrominoState> ();
                        Vector3 new_dist = handCenterPos - close_thing.transform.position;
                        
                        if (close_thing.gameObject.transform.parent != null
                            && tetrominoState != null
                            && tetrominoState.getState() == TetrominoState.states.ACTIVE
                            && new_dist.magnitude < dist.magnitude
                        ) {
                            // Get the block's parent shape
                            grabbedObject = close_thing.gameObject.transform.parent.gameObject;
                            dist = new_dist;
                        }
                    }
                }
                if (grabbedObject != null) {
                    // Found object to grab
                    print(string.Format("GRABBED {0}", grabbedObject));
                    grabbing = true;
                    // Record position and rotation relative to hand
                    offsetPos = handCenterPos - grabbedObject.transform.position;
                    offsetRotX = palmRotationX - grabbedObject.transform.localEulerAngles.x;
                    offsetRotY = palmRotationY - grabbedObject.transform.localEulerAngles.y;
                    offsetRotZ = palmRotationZ - grabbedObject.transform.localEulerAngles.z;
                    /*foreach (Transform block in grabbedObject.transform) {
                        block.GetComponent<Renderer> ().material.color = Color.yellow;
                    }*/
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
            if (constrainTet) {
                grabbedObject.transform.position = new Vector3(pivotPoint.x, pivotPoint.y, 0);
                grabbedObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, palmRotationZ - offsetRotZ));
            } else {
                grabbedObject.transform.position = pivotPoint;
                grabbedObject.transform.rotation = Quaternion.Euler(new Vector3(palmRotationX - offsetRotX, palmRotationY - offsetRotY, palmRotationZ - offsetRotZ));
            }

            // Force drop if deactivated for some reason
            TetrominoState grabbedObjectState = grabbedObject.GetComponent<TetrominoState> ();
            if (grabbedObjectState.getState() == TetrominoState.states.INACTIVE) {
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