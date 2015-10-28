using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// To use in other classes, do: TetrinoSelector _tetrinoSelector =  obj.AddComponent<TetrinoSelector>();
// _tetrinoSelector.Pop() to return a GameObject tetrino prefab

public class TetrinoSelector : MonoBehaviour {

	private List<Object> _queue;
	private Object[] _prefabs;
	private int _queueSize;

	// Use this for initialization
	public TetrinoSelector()
	{
		_queue = new List<Object>();
		_queueSize = 10;
		_prefabs = (Object[])Resources.LoadAll("Tetrinos");

		// Pre-populate queue
		for (int ii = 0; ii < _queueSize; ii++)
		{
			GameObject tetrino = GetNextTetrino();
			_queue.Add(tetrino);
		}
	}

	#region Public Properties
	public List<Object> Queue
	{
		get
		{
			return _queue;
		}
	}

	#endregion
	
	public GameObject Pop()
	{
		_queue.Add(GetNextTetrino());
		GameObject go = (GameObject)_queue[0];
		_queue.RemoveAt(0);
		return go;
	}

	private GameObject GetNextTetrino(){
		return (GameObject)_prefabs[Random.Range(0, _prefabs.Length)];
	}
}
