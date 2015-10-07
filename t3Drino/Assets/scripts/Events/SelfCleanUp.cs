//YU: Delete self if no children

using UnityEngine;
using System.Collections;

public class SelfCleanUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (this.transform.childCount == 0) {
			Object.Destroy(this.gameObject);
		}
	}
}
