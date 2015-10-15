using UnityEngine;
using System.Collections;
using Leap;

public class HandScript : MonoBehaviour {

    private Transform[] childTransforms;
    private Hand hand;

    public float grabStrength;

    void Start () {
        //Assign hand rigidbodies to layer 8 to disable collision with inactive objects
        childTransforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform transform in childTransforms) {
            transform.gameObject.layer = 8;
        }
    }

    void Update() {
        hand = GetComponent<HandModel>().GetLeapHand();
        grabStrength = hand.GrabStrength;
    }
}