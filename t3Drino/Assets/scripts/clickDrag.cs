//nabil click and drag 
//this is just a test
using UnityEngine;
using System.Collections;

public class clickDrag : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 curPosition;
    private bool canRotate;
    private float rortateSpeed;

	private GameObject _wallLeft;
	private GameObject _wallRight;
	private Vector3 _negativeXBorder;
	private Vector3 _positiveXBorder;
	private Vector3 _targetPosition;
	private bool _collided;

	public Classic _classicModeState;

    // Use this for initialization
    void Start ()
    {
        canRotate = false;
        rortateSpeed = 300.0f;
		_wallLeft = GameObject.Find("wallLeft");
		_wallRight = GameObject.Find("wallRight");
		_collided = false;
    }
    
    // Update is called once per frame
    void FixedUpdate ()
    {
        float rotateDirection = 0.0f;
        if (canRotate  && transform.tag == "movableTag") {
            if (Input.GetKey(KeyCode.LeftArrow)) rotateDirection = -1.0f;
            else
                if (Input.GetKey(KeyCode.RightArrow)) rotateDirection = 1.0f;

            transform.RotateAround(curPosition, Vector3.back, rortateSpeed * Time.deltaTime * rotateDirection);
        }
    }

    void OnMouseDown()
    {
        canRotate = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

		_negativeXBorder = new Vector3(_wallLeft.transform.position.x, _wallLeft.transform.position.y, screenPoint.z);
		_positiveXBorder = new Vector3(_wallRight.transform.position.x, _wallRight.transform.position.y, screenPoint.z);
    }

    void OnMouseUp()
    {
        canRotate = false;
    }

	void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		if (!_collided)
        	curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

		if(transform.tag == "movableTag" && curPosition.x > _negativeXBorder.x && curPosition.x < _positiveXBorder.x) {
			transform.position = Vector3.Lerp(transform.position, curPosition, Time.deltaTime * 30f);
        }
		_collided = false;
    }
    
    void OnCollisionEnter(Collision collision)
    {
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
