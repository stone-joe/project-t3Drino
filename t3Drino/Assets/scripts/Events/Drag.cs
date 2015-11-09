//nabil click and drag 
//this is just a test
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curPosition;
    private float rotateSpeed;
    private TetrominoState tetrominoState;
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
        rotateSpeed = 300.0f;
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
        if (tetrominoState == null) {
            tetrominoState = transform.gameObject.GetComponent<TetrominoState>();
        }
        
        float rotateDirection = 0.0f;
        if (tetrominoState.getState() == TetrominoState.states.GRABBED) {
            if (Input.GetKey(KeyCode.LeftArrow)) rotateDirection = -1.0f;
            else
                if (Input.GetKey(KeyCode.RightArrow)) rotateDirection = 1.0f;

            transform.RotateAround(curPosition, Vector3.back, rotateSpeed * Time.deltaTime * rotateDirection);
        }
        if (isLeapMenuTet && transform.position.y < -20F) {
            transform.position = initPosition;
            transform.localEulerAngles = initRotation;
        }
    }

    void OnMouseDown()
    {
        if (tetrominoState.getState () != TetrominoState.states.INACTIVE) {
            tetrominoState.setState (TetrominoState.states.GRABBED);
            screenPoint = Camera.main.WorldToScreenPoint (transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseUp()
    {
        if (tetrominoState.getState () != TetrominoState.states.INACTIVE) {
            tetrominoState.setState (TetrominoState.states.ACTIVE);
        }
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if(transform.gameObject.GetComponent<TetrominoState>().getState() == TetrominoState.states.GRABBED) {
            transform.position = curPosition; //I make the parent move to the mouse's position.
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {   
        if (!isLeapMenuTet) {
            TetrominoState tetrominoState = transform.gameObject.GetComponent<TetrominoState> ();
            
            // TODO: Merge this & apply state changes... this code initalizes the lose screen
            if (tetrominoState.getState() == TetrominoState.states.INACTIVE && collision.gameObject.name == "roof")
            {
                GameObject go = GameObject.Find("Main Camera");
                _classicModeState = (Classic)go.GetComponent(typeof(Classic));
                _classicModeState.InitLoseScreen();
            }

            foreach (Transform child in transform) {
                child.GetComponent<Renderer> ().material.color = Color.grey;
            }
            tetrominoState.setState (TetrominoState.states.INACTIVE);
        }
    }
}
