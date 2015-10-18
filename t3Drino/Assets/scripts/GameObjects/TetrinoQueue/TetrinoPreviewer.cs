using UnityEngine;
using System.Collections;

// This class takes in a queue of tetrominos, and enables previewing up to a designated number of tetrominos in the queue

public class TetrinoPreviewer : MonoBehaviour {

	public GameObject NextTetrino;

	private TetrinoSelector _tetrinoSelector;
	private Texture2D[] _tetrinoSprites;
	private Texture2D _nextTetrinoSprite;
	private int _totalSprites;
	private int I, J, L, O, S, T, Z;

	// Use this for initialization
	void Start () {

		// Load selector/queue
		GameObject go = GameObject.Find("TetrinoSelector");
		_tetrinoSelector = (TetrinoSelector) go.GetComponent(typeof(TetrinoSelector));

		// Load texture array indices
		_totalSprites = 7;

		I = 0;
		J = 1;
		L = 2;
		O = 3;
		S = 4;
		T = 5;
		Z = 6;

		// Load textures
		_tetrinoSprites = new Texture2D[_totalSprites];
		_tetrinoSprites[I] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteI, typeof(Texture2D));
		_tetrinoSprites[J] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteJ, typeof(Texture2D));
		_tetrinoSprites[L] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteL, typeof(Texture2D));
		_tetrinoSprites[O] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteO, typeof(Texture2D));
		_tetrinoSprites[S] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteS, typeof(Texture2D));
		_tetrinoSprites[T] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteT, typeof(Texture2D));
		_tetrinoSprites[Z] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteZ, typeof(Texture2D));

		NextTetrino = GameObject.CreatePrimitive(PrimitiveType.Quad);							// Create base game object of type Quad
		NextTetrino.name = "NextTetrino";														// Name base game object
		Destroy(NextTetrino.GetComponent("MeshCollider"));										// Remove collider
		NextTetrino.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Diffuse");	// Set transparency

		// Set location
		NextTetrino.transform.position = new Vector3(16.49f, 12.81f, 0);
	}


	void Update () {

		float scaleWidth = 1;
		float scaleHeight = 1;

		float widthIO = 15f;
		float heightIO = widthIO;

		float widthJLSTZ = 10f;
		float heightJLSTZ = widthJLSTZ * 1.5f;

		// Set texture and scaling
		string tetrinoName = _tetrinoSelector.Queue[0].name;

		if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameI))
		{
			_nextTetrinoSprite = _tetrinoSprites[I];
			scaleWidth = widthIO;
			scaleHeight = heightIO;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameJ))
		{
			_nextTetrinoSprite = _tetrinoSprites[J];
			scaleWidth = widthJLSTZ;
			scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameL))
		{
			_nextTetrinoSprite = _tetrinoSprites[L];
			scaleWidth = widthJLSTZ;
			scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameO))
		{
			_nextTetrinoSprite = _tetrinoSprites[O];
			scaleWidth = widthIO;
			scaleHeight = heightIO;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameS))
		{
			_nextTetrinoSprite = _tetrinoSprites[S];
			scaleWidth = widthJLSTZ;
			scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameT))
		{
			_nextTetrinoSprite = _tetrinoSprites[T];
			scaleWidth = widthJLSTZ;
			scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameZ))
		{
			_nextTetrinoSprite = _tetrinoSprites[Z];
			scaleWidth = widthJLSTZ;
			scaleHeight = heightJLSTZ;
		}

		// Change texture and rescale
		NextTetrino.GetComponent<Renderer>().material.mainTexture = _nextTetrinoSprite;
		NextTetrino.GetComponent<Transform>().localScale = new Vector3(_nextTetrinoSprite.width/scaleWidth,
		                                                               _nextTetrinoSprite.height/scaleHeight,
		                                                               1);
	}
}
