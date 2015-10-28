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
	/**
	 * @member {Vector3} prevScreenPoint
	 * @description The previous position of the mouse - used to calculate which direction the mouse is moving
	 */
	private Vector3 prevScreenPoint;
	/**
	 * @member {float[]} wallPositions
	 * @description An array containing the x-positions of the inner part of each wall. wallPositions[0] => left wall
	 * wallPositions[1] => right wall
	 */
	private float[] wallPositions = {0.0f, 0.0f};


    // Use this for initialization
    void Start ()
    {
        rotateSpeed = 300.0f;
		tetromino = transform.gameObject.GetComponent<Tetromino>();

		// Get wall positions
		GameObject wallLeft = GameObject.Find ("wallLeft");
		wallPositions [0] = wallLeft.transform.position.x + wallLeft.GetComponent<Renderer> ().bounds.size.x / 2;
		GameObject wallRight = GameObject.Find ("wallRight");
		wallPositions [1] = wallRight.transform.position.x - wallRight.GetComponent<Renderer>().bounds.size.x / 2;
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
		float rotateDirection = 0.0f;

		if (tetromino) {
			if (tetromino.getState () == Tetromino.states.GRABBED) {
				if (Input.GetKey (KeyCode.LeftArrow)){
					rotateDirection = -1.0f;
					Debug.Log (transform.eulerAngles.z);
				}
				else
	                if (Input.GetKey (KeyCode.RightArrow)){
					rotateDirection = 1.0f;
					Debug.Log (transform.eulerAngles.z);
				}

				transform.RotateAround (curPosition, Vector3.back, rotateSpeed * Time.deltaTime * rotateDirection);
			}
		}
    }

    void OnMouseDown()
    {
		if (tetromino.getState () != Tetromino.states.INACTIVE) {
			tetromino.setState (Tetromino.states.GRABBED);
			screenPoint = Camera.main.WorldToScreenPoint (transform.position);
			prevScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
    }

    void OnMouseUp()
    {
		if (tetromino.getState () != Tetromino.states.INACTIVE) {
			tetromino.setState (Tetromino.states.ACTIVE);
		}
    }

    void OnMouseDrag()
    {
		if(tetromino.getState() == Tetromino.states.GRABBED) {
			Vector3 curScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	        curPosition = curScreenPoint + offset;
			Tetromino.WallCollision collisionData = tetromino.willCollideWithWall(curScreenPoint, prevScreenPoint);

			if ( collisionData.wall == Tetromino.Wall.NONE ){
				// If none of the corners will be outside of the wall then it
				transform.position = curPosition; //I make the parent move to the mouse's position.            
			}
			else {
				tetromino.moveToWall(collisionData.wall, curPosition.y);
			}
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {	
		if (tetromino) {
			if ( (collision.transform.gameObject.GetComponent<Tetromino>() != null && collision.transform.gameObject.GetComponent<Tetromino>().getState() == Tetromino.states.INACTIVE) 
			    	|| collision.transform.name == "floor" ){
				foreach (Transform child in transform) {
					child.GetComponent<Renderer> ().material.color = Color.black;
				}
				tetromino.setState (Tetromino.states.INACTIVE);	
			}
		}
    }
}
