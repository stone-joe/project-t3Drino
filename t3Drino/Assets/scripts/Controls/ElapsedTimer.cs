using UnityEngine;
using System.Collections;

public class ElapsedTimer : Timer {

	public string txtElapsed;
	private TextMesh _txtMesh;
	private string timer = "0";

	// Use this for initialization
	public override void Start ()
	{
		base.Start();

		SetTimer(0f, float.PositiveInfinity, 0f);
		BeginCountUp();

		_txtMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.UpdateTime();
		_txtMesh.text = base.TextCurrentTime;
	}
}
