//Nabil raycaster and block destroyer
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingRay : MonoBehaviour {

    private int neighborCount = 0;
    private bool lineInvalid = false;
    private bool allObjectsInThreshhold = true;
    private float rayYCoord;
    private float threshholdUpper;
    private float threshholdLower;
    private float halfDistanceBetweenRays;
    private float plusMinusThreshholdRangeFromRay;

    private RaycastHit[] hits;
    private int layerMask = 1 << 9; // Only Raycast 'blocks' layer (9)
    private Transform hitBlock; 
    private Vector3 difference;

    private ScoreManager _scoreManager;

    // Use this for initialization
    void Start () {

        // Get score manager reference
        GameObject go = GameObject.Find("ScoreManager");
        _scoreManager = (ScoreManager) go.GetComponent(typeof(ScoreManager));
    
        // There are two ways/options to determine if line should be cleared via block distance from this ray
        // Set values depending on which option used. See method Update() to select option 1 or 2
        halfDistanceBetweenRays = 1.10f;            // For option 1
        plusMinusThreshholdRangeFromRay = 0.6f;     // For option 2
    }
    
    // Update is called once per frame
    void Update () {

        Debug.DrawRay(transform.position, transform.right * 100); // this only draws a line. just for visuals

        // Set threshhold
        rayYCoord = transform.position.y;
        threshholdUpper = rayYCoord + halfDistanceBetweenRays;  // For option 1
        threshholdLower = rayYCoord - halfDistanceBetweenRays;  // For option 1

        // Get the collisions from hitting this raycast
        hits = Physics.RaycastAll(transform.position, transform.right, 100, layerMask);

        if (hits.Length >= 7) {

            // Only clear line if all objects are stationary
            lineInvalid = false;

            // Only clear line if all objects are within threshhold
            allObjectsInThreshhold = true;

            // If any one of the blocks is still lineInvalid, set lineInvalid flag
            foreach (RaycastHit hit in hits) {
                TetrominoState tetrominoState = hit.transform.gameObject.GetComponent<TetrominoState> ();

                if (tetrominoState.getState() == TetrominoState.states.INACTIVE) {
                    if (hit.transform.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.05F) {
                        lineInvalid = true;
                    }

                    float hitBlockY = hit.collider.transform.position.y;                    
                    // There are two ways to determine if block is within threshhold/should be destroyed or not.
                    // Option 1
                    /*if (!(hitBlockY >= threshholdLower && hitBlockY < threshholdUpper))
                    {
                        allObjectsInThreshhold = false;
                    }*/
                    // Option 2: This seems easier to adjust
                    float distanceBlockFromRay = hitBlockY - rayYCoord;
                    if (distanceBlockFromRay > plusMinusThreshholdRangeFromRay)
                    {
                        allObjectsInThreshhold = false;
                    }
                } else {
                    lineInvalid = true;
                }
                
            }

            if (!lineInvalid && allObjectsInThreshhold) {
                _scoreManager.AddToScore(10f);      // Increase score
                DestroyAndDeparent();               // Destroy and initiate explosion
            }
        }
    }

    public void DestroyAndDeparent()
    {       
        foreach (RaycastHit hit in hits) {
            // Get hit block
            hitBlock = hit.collider.transform;
            
            if (hitBlock.parent != null) {
                TetrominoState tetrominoState = hitBlock.parent.gameObject.GetComponent<TetrominoState> ();  
                if (tetrominoState.getState() == TetrominoState.states.INACTIVE) { // only handle blocks, not the side walls
                    
                    hitBlock.GetComponent<Renderer> ().material.color = Color.red;
                    
                    // Make deleting blocks freeze to prevent chain reaction line completions
                    if (hitBlock.gameObject.GetComponent<Rigidbody>() == null) {
                        hitBlock.gameObject.AddComponent<Rigidbody>();
                    }
                    hitBlock.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    hitBlock.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    
                    //getting the position of explosion, the starting the explosion.
                    //each explosion has a ExplosionSelfDestruct.cs which takes care of destroying the eplosion particle system
                    Vector3 pos = hitBlock.transform.position;         
                    GameObject explo = (Resources.Load("Explosions/Explosion1", typeof(GameObject))) as GameObject;
                    Instantiate(explo, pos, Quaternion.identity);
                    
                    Destroy(hitBlock.gameObject, 1F);
                    
                    Cube cubeState = hitBlock.gameObject.GetComponent<Cube> ();
                    cubeState.setState(Cube.states.DESTROYING);
                    //hitBlock.gameObject.tag = "blockDestroyingTag";
                    
                    // iterate through all children blocks in tet/new parent...
                    foreach (Transform child in hitBlock.parent) {
                        // ...except for the destroyed block or any destroying block
                        if (child.name != hitBlock.name && child.gameObject.GetComponent<Cube>().getState() != Cube.states.DESTROYING) {
                            neighborCount = 0;
                            // count number of adjacent neighbors...
                            foreach (Transform potentialNeighbor in hitBlock.parent) {
                                // ...besides the destroyed block or any other blocks to be destroyed
                                if (potentialNeighbor.name != child.name && potentialNeighbor.gameObject.GetComponent<Cube>().getState() != Cube.states.DESTROYING) {
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
                                newParent.AddComponent<TetrominoState>().setState(TetrominoState.states.INACTIVE);
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