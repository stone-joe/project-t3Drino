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

	private Tetromino tetromino;
    public GameObject wholeobject;


    // Use this for initialization
    void Start ()
    {
        rotateSpeed = 300.0f;
		tetromino = transform.gameObject.GetComponent<Tetromino>();
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
		float rotateDirection = 0.0f;

		if (tetromino) {
			if (tetromino.getState () == Tetromino.states.GRABBED) {
				if (Input.GetKey (KeyCode.LeftArrow))
					rotateDirection = -1.0f;
				else
	                if (Input.GetKey (KeyCode.RightArrow))
					rotateDirection = 1.0f;

				transform.RotateAround (curPosition, Vector3.back, rotateSpeed * Time.deltaTime * rotateDirection);
			}
		}
    }

    void OnMouseDown()
    {
		if (tetromino.getState () != Tetromino.states.INACTIVE) {
			tetromino.setState (Tetromino.states.GRABBED);
			screenPoint = Camera.main.WorldToScreenPoint (transform.position);
			offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
    }

    void OnMouseUp()
    {
		if (tetromino.getState () != Tetromino.states.INACTIVE) {
			tetromino.setState (Tetromino.states.ACTIVE);
		}
		Debug.Log (tetromino.GetType());
    }

    void OnMouseDrag()
    {
		if(tetromino.getState() == Tetromino.states.GRABBED) {
			Vector3 curScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	        curPosition = curScreenPoint + offset;
			float[] bounds = tetromino.calculateBounds();

			if ( curScreenPoint.x > bounds[0] && curScreenPoint.x < bounds[1] ){
				// If none of the corners will be outside of the wall then it
				transform.position = curPosition; //I make the parent move to the mouse's position.            
			}
			else {
				transform.position = new Vector3(transform.position.x, curPosition.y , transform.position.z);
			}

			Debug.Log (curScreenPoint.x);
			Debug.Log (bounds[0]);
			Debug.Log (bounds[1]);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {	
		if (tetromino) {
			if ( collision.transform.gameObject.GetComponent<Tetromino>() != null ){
				foreach (Transform child in transform) {
					child.GetComponent<Renderer> ().material.color = Color.black;
				}
				tetromino.setState (Tetromino.states.INACTIVE);	
			}
		}
    }
}
