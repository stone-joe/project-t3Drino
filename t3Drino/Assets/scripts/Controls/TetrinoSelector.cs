using UnityEngine;
using System.Collections;

// To use in other classes, do: TetrinoSelector _tetrinoSelector =  obj.AddComponent<TetrinoSelector>();
// _tetrinoSelector.Pop() to return a GameObject tetrino prefab

public class TetrinoSelector : MonoBehaviour {

	private Queue _queue;
	private Object[] _prefabs;

	// Use this for initialization
	public TetrinoSelector()
	{
		_queue = new Queue();
		_prefabs = (Object[])Resources.LoadAll("Tetrinos");

		// Pre-populate queue
		for (int ii = 0; ii < 10; ii++)
		{
			GameObject go = (GameObject)_prefabs[Random.Range(0, _prefabs.Length)];
			_queue.Enqueue(go);
		}
	}
	
	public GameObject Pop()
	{
		GameObject go = (GameObject)_prefabs[Random.Range(0, _prefabs.Length)];
		_queue.Enqueue(go);

		return (GameObject)_queue.Dequeue();
	}
}
