//nabil click and drag 
//this is just a test
using UnityEngine;
using System.Collections;

public class ClickDrag : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;

    public GameObject wholeobject;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        if(transform.parent.tag == "movableTag")
        transform.parent.transform.position = curPosition; //I make the parent move to the ouse's position.

    }
    
    void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<Renderer>().material.color = Color.black;
        transform.parent.tag = "notMovableTag";
    }

}
