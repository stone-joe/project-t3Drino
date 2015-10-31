using UnityEngine;
using System.Collections;

public class TetrinoMeshSelector : MonoBehaviour
{

	public Mesh me;

	// Use this for initialization
	void Start () {

        GameObject go = GameObject.Instantiate(Resources.Load("BlockTypes/simpleCube3")) as GameObject;
	   me = go.transform.GetComponent<MeshFilter>().mesh;

	  // if (me == null) Debug.Log(" what the f");
       print(go);
	}

   public Mesh getmesh() { return me; }

	 
}
