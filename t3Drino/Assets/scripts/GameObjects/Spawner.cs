//Nabil spawner
//this is just a test
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public float period = 3f; //every 3 seconds
	private float nextActionTime = 0.0f;
	private int _tetrinoCount = 0;
	private TetrinoSelector _tetrinoSelector;
	private TetrinoMeshSelector _tetrinoMeshSelector;

	//this script is attached to an empty GameObject called TheSpawner
	// if you click on TheSpawner in the Hierarchy column you will see this script as 
	// a component of TheSpawner in the Innspector column

	// created an array of GameObjects , since this array is public
	//you can see it in the  inspector
	// I dragged my tetrino prefabs in the array and we now have  ref to each one of the 
	//public GameObject[] golist = new GameObject[6];
	public int numberOfTetrinosToSpawn=20;


	public Mesh me1;
	public Mesh me2;
	public Mesh me3;
	public Mesh me4;
	public Mesh me5;

	private GameObject  MeshSelectorObject;
	private TetrinoMeshSelector tms;
	// Use this for initialization
	void Start () {
		/*
		MeshSelectorObject = (GameObject)Resources.Load("MeshSelector/MeshSelectorPrefab");
		tms = MeshSelectorObject.GetComponent<TetrinoMeshSelector>();
		if (tms != null)
		{
			me5 = tms.getmesh();
			if (me5 == null) Debug.Log("nomesh returned");
		}
		else
			Debug.Log("tms is null");
		*/
		GameObject go = GameObject.Find("TetrinoSelector");
		_tetrinoSelector = (TetrinoSelector) go.GetComponent(typeof(TetrinoSelector));


		GameObject go2 = GameObject.Find("TetrinoMeshSelect");
		_tetrinoMeshSelector = (TetrinoMeshSelector)go2.GetComponent(typeof(TetrinoMeshSelector));
	}

	void FixedUpdate()
	{
		if (Time.time > nextActionTime)
		{
			nextActionTime += period;

 
			_tetrinoCount++;
			if (_tetrinoCount < numberOfTetrinosToSpawn)
			{
				GameObject go = _tetrinoSelector.Pop();

				foreach (Transform child in go.transform ) {
					child.GetComponent<MeshFilter>().mesh = _tetrinoMeshSelector.getmesh();
				}
 

			  

				Instantiate(go, transform.position, transform.rotation); //this will instanciate a tetrino at the location and rotation of the object on which this script is attached (TheSpawner)
			}


		}
	}
}
