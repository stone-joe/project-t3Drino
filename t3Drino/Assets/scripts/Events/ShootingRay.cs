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
    private ScoreManager _scoreManager;
    private bool _isRowCleared;

    // Use this for initialization
    void Start () {
        // Get score manager reference
        GameObject go = GameObject.Find("ScoreManager");
        _scoreManager = (ScoreManager) go.GetComponent(typeof(ScoreManager));

        //_isRowCleared = false;
    }


    void doExplosion(Transform thisHitblock) {
        //prefab_S_tet(Clone)  Explosion_SZ
        //prefab_Z_tet(Clone)  Explosion_SZ
        //prefab_L_tet(Clone)  Explosion_LJ
        //prefab_J_tet(Clone)  Explosion_LJ
        //prefab_O_tet(Clone)  Explosion_O
        //prefab_I_tet(Clone)  Explosion_I
        //prefab_T_tet(Clone)  Explosion_T
        // default Explosion_simple

        


        Vector3 pos = thisHitblock.transform.position;
        string explosionpath = "Explosions/";
        string nameofparent = thisHitblock.transform.parent.name;
        switch (nameofparent) { 
            case "prefab_S_tet(Clone)":
                explosionpath = explosionpath + "Explosion_SZ";
                break;
            case "prefab_Z_tet(Clone)":
                explosionpath = explosionpath + "Explosion_SZ";
                break;
            case "prefab_L_tet(Clone)":
                explosionpath = explosionpath + "Explosion_LJ";
                break;
            case "prefab_J_tet(Clone)":
                explosionpath = explosionpath + "Explosion_LJ";
                break;
            case "prefab_O_tet(Clone)":
                explosionpath = explosionpath + "Explosion_O";
                break;
            case "prefab_I_tet(Clone)":
                explosionpath = explosionpath + "Explosion_I";
                break;
            case "prefab_T_tet(Clone)":
                explosionpath = explosionpath + "Explosion_T";
                break;
            default:
                explosionpath = explosionpath + "Explosion_simple";
                break;
        }


        Debug.Log("my name is " + nameofparent);

        GameObject explo = (Resources.Load(explosionpath, typeof(GameObject))) as GameObject;
        Instantiate(explo, pos, Quaternion.identity);
    
    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(transform.position, transform.right * 100); // this only draws a line. just for visuals

        hits = Physics.RaycastAll(transform.position, transform.right, 100, layerMask);
        /*foreach (RaycastHit hit in hits)
        {

            // hit.y
            //blockthatwashit.transform.y - hit.point.y
            // if above is smaller

            // DETERMINE THRESTHOLD ABOVE/BELOW RayCast

            // else hit = null
        }

        foreach (RaycastHit hit in hits)
        {
            if (hit == null)
            {
                // add to new array of hits
            }
        }*/

        /*if (hits.Length >= 7)
            _isRowCleared = true;

        // Check that we detect 8 blocks
        // hit.length also includes the 2 vertical walls 
        if (hits.Length >= 7 && _isRowCleared) {
            _isRowCleared = false;

            // TODO: Add to score since a row was cleared: UPDATE THIS.. THIS IS AN INITIAL TEST
            _scoreManager.AddToScore(10f);
        }*/

        if (hits.Length >= 7) {
            // Only clear line if all objects are stationary
            moving = false;

            // Check that the block is moving or not.
            foreach (RaycastHit hit in hits) {
                if (hit.transform.gameObject.tag == "notMovableTag" && hit.transform.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.05F) {
                    moving = true;
                }
            }

            if (!moving) {
	        _scoreManager.AddToScore(10f);
                foreach (RaycastHit hit in hits) {
                    // Get hit block
                    hitBlock = hit.collider.transform;

                    if (hitBlock.parent != null && hitBlock.parent.gameObject.tag == "notMovableTag") { // only handle blocks, not the side walls

                        hitBlock.GetComponent<MeshRenderer>().enabled = false;
                        
                        // Make deleting blocks freeze to prevent chain reaction line completions
                        if (hitBlock.gameObject.GetComponent<Rigidbody>() == null) {
                            hitBlock.gameObject.AddComponent<Rigidbody>();
                        }
                        hitBlock.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hitBlock.gameObject.GetComponent<Rigidbody>().isKinematic = true;


                        //geting the position of explosion, the starting the explosion.
                        //each explosion has a ExplosionSelfDestruct.cs which takes care of destroying the eplosion particle system
                        doExplosion(hitBlock);
                       
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