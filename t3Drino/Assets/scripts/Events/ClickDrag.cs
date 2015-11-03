//nabil click and drag 
//this is just a test
using UnityEngine;
using System.Collections;

public class ClickDrag : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curPosition;
    private bool canRotate;
    private float rortateSpeed;
    private Vector3 initPosition; // For Leap Menu respawn of shape
    private Vector3 initRotation; // For Leap Menu respawn of shape

    public GameObject wholeobject;
    public Classic _classicModeState;
    public bool isLeapMenuTet; // For Leap Menu. Don't deactivate shapes when they hit the table.

    // Use this for initialization
    void Start ()
    {
        initPosition = transform.position;
        initRotation = transform.localEulerAngles;
        canRotate = false;
        rortateSpeed = 300.0f;
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
        float rotateDirection = 0.0f;
        if (canRotate && transform.tag == "movableTag") {
            if (Input.GetKey(KeyCode.LeftArrow)) rotateDirection = -1.0f;
            else
                if (Input.GetKey(KeyCode.RightArrow)) rotateDirection = 1.0f;

            transform.RotateAround(curPosition, Vector3.back, rortateSpeed * Time.deltaTime * rotateDirection);
        }

        if (isLeapMenuTet && transform.position.y < -15F) {
            transform.position = initPosition;
            transform.localEulerAngles = initRotation;
        }
    }

    void OnMouseDown()
    {
        canRotate = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseUp()
    {
        canRotate = false;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
         curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if(transform.tag == "movableTag") {
            transform.position = curPosition; //I make the parent move to the mouse's position.
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {   
        if (!isLeapMenuTet) {
            // TODO: Merge this & apply state changes... this code initalizes the lose screen
            if (transform.tag == "notMovableTag" && collision.gameObject.name == "roof")
            {
                GameObject go = GameObject.Find("Main Camera");
                _classicModeState = (Classic)go.GetComponent(typeof(Classic));
                _classicModeState.InitLoseScreen();
            }

            foreach (Transform child in transform) {
                child.GetComponent<Renderer> ().material.color = Color.black;
            }
            transform.tag = "notMovableTag";
        }
    }

}
