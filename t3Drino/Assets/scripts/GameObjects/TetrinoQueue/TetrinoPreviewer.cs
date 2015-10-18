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
	private float _scaleWidth;
	private float _scaleHeight;

	// Use this for initialization
	void Start () {
		
		LoadTetrinoSelectorReference();					// Load selector/queue
		LoadTextureArrayIndices();
		LoadTextures();
		InitializeNextTetrominoGO();
		SetNextTetrominoLocation(16.49f, 12.81f, 0);	// Set previewer location
	}


	void Update () {

		SetNextTetromino();

		// Change texture and rescale
		NextTetrino.GetComponent<Renderer>().material.mainTexture = _nextTetrinoSprite;
		NextTetrino.GetComponent<Transform>().localScale = new Vector3(_nextTetrinoSprite.width/_scaleWidth,
		                                                               _nextTetrinoSprite.height/_scaleHeight,
		                                                               1);
	}

	private void LoadTetrinoSelectorReference()
	{
		GameObject go = GameObject.Find("TetrinoSelector");
		_tetrinoSelector = (TetrinoSelector) go.GetComponent(typeof(TetrinoSelector));
	}

	private void LoadTextureArrayIndices()
	{
		_totalSprites = 7;
		
		I = 0;
		J = 1;
		L = 2;
		O = 3;
		S = 4;
		T = 5;
		Z = 6;
	}

	private void LoadTextures()
	{
		_tetrinoSprites = new Texture2D[_totalSprites];
		_tetrinoSprites[I] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteI, typeof(Texture2D));
		_tetrinoSprites[J] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteJ, typeof(Texture2D));
		_tetrinoSprites[L] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteL, typeof(Texture2D));
		_tetrinoSprites[O] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteO, typeof(Texture2D));
		_tetrinoSprites[S] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteS, typeof(Texture2D));
		_tetrinoSprites[T] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteT, typeof(Texture2D));
		_tetrinoSprites[Z] = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(TetrinoSpriteLibrary.PathTetrominoSpriteZ, typeof(Texture2D));
	}

	private void InitializeNextTetrominoGO()
	{
		NextTetrino = GameObject.CreatePrimitive(PrimitiveType.Quad);							// Create base game object of type Quad
		NextTetrino.name = "NextTetrino";														// Name base game object
		Destroy(NextTetrino.GetComponent("MeshCollider"));										// Remove collider
		NextTetrino.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Diffuse");	// Set transparency
	}

	private void SetNextTetrominoLocation(float x, float y, float z)
	{
		NextTetrino.transform.position = new Vector3(x, y, z);
	}

	private void SetNextTetromino()
	{
		float widthIO = 15f;
		float heightIO = widthIO;
		
		float widthJLSTZ = 10f;
		float heightJLSTZ = widthJLSTZ * 1.5f;

		// Set texture and scaling
		string tetrinoName = _tetrinoSelector.Queue[0].name;
		
		if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameI))
		{
			_nextTetrinoSprite = _tetrinoSprites[I];
			_scaleWidth = widthIO;
			_scaleHeight = heightIO;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameJ))
		{
			_nextTetrinoSprite = _tetrinoSprites[J];
			_scaleWidth = widthJLSTZ;
			_scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameL))
		{
			_nextTetrinoSprite = _tetrinoSprites[L];
			_scaleWidth = widthJLSTZ;
			_scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameO))
		{
			_nextTetrinoSprite = _tetrinoSprites[O];
			_scaleWidth = widthIO;
			_scaleHeight = heightIO;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameS))
		{
			_nextTetrinoSprite = _tetrinoSprites[S];
			_scaleWidth = widthJLSTZ;
			_scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameT))
		{
			_nextTetrinoSprite = _tetrinoSprites[T];
			_scaleWidth = widthJLSTZ;
			_scaleHeight = heightJLSTZ;
		}
		else if (tetrinoName.Equals(TetrinoSpriteLibrary.PrefabTetrominoNameZ))
		{
			_nextTetrinoSprite = _tetrinoSprites[Z];
			_scaleWidth = widthJLSTZ;
			_scaleHeight = heightJLSTZ;
		}
	}
}
