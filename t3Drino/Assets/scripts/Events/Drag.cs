//nabil click and drag 
//this is just a test
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curPosition;
    private bool canRotate;
    private float rotateSpeed;

    public GameObject wholeobject;
	private TetrominoState tetrominoState;
	private float directionToUnlock = 0.0f;


    // Use this for initialization
    void Start ()
    {
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
		TetrominoState tetrominoState = transform.gameObject.GetComponent<TetrominoState> ();
		foreach (Transform child in transform) {
			child.GetComponent<Renderer> ().material.color = Color.black;
		}
		tetrominoState.setState (TetrominoState.states.INACTIVE);	
    }
}
