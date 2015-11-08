using UnityEngine;
using System.Collections;

public class ShapeDetect : MonoBehaviour {

    private Transform detectedTransform;

    public LeapMenu leapMenuScript;
    public AudioClip fart;
    public AudioSource audio;

    // Use this for initialization
    void Start () {
        leapMenuScript = GameObject.Find("Main Camera").GetComponent<LeapMenu>();
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void OnTriggerEnter(Collider other) {
        if (other.transform.root.transform.tag == "movableTag" && other.transform.root.transform != detectedTransform) {
            detectedTransform = other.transform.root.transform;
            print(string.Format("ENTERED: {0}", other.transform.root.transform));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.transform.root.transform == detectedTransform) {
            detectedTransform = null;
            print(string.Format("EXITED: {0}", other.transform.root.transform));

            if (other.transform.root.transform.name == "start_tet") {
                leapMenuScript.StartClassicMode();
                AudioSource.PlayClipAtPoint(fart, new Vector3(0, 0, 0));
            } else if (other.transform.root.transform.name == "quit_tet") {
                leapMenuScript.Quit();
            }
        }
    }
}
