﻿using UnityEngine;
using System.Collections;

public class ElapsedTimer : Timer {

	private TextMesh _txtMesh;

	// Use this for initialization
	public override void Start ()
	{
		base.Start();

		SetTimer(0f, float.PositiveInfinity, 0f);
		BeginCountUp();

		_txtMesh = GetComponent<TextMesh>();
	}

	void FixedUpdate () 
	{
		base.UpdateTime();
		_txtMesh.text = base.TextCurrentTime;
	}
}
